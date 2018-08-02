using System;
using System.Diagnostics;
using System.ServiceModel.Description;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LoyalFilial.Framework.Job.Domain.Proxy;

namespace LoyalFilial.Framework.Job.Domain.Models
{
    /// <summary>
    /// 任务代理领域对象
    /// </summary>
    internal class JobAgent : IAggregateRoot
    {

        #region Ctors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jobUrl">任务地址</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="methodName">方法名</param>
        /// <param name="valus">值集</param>
        /// <param name="internval">间隔时间</param>
        /// <param name="effectHour">生效时间</param>
        /// <param name="expireHour">失效时间</param>
        /// <param name="name">任务名称</param>
        public JobAgent(string jobUrl, string serviceName, string methodName, string valus, int internval, int effectHour, int expireHour, string name)
        {
            Id = Guid.NewGuid();
            var factory = new DynamicProxyFactory(jobUrl);
            dynamicProxy = factory.CreateProxy(serviceName);
            timer = new System.Timers.Timer(internval);
            this.effectHour = effectHour;
            this.expireHour = expireHour;
            Name = name;
            this.serviceName = serviceName;
            this.valus = valus;
            jobAgentContract = new JobAgentContract(methodName, valus.Split(','));
            timerHandler = this.timer_Elapsed;

            foreach (ServiceEndpoint endpoint in factory.Endpoints)
            {
                Console.WriteLine("Service Endpoint={0}", endpoint.Name);
                Console.WriteLine("\tAddress = " + endpoint.Address);
                Console.WriteLine("\tContract = " + endpoint.Contract.Name);
                Console.WriteLine("\tBinding = " + endpoint.Binding.Name);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region Fields

        /// <summary>
        /// 服务名称
        /// </summary>
        string serviceName;

        /// <summary>
        /// 值集
        /// </summary>
        string valus;

        /// <summary>
        /// 任务代理契约
        /// </summary>
        JobAgentContract jobAgentContract;

        /// <summary>
        /// 计时器
        /// </summary>
        System.Timers.Timer timer;

        /// <summary>
        /// 动态代理工厂
        /// </summary>
        DynamicProxy dynamicProxy;

        /// <summary>
        /// 生效时间
        /// </summary>
        int effectHour;

        /// <summary>
        /// 失效时间
        /// </summary>
        int expireHour;

        /// <summary>
        /// 执行次数
        /// </summary>
        int runCount = 1;

        /// <summary>
        /// 计时器委托
        /// </summary>
        ElapsedEventHandler timerHandler;

        /// <summary>
        /// 是否可以运行
        /// </summary>
        protected bool canRun
        {
            get
            {
                return DateTime.Now.Hour >= effectHour && DateTime.Now.Hour <= expireHour;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 时间事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Run();
        }

        #endregion

        /// <summary>
        /// 开始
        /// </summary>
        public void Start(bool isFirstRun = false)
        {

            if (isFirstRun)
            {
                Run();
            }
            else
            {
                timer.Elapsed += timerHandler;
                timer.Enabled = true;
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void Stop()
        {
            timer.Elapsed -= timerHandler;
            timer.Enabled = false;
        }

        /// <summary>
        /// 执行
        /// </summary>
        private void Run()
        {
            var task = Task.Factory.StartNew(RunAgent);
            task.ContinueWith(t =>
            {
                if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted || task.Status == TaskStatus.Canceled)
                {
                    runCount++;
                    task.Dispose();
                }
                t.Dispose();
            });
        }

        /// <summary>
        /// 执行代理
        /// </summary>
        private void RunAgent()
        {
            Stop();
            if (canRun)
            {
                var waitCount = 0;
                while (true)
                {
                    if (waitCount > 10)
                    {
                        break;
                    }
                    if (IsRunning)
                    {
                        waitCount++;
                        Thread.Sleep(500);
                    }
                    else
                    {
                        IsRunning = true;
                        try
                        {
                            Console.WriteLine("[{0}] {1} 第{2}次开始执行", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Name, runCount);
                            Stopwatch w = new Stopwatch();
                            w.Start();
                            var response = dynamicProxy.CallMethod(jobAgentContract.MethodName, jobAgentContract.Valus);
                            w.Stop();
                            Console.WriteLine("[{0}] {1} 第{2}次执行完毕", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Name, runCount);
                            var duringTime = w.Elapsed.TotalMilliseconds;
                            if (response != null && !string.IsNullOrWhiteSpace(response.ToString()))
                            {
                                var stringBuilder = new StringBuilder();
                                stringBuilder.AppendLine(string.Format("========== {0} ==========", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                stringBuilder.AppendLine("[运行次数]");
                                stringBuilder.AppendLine(runCount.ToString());
                                stringBuilder.AppendLine("[运行时间]");
                                stringBuilder.AppendLine(duringTime.ToString());
                                stringBuilder.AppendLine("[Job名称]");
                                stringBuilder.AppendLine(Name);
                                stringBuilder.AppendLine("[返回信息]");
                                stringBuilder.AppendLine(response.ToString() ?? string.Empty);
                                //WriteLog(stringBuilder.ToString(), "log.txt");
                                Console.WriteLine(stringBuilder.ToString());
                            }
                        }
                        catch (Exception exception)
                        {
                            var stringBuilder = new StringBuilder();
                            stringBuilder.AppendLine(string.Format("========== {0} ==========", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                            stringBuilder.AppendLine("[Job名称]");
                            stringBuilder.AppendLine(Name);
                            stringBuilder.AppendLine("[返回信息]");
                            stringBuilder.AppendLine(exception.Message);
                            stringBuilder.AppendLine("[Source]");
                            stringBuilder.AppendLine(exception.Source);
                            stringBuilder.AppendLine("[StackTrace]");
                            stringBuilder.AppendLine(exception.StackTrace);
                            WriteLog(stringBuilder.ToString(), "exception.txt");
                        }
                        finally
                        {
                            IsRunning = false;
                        }
                        break;
                    }
                }
            }
            Start();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logContent"></param>
        private void WriteLog(string logContent, string fileName)
        {
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.WriteLine(logContent + "\r\n");
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}

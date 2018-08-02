using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start(); //  开始监视代码运行时间

            //var entityList = LoyalFilial.Framework.Core.Framework.DataManager.TableQuery<CqsEntity>().Select(t => t.Name, t => t.Age).From().Where().OrderByDesc(t => t.Age, t => t.No).Page(10, 1).ExecuteList();
            //if (entityList != null && entityList.Count > 0)
            //{
            //    foreach (var et in entityList)
            //    {
            //        Response.Write(et.Name + "<br/>");
            //    }

            //}
            //stopwatch.Stop(); //  停止监视
            //Response.Write("查询耗时：" + stopwatch.Elapsed.TotalMilliseconds + "<br/>");


            //stopwatch.Restart();
            //var entity = LoyalFilial.Framework.Core.Framework.DataManager.TableQuery<CqsEntity>().Select().From().Where(t => t.Age > 10).OrderByAsc().Execute();
            //if (entity != null)
            //{
            //    Response.Write(entity.Name + "<br/>");
            //}
            //stopwatch.Stop(); //  停止监视
            ////Response.Write("查询一个耗时：" + stopwatch.Elapsed.TotalMilliseconds + "<br/>");
            try
            {

                //List<CqsEntity> en = LFFK.DataManager.TableQuery<CqsEntity>().Select().From().ExecuteList();

                //Thread.Sleep(10000);
                //var cache = LFFK.CacheManager.MemCached;
                //cache["Test"] = "testeste欠";
                //Response.Write(cache["Test"] + "<br/>");

                //var lc = LFFK.CacheManager.Localcached;
                //lc["Test", CacheDependency.Create(new TimeSpan(0, 0, 0, 30))] = "testestelllll我";
                //Response.Write(lc["Test"] + "<br/>");
                //Response.Write(lc.Count + "<br/>");

                //LFFK.LogManager.Debug("测试");

                //var en1 = LFFK.DataManager.TableQuery<CqsEntity>().Select().From().Where(c => c.No == 1).Execute();
                //en1.Age += 1;
                //LFFK.DataManager.Update(en1);

                //var ens2 = LFFK.DataManager.TableQuery<CqsEntity>(LFFK.DataManager.DataProvider.GetConnectionString("mysqlTest2")).Select().From().Where(c => c.No == 1).Execute();
                //ens2.Age += 1;
                //LFFK.DataManager.Update(LFFK.DataManager.DataProvider.GetConnectionString("mysqlTest2"), ens2);

                //var en3 = LFFK.DataManager.TableQuery<CqsEntity>().Select().From().Where(c => c.No == 1).Execute();
                //en3.Age += 1;
                //LFFK.DataManager.Update(en3);



                LFFK.Init(false);
                var strList = new List<string>();
                var intList = new List<int>();
                var task = new Task(() =>
                  {
                      var cts = new CancellationTokenSource();
                      var tf = new TaskFactory(cts.Token,
                          TaskCreationOptions.AttachedToParent,
                          TaskContinuationOptions.ExecuteSynchronously,
                          TaskScheduler.Default);
                      var childTasks = new[]
                      {
                          tf.StartNew(() => this.GetName52(1, ref strList,ref intList)),
                          tf.StartNew(() => this.GetName52(2, ref strList,ref intList)),
                          tf.StartNew(() => this.GetName52(4, ref strList,ref intList)),
                          tf.StartNew(() => this.GetName54(1, ref strList,ref intList)),
                          tf.StartNew(() => this.GetName54(2, ref strList,ref intList))
                      };
                      for (int i = 0; i < childTasks.Length; i++)
                          childTasks[i].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                  });
                task.Start();
                task.Wait();

                var strListTemp = new List<string>();
                foreach (var st in strList)
                {
                    Response.Write(st + "<br/>");
                    if (strListTemp.Contains(st))
                    {
                        Response.Write("<font color='red'>" + st + "重复</font><br/>");
                    }
                    else
                    {
                        strListTemp.Add(st);
                    }
                }
                foreach (var i in intList)
                {
                    Response.Write(i + "<br/>");
                }
            }
            catch (Exception ex)
            {
                LFFK.LogManager.Error("出大错了", ex);
            }
        }

        public void GetName52(int age, ref List<string> strListName, ref List<int> strListCode)
        {

            var tq = LFFK.DataManager.TableQuery<CqsEntity>();
            var where = tq.Select().From().Where(t => t.No == age);
            var entity1 = where.Execute();
            if (entity1 != null)
            {
                strListName.Add(entity1.Name + "  &nbsp; &nbsp; &nbsp; &nbsp;  |52|  &nbsp; &nbsp; &nbsp; &nbsp;   " + where.Command.GetHashCode());
                strListCode.Add(tq.GetHashCode());
            }
        }

        public void GetName54(int age, ref List<string> strListName, ref List<int> strListCode)
        {

            var tq = LFFK.DataManager.TableQuery<CqsEntity>(LFFK.DataManager.DataProvider.GetConnectionString("mysqlTest2"));
            var where = tq.Select().From().Where(t => t.No == age);
            var entity1 = where.Execute();
            if (entity1 != null)
            {
                strListName.Add(entity1.Name + "     &nbsp; &nbsp; &nbsp; &nbsp;|54|   &nbsp; &nbsp; &nbsp; &nbsp;  " + where.Command.GetHashCode());
                strListCode.Add(tq.GetHashCode());
            }
        }
    }
}
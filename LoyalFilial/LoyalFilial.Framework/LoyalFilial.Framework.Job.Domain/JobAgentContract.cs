using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Framework.Job.Domain
{
    /// <summary>
    /// 任务代理契约
    /// </summary>
    internal class JobAgentContract
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="methodName"></param>
        public JobAgentContract(string methodName, object[] valus)
        {
            this.methodName = methodName;
            this.valus = valus;
        }

        private string methodName;

        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get { return methodName; } }

        private object[] valus;

        /// <summary>
        /// 值集
        /// </summary>
        public object[] Valus { get { return valus; } }
    }
}

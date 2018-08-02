using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Framework.Search
{
    public class Response
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 状态编码
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 花费时间
        /// </summary>
        public int QTime { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }

    public class IndexOperationResponse : Response { }

    public class QueryResponse<T> : Response
    {
        public T Data { get; set; }

        public List<T> DataList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LoyalFilial.Entity
{
    /// <summary>
    /// 公共响应结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResponseDTO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [DataMember]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        public int Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}

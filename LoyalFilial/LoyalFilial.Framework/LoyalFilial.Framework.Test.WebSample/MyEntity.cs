using LoyalFilial.Framework.Data.DataMap.Core;
using System;

namespace LoyalFilial.Framework.Test.WebSample
{
    [Serializable]
    [Table("biz_dev", "OnlineProduct")]
    public class OnlineProduct
    {
        [Column("ProductId", true, true)]
        public int ProductId { get; set; }
    }

    [Serializable]
    [Table("biz_dev", "TaoUserID")]
    public class TaoUserID
    {
        [Column("idx", true, true)]
        public int No { get; set; }

        public string UserId { get; set; }

        public string Vid { get; set; }

        public int VCount { get; set; }
    }

    [Serializable]
    [Table("biz_dev", "cqs_test")]
    public class CqsEntity
    {
       [Column("idx", true, true)]
        public int No { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Age { get; set; }

        public DateTime BoardTime { get; set; }

        public string OP { get; set; }

        public DateTime UpdateTime { get; set; }

    }

    [Serializable]
    [Table("biz_dev", "cqs_test")]
    public class CqsEntity2
    {
        [Column("idx", true, true)]
        public int No { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Age { get; set; }

        public DateTime BoardTime { get; set; }

        public string OP { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
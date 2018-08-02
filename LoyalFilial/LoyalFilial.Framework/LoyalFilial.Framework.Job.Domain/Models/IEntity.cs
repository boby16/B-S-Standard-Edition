using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Framework.Job.Domain.Models
{
    /// <summary>
    /// 实体接口
    /// </summary>
    internal interface IEntity
    {
        Guid Id { get; }
    }
}

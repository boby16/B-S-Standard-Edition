using LoyalFilial.Framework.Data.DataMap.Core;
using System;
using System.Linq;

namespace LoyalFilial.Framework.Data.DataMap.Map
{
    public class EntityMap
    {
        public PropertyMapCollection PropertyMapList { get; set; }

        public TableAttribute Table { get; private set; } 

        public EntityMap(Type objType)
        {
            this.Table = objType.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault() as TableAttribute;
        }
    }
}

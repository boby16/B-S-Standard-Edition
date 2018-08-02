using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RosetteStone.Framework.Core.Data
{
    public class PropertyInstance
    {
        public string Name { get; set; }

        public object value { get; set; }

        public Type PropertyType { get; set; }

        public string MapField { get; set; }
    }
}
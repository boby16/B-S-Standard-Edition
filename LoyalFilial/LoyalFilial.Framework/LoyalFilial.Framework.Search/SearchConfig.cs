using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Framework.Search
{
    internal class SearchConfig
    {
        public SearchConfig()
        {
            ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
            Collection = ConfigurationManager.AppSettings["Collection"];
        }

        public string ServerUrl { get; set; }

        public string Collection { get; set; }
    }
}

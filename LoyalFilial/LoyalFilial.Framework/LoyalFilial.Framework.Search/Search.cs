using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.Framework.Search
{
    public class Search
    {
        private static ISearchManage _searchManager;

        /// <summary>
        /// 搜索管理者
        /// </summary>
        public static ISearchManage SearchManager
        {
            get { return _searchManager ?? (_searchManager = new SearchManager()); }
        }
    }
}

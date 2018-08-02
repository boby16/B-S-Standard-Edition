using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.Framework.Core.Util.Enum
{
    internal static class EnumTypeCache
    {
        private static Dictionary<Type, EnumTypeCacheItem> _enumTypeCache
            = new Dictionary<Type, EnumTypeCacheItem>();

        public static bool TryGetValue(Type type, out EnumTypeCacheItem cacheItem)
        {
            return _enumTypeCache.TryGetValue(type, out cacheItem);
        }

        public static void Add(Type type, EnumTypeCacheItem cacheItem)
        {
            _enumTypeCache.Add(type, cacheItem);
        }
    }
}
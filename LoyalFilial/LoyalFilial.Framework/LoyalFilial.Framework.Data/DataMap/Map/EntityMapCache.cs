using System;
using System.Collections.Generic;
using LoyalFilial.Framework.Data.DataMap.Core;

namespace LoyalFilial.Framework.Data.DataMap.Map
{
    internal class EntityMapCache
    {
        // Fields
        private static Dictionary<Type, EntityMap> s_maps = new Dictionary<Type, EntityMap>();
        private static readonly object s_SyncObj = new object();

        protected static EntityMap BuildEntityMap(Type objType)
        {
            var em = new EntityMap(objType);
            em.PropertyMapList = BuildPropertyMap(objType);
            return em;
        }

        protected static PropertyMapCollection BuildPropertyMap(Type objType)
        {
            var pmc = new PropertyMapCollection();
            var propertyInfos = objType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                Type propertyType = propertyInfo.PropertyType;
                var columnAttribute =
                    Attribute.GetCustomAttribute(propertyInfo, typeof (ColumnAttribute), false) as ColumnAttribute;
                if (columnAttribute != null && columnAttribute.IsIgnore)
                    continue;
                pmc.Add(new PropertyMap(
                            string.Format("{0}.{1}", propertyInfo.DeclaringType.FullName, propertyInfo.Name),
                            propertyInfo, columnAttribute));
            }
            return pmc;
        }

        public static void AddMap(Type objType)
        {
            var pmc = BuildEntityMap(objType);
            AddMap(objType, pmc);
        }

        public static void AddMap(Type objType, EntityMap em)
        {
            lock (s_SyncObj)
            {
                if (!s_maps.ContainsKey(objType))
                {
                    if (em != null)
                        s_maps.Add(objType, em);
                }
            }
        }

        public static EntityMap GetEntityMap(Type objType)
        {
            EntityMap em = null;
            s_maps.TryGetValue(objType, out em);
            if (em == null)
            {
                em = BuildEntityMap(objType);
                AddMap(objType, em);
            }
            return em;
        }
    }
}

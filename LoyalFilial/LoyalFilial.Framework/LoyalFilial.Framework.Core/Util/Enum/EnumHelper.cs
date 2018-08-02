using System;
using System.Reflection;

namespace LoyalFilial.Framework.Core.Util.Enum
{
    public static class EnumHelper
    {
        private static object _syncObj = new object();

        public static string GetDescription(System.Enum enumObj)
        {
            int enumValue = Convert.ToInt32(enumObj);
            EnumItem enumItem = EnumHelper.GetEnumItemByValue(enumObj.GetType(), enumValue);
            return enumItem != null ? enumItem.Description : string.Empty;
        }

        public static string GetKey(object value)
        {
            int enumValue = Convert.ToInt32(value);
            EnumItem enumItem = EnumHelper.GetEnumItemByValue(value.GetType(), enumValue);
            return enumItem != null ? enumItem.Key : "0";
        }

        public static T GetEnumByKey<T>(string key)
        {
            return (T)GetEnumByKey(typeof(T), key);
        }

        public static object GetEnumByKey(Type enumType, string key)
        {
            KeyedEnumItemCollection enumItems = GetKeyedEnumItems(enumType);
            EnumItem item = enumItems[key];
            return item == null ? enumItems.Default.Object : item.Object;
        }

        public static T GetEnumByValue<T>(int value)
        {
            return (T)GetEnumByValue(typeof(T), value);
        }

        public static object GetEnumByValue(Type enumType, int value)
        {
            ValuedEnumItemCollection enumItems = GetValuedEnumItems(enumType);
            EnumItem item = enumItems[value.ToString()];
            return item == null ? enumItems.Default.Object : item.Object;
        }

        public static EnumItem GetEnumItemByValue(Type enumType, int value)
        {
            ValuedEnumItemCollection enumItems = GetValuedEnumItems(enumType);
            EnumItem item = enumItems[value.ToString()];
            return item;
        }

        public static KeyedEnumItemCollection GetKeyedEnumItems(Type enumType)
        {
            KeyedEnumItemCollection enumItems = null;
            EnumTypeCacheItem cacheItem = null;
            if (EnsureCacheFilled(enumType, out cacheItem))
            {
                enumItems = cacheItem.KeyedEnumItems;
            }

            return enumItems;
        }

        public static ValuedEnumItemCollection GetValuedEnumItems(Type enumType)
        {
            ValuedEnumItemCollection enumItems = null;
            EnumTypeCacheItem cacheItem = null;
            if (EnsureCacheFilled(enumType, out cacheItem))
            {
                enumItems = cacheItem.ValuedEnumItems;
            }

            return enumItems;
        }

        internal static bool EnsureCacheFilled(Type enumType, out EnumTypeCacheItem cacheItem)
        {
            if (!EnumTypeCache.TryGetValue(enumType, out cacheItem))
            {
                lock (_syncObj)
                {
                    if (!EnumTypeCache.TryGetValue(enumType, out cacheItem))
                    {
                        if (enumType.IsEnum)
                        {
                            cacheItem = new EnumTypeCacheItem();
                            FieldInfo[] fields = enumType.GetFields();
                            foreach (FieldInfo field in fields)
                            {
                                if (field.FieldType.IsEnum)
                                {
                                    EnumItem enumItem = new EnumItem();
                                    enumItem.Name = field.Name;
                                    object enumItemObj = enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
                                    enumItem.Value = (int)enumItemObj;
                                    enumItem.Object = enumItemObj;                                    
                                    object[] itemAttributes = field.GetCustomAttributes(typeof(EnumItemAttribute), true);
                                    if (itemAttributes != null && itemAttributes.Length == 1)
                                    {
                                        EnumItemAttribute itemAttribute = itemAttributes[0] as EnumItemAttribute;
                                        if (itemAttribute != null)
                                        {
                                            enumItem.Key = string.IsNullOrWhiteSpace(itemAttribute.Key) ? enumItem.Name : itemAttribute.Key;
                                            enumItem.Description = string.IsNullOrWhiteSpace(itemAttribute.Description) ? enumItem.Name : itemAttribute.Description;
                                            enumItem.IsDefault = itemAttribute.IsDefault;
                                        }
                                    }
                                    else
                                    {
                                        enumItem.Key = enumItem.Name;
                                        enumItem.Description = enumItem.Name;
                                    }

                                    cacheItem.Add(enumItem);
                                }
                            }

                            EnumTypeCache.Add(enumType, cacheItem);
                        }
                    }
                }
            }

            return cacheItem != null;
        }
    }
}
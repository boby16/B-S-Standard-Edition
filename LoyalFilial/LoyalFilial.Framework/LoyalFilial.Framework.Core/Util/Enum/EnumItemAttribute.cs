using System;

namespace LoyalFilial.Framework.Core.Util.Enum
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class EnumItemAttribute : Attribute
    {
        public EnumItemAttribute()
            : this(string.Empty, string.Empty)
        {
        }

        public EnumItemAttribute(string key)
            : this(key, string.Empty)
        {
        }

        public EnumItemAttribute(string key, string description)
        {
            this.Key = key;
            this.Description = description;
            this.IsDefault = false;
        }

        public string Key { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
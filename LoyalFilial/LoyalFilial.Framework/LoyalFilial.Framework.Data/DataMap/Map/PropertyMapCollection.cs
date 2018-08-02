using System;
using System.Collections.ObjectModel;


namespace LoyalFilial.Framework.Data.DataMap.Map
{
    public class PropertyMapCollection : KeyedCollection<string, PropertyMap>
    {
        public PropertyMapCollection()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
            
        }

        protected override string GetKeyForItem(PropertyMap item)
        {
            return item.Key;
        }

        protected override void InsertItem(int index, PropertyMap item)
        {
            if (this.Contains(item.Key))
            {
                throw new ArgumentException(
                    string.Format("Duplicate Property Key:{0}.", item.Key),
                    "item");
            }

            base.InsertItem(index, item);
        }

        public new PropertyMap this[string key]
        {
            get
            {
                if (this.Contains(key))
                {
                    return base[key];
                }

                return null;
            }
        }
    }
}

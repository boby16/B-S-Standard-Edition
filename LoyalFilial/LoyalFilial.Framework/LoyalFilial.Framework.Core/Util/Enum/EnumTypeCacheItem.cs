namespace LoyalFilial.Framework.Core.Util.Enum
{
    internal class EnumTypeCacheItem
    {
        public EnumTypeCacheItem()
        {
            this.KeyedEnumItems = new KeyedEnumItemCollection();
            this.ValuedEnumItems = new ValuedEnumItemCollection();
        }

        public KeyedEnumItemCollection KeyedEnumItems { get; set; }
        public ValuedEnumItemCollection ValuedEnumItems { get; set; }

        public void Add(EnumItem enumItem)
        {
            this.KeyedEnumItems.Add(enumItem);
            this.ValuedEnumItems.Add(enumItem);
        }
    }
}
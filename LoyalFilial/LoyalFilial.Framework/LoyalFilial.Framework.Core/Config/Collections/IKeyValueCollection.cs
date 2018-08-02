using System.Collections;

namespace LoyalFilial.Framework.Core.Config.Collections
{
    public interface IKeyValueCollection : IList
    {
        ICollection Keys { get; }
        ICollection Values { get; }

        object GetValueByKey(object key);
    }
}

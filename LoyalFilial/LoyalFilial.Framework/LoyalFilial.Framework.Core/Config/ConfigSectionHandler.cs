using System.Configuration;
using System.Xml;

namespace LoyalFilial.Framework.Core.Config
{
    public class ConfigSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            if (section == null)
                return null;
            return new ConfigElement(null, (XmlElement)section);
        }

        #endregion
    }
}

using System.Xml;

namespace LoyalFilial.Framework.Core.Config
{
    public class ConfigElement:IConfigElement
    {
        private string _configFile;
        private XmlElement _xmlElement;

        #region IConfigElement 成员

        public bool IsXmlElementConfig
        {
            get { return _xmlElement != null; }
        }

        public string ConfigFile
        {
            get
            {
                return _configFile;
            }
            set
            {
                _configFile = value;
            }
        }

        public XmlElement XmlElement
        {
            get
            {
                return _xmlElement;
            }
            set
            {
                _xmlElement = value;
            }
        }

        #endregion

        #region Ctor.

        public ConfigElement(string configFile, XmlElement xmlElement)
        {
            _configFile = configFile;
            _xmlElement = xmlElement;
        }

        #endregion
    }
}

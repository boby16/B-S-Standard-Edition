using System;
using System.Xml.Linq;
using LoyalFilial.Framework.Core.Util;
using System.IO;

namespace LoyalFilial.Framework.Core.Config
{
    public class ConfigManager : IConfigManager
    {
        public const string FrameworkConfig = "Framework.Config";

        private IConfigElement _configElement = null;

        public IConfigElement ConfigElement
        {
            get { return _configElement; }
        }

        #region IConfigManager 成员

        public IConfigElement LoadConfig()
        {
            if (_configElement == null)
                _configElement = ConfigHelper.GetSectionFromConfiguration<ConfigElement>(ConfigManager.FrameworkConfig);

            XElement xElement = XmlHelper.ToXElement(_configElement.XmlElement);
            var configConnection = xElement.Element(Constants.Config_ConfigConnection).Value;
            var devFxId = xElement.Attribute(Constants.Config_DevFxId).Value;

            #region DB stored config

            /*
            var sql = string.Format(Constants.Config_SelectSql, devFxId);
            var xmlString = "";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(configConnection, CommandType.Text, sql))
            {
                while (sdr.Read())
                {
                    xmlString = sdr[Constants.Config_DB_ConfigColumnName].ToString();
                }
            }
            if (StringHelper.IsNullOrEmptyOrBlankString(xmlString))
                throw new Exception(Constants.Error_NoConfigInfo);
            _configElement.XmlElement = XmlHelper.ToXmlElement(xmlString);
            */

            #endregion

            var file = string.Format(Constants.Config_Module_Execution,
                                     AppDomain.CurrentDomain.SetupInformation.ApplicationBase, configConnection);
            if (!File.Exists(file))
            {
                file = string.Format(Constants.Config_Module_Execution_Web,
                                     AppDomain.CurrentDomain.SetupInformation.ApplicationBase, configConnection);
            }
            IConfigElement element = new ConfigElement(null, null);
            element.XmlElement = XmlHelper.ToXmlElement(file, true);
            return element;
        }

        public IConfigElement LoadConfig(string moduleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
using System.Xml;
using System.Xml.Linq;

namespace LoyalFilial.Framework.Core.Util
{
    public class XmlHelper
    {

        public static XmlElement ToXmlElement(string xmlString, bool isFilePath)
        {
            var doc = new XmlDocument();
            if (!isFilePath)
            {
                doc.LoadXml(xmlString);
            }
            else
            {
                doc.Load(xmlString);
            }
            if (doc == null)
                return null;
            return doc.DocumentElement;
        }

        /// <summary>
        /// Xml字符串转换为XmlElement
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static XmlElement ToXmlElement(string xmlString)
        {
            return ToXmlElement(xmlString, false);
        }

        public static string GetNodeValue(XmlNode config)
        {
            if (config != null)
            {
                if (config is XmlAttribute)
                {
                    return config.Value;
                }
                else if (config is XmlText)
                {
                    return config.Value;
                }
                else
                {
                    return config.InnerXml;
                }
            }
            return string.Empty;
        }

        #region XElement与XmlElement的转换

        /// <summary>  
        /// XElement转换为XmlElement  
        /// </summary>  
        public static XmlElement ToXmlElement(XElement xElement)
        {
            if (xElement == null) return null;

            XmlElement xmlElement = null;
            XmlReader xmlReader = null;
            try
            {
                xmlReader = xElement.CreateReader();
                var doc = new XmlDocument();
                xmlElement = doc.ReadNode(xElement.CreateReader()) as XmlElement;
            }
            catch
            {
            }
            finally
            {
                if (xmlReader != null) xmlReader.Close();
            }

            return xmlElement;
        }

        /// <summary>  
        /// XmlElement转换为XElement  
        /// </summary>  
        public static XElement ToXElement(XmlElement xmlElement)
        {
            if (xmlElement == null) return null;

            XElement xElement = null;
            try
            {
                var doc = new XmlDocument();
                doc.AppendChild(doc.ImportNode(xmlElement, true));
                xElement = XElement.Parse(doc.InnerXml);
            }
            catch { }

            return xElement;
        }

        #endregion
    }
}

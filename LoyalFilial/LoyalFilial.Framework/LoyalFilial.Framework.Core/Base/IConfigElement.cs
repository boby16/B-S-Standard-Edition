using System.Xml;

namespace LoyalFilial.Framework.Core
{
    public interface IConfigElement
    {
        /// <summary>
        /// 是否是XML节点配置
        /// </summary>
        bool IsXmlElementConfig { get; }

        /// <summary>
        /// 配置文件全称
        /// </summary>
        string ConfigFile { get; set; }

        /// <summary>
        /// XML配置节点内容
        /// </summary>
        XmlElement XmlElement { get; set; }
    }
}

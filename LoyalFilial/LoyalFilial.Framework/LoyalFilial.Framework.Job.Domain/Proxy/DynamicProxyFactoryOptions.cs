using System.Text;
namespace LoyalFilial.Framework.Job.Domain.Proxy
{
    public delegate string ProxyCodeModifier(string proxyCode);

    public class DynamicProxyFactoryOptions
    {
        public enum LanguageOptions { CS, VB };
        public enum FormatModeOptions { Auto, XmlSerializer, DataContractSerializer }

        private LanguageOptions lang;
        private FormatModeOptions mode;
        private ProxyCodeModifier codeModifier;

        public DynamicProxyFactoryOptions()
        {
            this.lang = LanguageOptions.CS;
            this.mode = FormatModeOptions.Auto;
            this.codeModifier = null;
        }

        public LanguageOptions Language
        {
            get
            {
                return this.lang;
            }

            set
            {
                this.lang = value;
            }
        }

        public FormatModeOptions FormatMode
        {
            get
            {
                return this.mode;
            }

            set
            {
                this.mode = value;
            }
        }

        public ProxyCodeModifier CodeModifier
        {
            get
            {
                return this.codeModifier;
            }

            set
            {
                this.codeModifier = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.SystemConstants.DynamicProxyFactoryOptions);
            sb.Append(Constants.SystemConstants.Language + Language);
            sb.Append(Constants.SystemConstants.FormatMode + FormatMode);
            sb.Append(Constants.SystemConstants.CodeModifier + CodeModifier);
            sb.Append(Constants.SystemConstants.EndString);

            return sb.ToString();
        }
    }
}

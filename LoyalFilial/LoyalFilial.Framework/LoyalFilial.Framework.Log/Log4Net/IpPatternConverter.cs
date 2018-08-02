using System.Configuration;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace LoyalFilial.Framework.Log
{
    internal sealed class IpPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            writer.Write(LoadLog4NetConfig.GetLocalIpAddress());
        }
    }

    internal sealed class AppIdPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            writer.Write(ConfigurationManager.AppSettings[LogConstants.AppId] ?? LogConstants.DefaultAppId);
        }
    }
}

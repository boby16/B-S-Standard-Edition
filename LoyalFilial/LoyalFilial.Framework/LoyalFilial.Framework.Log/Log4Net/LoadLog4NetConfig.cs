using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LoyalFilial.Framework.Log
{
    public class LoadLog4NetConfig
    {
        #region 加载Log4Net配置
        public static void LoadADONetAppender(string name, string bufferSize, string connectionType, string connectionString, string CommandText)
        {
            Hierarchy hier = LogManager.GetRepository() as Hierarchy;
            if (hier != null)
            {
                AdoNetAppender appender = new AdoNetAppender();
                appender.Name = name;
                appender.CommandType = CommandType.Text;
                appender.BufferSize = Convert.ToInt32(bufferSize);
                appender.ConnectionType = connectionType;
                appender.ConnectionString = connectionString;
                appender.CommandText = @CommandText;

                var appId = ConfigurationManager.AppSettings[LogConstants.AppId] ?? LogConstants.DefaultAppId;

                var localIpAddress = GetLocalIpAddress();

                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_AppId, DbType = DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new PatternLayout(appId)) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_Ip, DbType = DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new PatternLayout(localIpAddress)) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_LogDate, DbType = DbType.DateTime, Layout = new RawTimeStampLayout() });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_Thread, DbType = DbType.String, Size = 255, Layout = new Layout2RawLayoutAdapter(new PatternLayout(LogConstants.V_Thread)) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_Level, DbType = DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new PatternLayout(LogConstants.V_Level)) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_Logger, DbType = DbType.String, Size = 255, Layout = new Layout2RawLayoutAdapter(new PatternLayout(LogConstants.V_Logger)) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_Message, DbType = DbType.String, Size = 4000, Layout = new Layout2RawLayoutAdapter(new PatternLayout(LogConstants.V_Exception)) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = LogConstants.P_Exception, DbType = DbType.String, Size = 4000, Layout = new Layout2RawLayoutAdapter(new ExceptionLayout()) });
                appender.ActivateOptions();
                BasicConfigurator.Configure(appender);
            }
        }

        public static void LoadRollingFileAppender(string name, string appendToFile, string datePattern, string file, string maximumFileSize, string maxSizeRollBackups, string staticLogFileName, string conversionPattern)
        {
            RollingFileAppender appender = new RollingFileAppender();
            appender.AppendToFile = Convert.ToBoolean(appendToFile);
            appender.Name = name;
            appender.DatePattern = datePattern;
            appender.File = file;
            appender.RollingStyle = RollingFileAppender.RollingMode.Composite;
            appender.MaximumFileSize = maximumFileSize;
            appender.MaxSizeRollBackups = Convert.ToInt32(maxSizeRollBackups);
            appender.StaticLogFileName = Convert.ToBoolean(staticLogFileName);
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.AddConverter(LogConstants.Ip, typeof(IpPatternConverter));
            patternLayout.AddConverter(LogConstants.AppId, typeof(AppIdPatternConverter));
            patternLayout.ConversionPattern = conversionPattern;
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;
            //选择UTF8编码，确保中文不乱码。
            appender.Encoding = Encoding.UTF8;
            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
        }

        public static string GetLocalIpAddress()
        {
            var ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            var address = ipHostEntry.AddressList.FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork);
            var localIpAddress = address == null ? string.Empty : address.ToString();
            return localIpAddress;
        }

        #endregion
    }
}

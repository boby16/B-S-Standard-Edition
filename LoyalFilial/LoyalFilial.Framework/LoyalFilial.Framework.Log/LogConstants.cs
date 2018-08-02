namespace LoyalFilial.Framework.Log
{
    internal class LogConstants
    {
        #region Log4Net

        public const string DefaultAppId = "00000";
        public const string AppId = "AppId";
        public const string Appender = "Appender";
        public const string Name = "name";
        public const string Type = "type";
        public const string Value = "value";
        public const string Ip = "Ip";

        public const string L_Debug = "Debug";
        public const string L_Error = "Error";
        public const string L_Fatal = "Fatal";
        public const string L_Info = "Info";
        public const string L_Warn = "Warn";

        public const string Type_DataBase = "DataBase";
        public const string Type_RollingFile = "RollingFile";

        public const string E_BufferSize = "BufferSize";
        public const string E_ConnectionType = "ConnectionType";
        public const string E_ConnectionString = "ConnectionString";
        public const string E_CommandText = "CommandText";


        public const string Config_BufferSize = "1";
        public const string Config_ConnectionType = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";
        public const string Config_ConnectionString = "server = 192.168.1.52;database = biz_dev;user id = biz_dev;password = biz_dev117go;allow zero datetime=true;";
        public const string Config_CommandText = "INSERT INTO `logs` (`AppId`, `Ip`, `Date`, `Thread`, `Level`,  `Message`,`Exception`) VALUES (@log_appid, @log_ip, @log_date, @thread, @log_level, @message, @exception)";


        public const string E_AppendToFile = "AppendToFile";
        public const string E_DatePattern = "DatePattern";
        public const string E_File = "File";
        public const string E_MaximumFileSize = "MaximumFileSize";
        public const string E_MaxSizeRollBackups = "MaxSizeRollBackups";
        public const string E_StaticLogFileName = "StaticLogFileName";
        public const string E_ConversionPattern = "ConversionPattern";


        public const string Config_AppendToFile = "true";
        public const string Config_DatePattern = "yyyy-MM-dd HH'.log'";
        public const string Config_File = "Logs";
        public const string Config_MaximumFileSize = "10240kb";
        public const string Config_MaxSizeRollBackups = "1000";
        public const string Config_StaticLogFileName = "false";
        public const string Config_ConversionPattern = "%n【AppId】%AppId%n【服务器Ip】%Ip%n【记录时间】%date%n【等级】%level%n【描述】%message%n【线程编号】%thread%n【执行时间】%r毫秒%n【当前机器名】%property%n";

        public const string P_AppId = "@log_appid";
        public const string P_Ip = "@log_ip";
        public const string P_LogDate = "@log_date";
        public const string P_Thread = "@thread";
        public const string P_Level = "@log_level";
        public const string P_Logger = "@logger";
        public const string P_Message = "@message";
        public const string P_Exception = "@exception";

        public const string V_Thread = "%thread";
        public const string V_Level = "%level";
        public const string V_Logger = "%logger";
        public const string V_Exception = "%message";
        #endregion
    }
}

using System;
using System.Xml;
using System.Xml.Linq;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Log;
using LoyalFilial.Framework.Core.Util;
using log4net;
using LogManager = log4net.LogManager;

namespace LoyalFilial.Framework.Log
{
    public class Log4NetManager : TimerBase<LogMessage>, ILogManager
    {
        #region 变量定义

        //ILog对象
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region 定义信息二次处理方式

        protected override void OnTimer(LogMessage logMessage)
        {
            WriteLog(logMessage);
        }

        private void WriteLog(LogMessage logMessage)
        {
            switch (logMessage.Type)
            {
                case LogConstants.L_Debug:
                    if (logMessage.Exception == null)
                    {
                        if (log.IsDebugEnabled)
                        {
                            log.Debug(logMessage.Message);
                        }
                    }
                    else
                    {
                        if (log.IsDebugEnabled)
                        {
                            log.Debug(logMessage.Message, logMessage.Exception);
                        }
                    }
                    break;

                case LogConstants.L_Error:
                    if (logMessage.Exception == null)
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error(logMessage.Message);
                        }
                    }
                    else
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error(logMessage.Message, logMessage.Exception);
                        }
                    }
                    break;

                case LogConstants.L_Fatal:
                    if (logMessage.Exception == null)
                    {
                        if (log.IsFatalEnabled)
                        {
                            log.Fatal(logMessage.Message);
                        }
                    }
                    else
                    {
                        if (log.IsFatalEnabled)
                        {
                            log.Fatal(logMessage.Message, logMessage.Exception);
                        }
                    }
                    break;
                case LogConstants.L_Info:
                    if (logMessage.Exception == null)
                    {
                        if (log.IsInfoEnabled)
                        {
                            log.Info(logMessage.Message);
                        }
                    }
                    else
                    {
                        if (log.IsInfoEnabled)
                        {
                            log.Info(logMessage.Message, logMessage.Exception);
                        }
                    }
                    break;
                case LogConstants.L_Warn:
                    if (logMessage.Exception == null)
                    {
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(logMessage.Message);
                        }
                    }
                    else
                    {
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(logMessage.Message, logMessage.Exception);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region 封装Log4net

        public void Debug(object message)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = null, Type = LogConstants.L_Debug });
            this.StartTimer();
        }

        public void Debug(object message, Exception ex)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = ex, Type = LogConstants.L_Debug });
            this.StartTimer();
        }

        public void Error(object message)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = null, Type = LogConstants.L_Error });
            this.StartTimer();
        }

        public void Error(object message, Exception ex)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = ex, Type = LogConstants.L_Error });
            this.StartTimer();
        }

        public void Fatal(object message)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = null, Type = LogConstants.L_Fatal });
            this.StartTimer();
        }

        public void Fatal(object message, Exception ex)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = ex, Type = LogConstants.L_Fatal });
            this.StartTimer();
        }

        public void Info(object message)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = null, Type = LogConstants.L_Info });
            this.StartTimer();
        }

        public void Info(object message, Exception ex)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = ex, Type = LogConstants.L_Info });
            this.StartTimer();
        }


        public void Warn(object message)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = null, Type = LogConstants.L_Warn });
            this.StartTimer();
        }

        public void Warn(object message, Exception ex)
        {
            this.AddMessage(new LogMessage { Message = message, Exception = ex, Type = LogConstants.L_Warn });
            this.StartTimer();
        }

        public void Log(string log)
        {
            throw new NotImplementedException();
        }

        public void Log(string Level, string log)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IModule 成员
        public string Name { get; set; }

        public IFramework Framework
        {
            get;
            private set;
        }

        public bool Config(IFramework framework, IConfigElement configElement)
        {
            return this.Config(framework, configElement, true);
        }

        public bool RefreshCache()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IConfigurable 成员
        public bool ConfigInitialized
        {
            get;
            private set;
        }

        public bool Config(IConfigElement configElement)
        {
            return this.Config(this.Framework, configElement, false);
        }

        public bool Config(IConfigElement configElement, bool isForce)
        {
            return this.Config(this.Framework, configElement, isForce);
        }
        #endregion

        #region Config
        protected bool Config(IFramework framework, IConfigElement configElement, bool isForce)
        {
            this.Framework = framework;
            if (!this.ConfigInitialized || isForce)
            {
                Init(configElement.XmlElement);
                this.ConfigInitialized = true;
            }
            return true;
        }

        private bool Init(XmlElement xmlElement)
        {
            if (xmlElement == null)
                throw new Exception(string.Format(Constants.Error_NoConfigForModule, Constants.Module_Log_Name));
            try
            {
                XElement xElement = XmlHelper.ToXElement(xmlElement);
                foreach (XElement m in xElement.Elements(LogConstants.Appender))
                {
                    if (m != null)
                    {
                        var name = m.Attribute(LogConstants.Name).Value;
                        var type = m.Attribute(LogConstants.Type).Value;
                        if (type.ToUpper() == LogConstants.Type_DataBase.ToUpper())
                        {
                            var bufferSize = LogConstants.Config_BufferSize;
                            var bufferSizeConfig = m.Element(LogConstants.E_BufferSize);
                            if (bufferSizeConfig != null)
                                bufferSize = bufferSizeConfig.Attribute(LogConstants.Value).Value;

                            var connectionType = LogConstants.Config_ConnectionType;
                            var connectionTypeConfig = m.Element(LogConstants.E_ConnectionType);
                            if (connectionTypeConfig != null)
                                connectionType = connectionTypeConfig.Attribute(LogConstants.Value).Value;

                            var connectionString = LogConstants.Config_ConnectionString;
                            var connectionStringConfig = m.Element(LogConstants.E_ConnectionString);
                            if (connectionStringConfig != null)
                                connectionString = connectionStringConfig.Attribute(LogConstants.Value).Value;

                            var commandText = LogConstants.Config_CommandText;
                            var commandTextConfgi = m.Element(LogConstants.E_CommandText);
                            if (commandTextConfgi != null)
                                commandText = commandTextConfgi.Attribute(LogConstants.Value).Value;
                            LoadLog4NetConfig.LoadADONetAppender(name, bufferSize, connectionType, connectionString, commandText);
                        }
                        else if (type.ToUpper() == LogConstants.Type_RollingFile.ToUpper())
                        {
                            var appendToFile = LogConstants.Config_AppendToFile;
                            var appendToFileConfig = m.Element(LogConstants.E_AppendToFile);
                            if (appendToFileConfig != null)
                                appendToFile = appendToFileConfig.Attribute(LogConstants.Value).Value;

                            var datePattern = LogConstants.Config_DatePattern;
                            var datePatternConfig = m.Element(LogConstants.E_DatePattern);
                            if (datePatternConfig != null)
                                datePattern = datePatternConfig.Attribute(LogConstants.Value).Value;

                            var file = LogConstants.Config_File;
                            var fileConfig = m.Element(LogConstants.E_File);
                            if (fileConfig != null)
                                file = fileConfig.Attribute(LogConstants.Value).Value;

                            var maximumFileSize = LogConstants.Config_MaximumFileSize;
                            var maximumFileSizeConfig = m.Element(LogConstants.E_MaximumFileSize);
                            if (maximumFileSizeConfig != null)
                                maximumFileSize = maximumFileSizeConfig.Attribute(LogConstants.Value).Value;

                            var maxSizeRollBackups = LogConstants.Config_MaxSizeRollBackups;
                            var maxSizeRollBackupsConfig = m.Element(LogConstants.E_MaxSizeRollBackups);
                            if (maxSizeRollBackupsConfig != null)
                                maxSizeRollBackups = maxSizeRollBackupsConfig.Attribute(LogConstants.Value).Value;

                            var staticLogFileName = LogConstants.Config_StaticLogFileName;
                            var staticLogFileNameConfig = m.Element(LogConstants.E_StaticLogFileName);
                            if (staticLogFileNameConfig != null)
                                staticLogFileName = staticLogFileNameConfig.Attribute(LogConstants.Value).Value;

                            var conversionPattern = LogConstants.Config_ConversionPattern;
                            var conversionPatternConfig = m.Element(LogConstants.E_ConversionPattern);
                            if (conversionPatternConfig != null)
                                conversionPattern = conversionPatternConfig.Attribute(LogConstants.Value).Value;
                            LoadLog4NetConfig.LoadRollingFileAppender(name, appendToFile, datePattern, file, maximumFileSize, maxSizeRollBackups, staticLogFileName, conversionPattern);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Error_Data_InitConfigFailed, ex);
            }
        }
        #endregion

    }
}

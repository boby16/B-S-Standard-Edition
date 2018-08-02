using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using LoyalFilial.Framework.Core.Util;
using System.IO;

namespace LoyalFilial.Framework.Core.Log
{
    public class LogManager : ILogManager
    {
        #region Log Para

        private IFramework _framework;

        private string _directory = null;
        private bool _isConfigInitialized = false;
        private float _fileMaxValue = 20;
        private Dictionary<string, string> _levelList = null;
        private string _name;

        public Dictionary<string, string> LevelList
        {
            get { return _levelList; }
        }

        public float FileMaxValue
        {
            get { return _fileMaxValue; }
        }

        public string Directory
        {
            get { return _directory; }
        }

        #endregion

        #region Config

        public bool Config(IFramework framework, IConfigElement configElement, bool isForce)
        {
            _framework = framework;
            if (!_isConfigInitialized || isForce)
            {
                Init(configElement.XmlElement);
                _isConfigInitialized = true;
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
                _directory = xElement.Element(Constants.Log_Directory).Value;
                var maxSize = xElement.Element(Constants.Log_MaxSize).Value;
                _fileMaxValue = Convert.ToSingle(maxSize.ToLower().Replace(Constants.Log_MaxSize_Appendix, ""));
                foreach (XElement level in xElement.Elements(Constants.Log_Level))
                {
                    if (_levelList == null)
                        _levelList = new Dictionary<string, string>();
                    _levelList.Add(level.Attribute(Constants.Log_Level_Name).Value, level.Attribute(Constants.Log_Level_Folder).Value);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(Constants.Error_Log_InitConfigFailed, ex);
            }
        }

        #endregion

        #region Log File

        private string GetLogFile(string level)
        {
            string subFolder = null;
            if (_levelList != null && !StringHelper.IsNullOrEmptyOrBlankString(level))
            {
                _levelList.TryGetValue(level, out subFolder);
            }

            string fileName = null;
            if (!StringHelper.IsNullOrEmptyOrBlankString(subFolder))
            {
                fileName = string.Format("{0}\\{1}\\{2}.txt", _directory, subFolder, DateTime.Now.ToString("yyyy-MM-dd"));
                if (CompareFileLength(fileName, _fileMaxValue))
                    return string.Format("{0}\\{1}\\{2}.txt", _directory, subFolder, DateTime.Now.ToString("yyyy-MM-dd HH_mm_sss"));
                else
                    return fileName;
            }
            else
            {
                fileName = string.Format("{0}\\{1}.txt", _directory, DateTime.Now.ToString("yyyy-MM-dd"));
                if (CompareFileLength(fileName, _fileMaxValue))
                    return string.Format("{0}\\{1}.txt", _directory, DateTime.Now.ToString("yyyy-MM-dd HH_mm_sss"));
                else
                    return fileName;
            }
        }

        private bool CompareFileLength(string fileName, float m)
        {
            if (File.Exists(fileName))
            {
                FileInfo file = new FileInfo(fileName);
                return file.Length > m * 1024 * 1024;
            }
            return false;
        }

        #endregion

        #region IModule 成员

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public IFramework Framework
        {
            get { return _framework; }
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
            get { return _isConfigInitialized; }
        }

        public bool Config(IConfigElement configElement)
        {
            return this.Config(_framework, configElement, false);
        }

        public bool Config(IConfigElement configElement, bool isForce)
        {
            return this.Config(_framework, configElement, isForce);
        }

        #endregion

        #region ILogManager 成员

        public void Log(string log)
        {
            Log(null, log);
        }

        public void Log(string Level, string log)
        {
            var fileName = GetLogFile(Level);
            if (!FileHelper.IsPhysicalPath(fileName))
            {
                fileName = string.Format("{0}\\{1}", AppDomain.CurrentDomain.SetupInformation.ApplicationBase, fileName);
            }
            StringBuilder logBuilder = new StringBuilder();
            logBuilder.AppendLine(LogDecoration("Begin"));
            logBuilder.Append(log);
            logBuilder.AppendLine("");
            logBuilder.AppendLine(LogDecoration("End"));
            TextLogger.WriteLog(fileName, logBuilder.ToString());
        }

        public void Debug(object message) {
        }

        public void Debug(object message, Exception ex) {
        }

        public void Error(object message) {
        }

        public void Error(object message, Exception ex) {
        }

        public void Info(object message) {
        }

        public void Info(object message, Exception ex) {
        }

        private string LogDecoration(string input)
        {
            return string.Format("=================================================={0} {1}==================================================",
                StringHelper.DataTimeLongString(DateTime.Now), input);
        }

        #endregion

        #region Stack info

        private string GetExecutionStackTrace()
        {
            StackTrace t = new StackTrace(true);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            foreach (var f in t.GetFrames())
            {
                var fileName = f.GetFileName();
                if (fileName != "" && fileName != null)
                {
                    sb.AppendLine(string.Format("{0} in {1}: Line:{2} Column:{3}", f.GetMethod().ToString(), fileName, f.GetFileLineNumber().ToString(), f.GetFileColumnNumber().ToString()));
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Xml.Linq;
using LoyalFilial.Framework.Core;
using LoyalFilial.Framework.Core.Log;
using LoyalFilial.Framework.Core.Mail;
using LoyalFilial.Framework.Core.Util;
using LoyalFilial.Framework.Mail.Annotations;

namespace LoyalFilial.Framework.Mail
{
    /// <summary>
    /// MailManager implementation
    /// </summary>
    public class MailManager : IMailManager
    {
        #region Imp IMail

        void IMailManager.Send(MailMessage message)
        {
            this.MailServiceInstance.SendInternal(true, message);
        }

        void IMailManager.Send(bool queued, MailMessage message)
        {
            this.MailServiceInstance.SendInternal(queued, message);
        }

        void IMailManager.Send(string from, string to, string subject, string messageText, bool isHtmlMail = false)
        {
            this.Send(true, from, to, string.Empty, subject, messageText, isHtmlMail);
        }

        public void Send(string from, string to, string cc, string subject, string messageText, bool isHtmlMail = false)
        {
            this.Send(true, from, to, cc, subject, messageText, isHtmlMail);
        }

        void IMailManager.Send(bool queued, string from, string to, string subject, string messageText, bool isHtmlMail = false)
        {
            this.Send(queued, from, to, string.Empty, subject, messageText, isHtmlMail);
        }

        public void Send(bool queued, string from, string to, string cc, string subject, string messageText, bool isHtmlMail = false)
        {
            var message = new MailMessage
                {
                    From = new MailAddress(from, this.config.DisplayName),
                    Subject = subject,
                    Body = messageText
                };

            if (!string.IsNullOrEmpty(cc))
            {
                var ccs = cc.Split(MailConstants.Semicolon);
                if (ccs.Length > 0)
                {
                    foreach (var c in ccs)
                    {
                        if (!string.IsNullOrEmpty(c))
                            message.CC.Add(c);
                    }
                }
            }

            if (!string.IsNullOrEmpty(to))
            {
                var tos = to.Split(MailConstants.Semicolon);
                if (tos.Length > 0)
                {
                    foreach (var t in tos)
                    {
                        if (!string.IsNullOrEmpty(t))
                            message.To.Add(t);
                    }
                }
            }
            message.IsBodyHtml = isHtmlMail;

            this.MailServiceInstance.SendInternal(queued, message);
        }

        #endregion

        #region send mail

        /// <summary>
        /// 发送邮件实例
        /// </summary>
        protected static MailServiceInternal mailServiceInternal;

        protected MailServiceInternal MailServiceInstance
        {
            get { return mailServiceInternal ?? new MailServiceInternal { LogManager = null, Config = config }; }
        }

        /// <summary>
        /// 邮件配置
        /// </summary>
        private MailConfig config = null;

        protected class MailServiceInternal : TimerBase<MailMessage>
        {
            protected override int Interval
            {
                get { return this.Config.Interval; }
            }

            protected override void OnTimer(MailMessage message)
            {
                var smtp = GetSmtpClient();
                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    if (this.LogManager != null)
                    {
                        var title = message.Subject;
                        var to = message.To.ToString();
                        this.LogManager.Error(string.Format("Mail Send Failed\r\nTitle: {0}\r\nTo: {1}\r\nException:\r\n{2}", title, to, ex), ex);
                    }
                }
            }

            /// <summary>
            /// 日志服务
            /// </summary>
            protected internal ILogManager LogManager { get; set; }

            /// <summary>
            /// 邮件配置
            /// </summary>
            protected internal MailConfig Config { get; set; }

            /// <summary>
            /// 发送邮件
            /// </summary>
            /// <param name="queued">是否缓存邮件（提高程序响应速度）</param>
            /// <param name="message">MailMessage实体</param>
            protected internal virtual void SendInternal(bool queued, MailMessage message)
            {
                if (queued)
                {
                    this.AddMessage(message);
                    this.StartTimer();
                }
                else
                {
                    GetSmtpClient().Send(message);
                }
            }

            protected virtual SmtpClient GetSmtpClient()
            {
                var server = this.Config.SmtpServer;
                var port = this.Config.ServerPort;
                var userName = this.Config.UserName;
                var password = this.Config.Password;
                var smtp = string.IsNullOrEmpty(server) ? new SmtpClient() : port > 0 ? new SmtpClient(server, port) : new SmtpClient(server);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    smtp.Credentials = new NetworkCredential(userName, password);
                }
                else if (!string.IsNullOrEmpty(server))
                {
                    smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                }

                return smtp;
            }
        }
        #endregion

        #region IConfigurable 成员
        public bool ConfigInitialized { get; private set; }

        public bool Config(IConfigElement configElement)
        {
            return this.Config(this.Framework, configElement, false);
        }

        public bool Config(IConfigElement configElement, bool isForce)
        {
            return this.Config(this.Framework, configElement, isForce);
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
                if (xElement != null)
                {
                    var server = MailConstants.Config_SmtpServer;
                    var serverConfig = xElement.Element(MailConstants.SmtpServer);
                    if (serverConfig != null)
                        server = serverConfig.Attribute(MailConstants.Value).Value;

                    var port = MailConstants.Config_ServerPort;
                    var portConfig = xElement.Element(MailConstants.ServerPort);
                    if (portConfig != null)
                        port = portConfig.Attribute(MailConstants.Value).Value;

                    var userName = MailConstants.Config_UserName;
                    var userNameConfig = xElement.Element(MailConstants.UserName);
                    if (userNameConfig != null)
                        userName = userNameConfig.Attribute(MailConstants.Value).Value;

                    var password = MailConstants.Config_Password;
                    var passwordConfig = xElement.Element(MailConstants.Password);
                    if (passwordConfig != null)
                        password = passwordConfig.Attribute(MailConstants.Value).Value;

                    var interval = MailConstants.Config_Interval;
                    var intervalConfig = xElement.Element(MailConstants.Interval);
                    if (intervalConfig != null)
                        interval = intervalConfig.Attribute(MailConstants.Value).Value;

                    var displayName = MailConstants.Config_DisplayName;
                    var displayNameConfig = xElement.Element(MailConstants.DisplayName);
                    if (displayNameConfig != null)
                        displayName = displayNameConfig.Attribute(MailConstants.Value).Value;
                    config = new MailConfig(server, port, userName, password, interval, displayName);
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

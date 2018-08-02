using System;

namespace LoyalFilial.Framework.Mail
{
    public class MailConfig
    {
        public MailConfig(string smtpServer, string serverPort, string userName, string password, string interval, string displayName)
        {
            this.SmtpServer = smtpServer;
            this.ServerPort = string.IsNullOrEmpty(serverPort) ? 0 : Convert.ToInt32(serverPort);
            this.UserName = userName;
            this.Password = password;
            this.Interval = string.IsNullOrEmpty(interval) ? 0 : Convert.ToInt32(interval);
            this.DisplayName = displayName;
        }

        /// <summary>
        /// SMTP服务器地址
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// SMTP服务器侦听端口
        /// </summary>

        public int ServerPort { get; set; }

        /// <summary>
        /// 认证用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 认证用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 缓存发送时定时器间隔时间（毫秒）
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}

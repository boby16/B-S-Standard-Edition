using System.Net.Mail;

namespace LoyalFilial.Framework.Core.Mail
{
    public interface IMailManager : IModule
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">MailMessage实体</param>
        void Send(MailMessage message);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="queued">是否缓存邮件（提高程序响应速度）</param>
        /// <param name="message">MailMessage实体</param>
        void Send(bool queued, MailMessage message);


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发送者地址</param>
        /// <param name="to">接收者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="messageText">邮件内容</param>
        /// <param name="isHtmlMail">是否html邮件</param>
        void Send(string from, string to, string subject, string messageText, bool isHtmlMail = false);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发送者地址</param>
        /// <param name="to">接收者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="cc">抄送者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="messageText">邮件内容</param>
        /// <param name="isHtmlMail">是否html邮件</param>
        void Send(string from, string to, string cc, string subject, string messageText, bool isHtmlMail = false);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="queued">是否缓存邮件（提高程序响应速度）</param>
        /// <param name="from">发送者地址</param>
        /// <param name="to">接收者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="messageText">邮件内容</param>
        /// <param name="isHtmlMail">是否html邮件</param>
        void Send(bool queued, string from, string to, string subject, string messageText, bool isHtmlMail = false);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="queued">是否缓存邮件（提高程序响应速度）</param>
        /// <param name="from">发送者地址</param>
        /// <param name="to">接收者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="cc">抄送者地址（可填多个地址，用英文分号“;”分割）</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="messageText">邮件内容</param>
        /// <param name="isHtmlMail">是否html邮件</param>
        void Send(bool queued, string from, string to, string cc, string subject, string messageText, bool isHtmlMail = false);
    }
}

using System;

namespace LoyalFilial.Framework.Core.Log
{
    public interface ILogManager : IModule
    {
        ///// <summary>
        ///// 将传入字符串记录日志,Log文件记录在日志根目录下。
        ///// </summary>
        ///// <param name="log">需要记录的字符串</param>
        void Log(string log);

        ///// <summary>
        ///// 根据指定的Level，记录日志字符串；Log文件记录在日志根目录/Level下。
        ///// </summary>
        ///// <param name="Level">日志分类级别：Info/Error/Exception</param>
        ///// <param name="log">需要记录的字符串</param>
        void Log(string Level, string log);

        void Debug(object message);
        void Debug(object message, Exception ex);
        void Error(object message);
        void Error(object message, Exception ex);
        void Info(object message);
        void Info(object message, Exception ex);

    }
}

using LoyalFilial.Common;
using LoyalFilial.Entity;
using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.BizCommon
{
    public class LogManager
    {
        public static void WriteErrorLogAsyn(string msg, string title="未知异常")
        {
            new Task(() =>
            {
                var log = new LogDO()
                {
                    Message = msg,
                    LogTime = DateTime.Now,
                    Level = "ERROR",
                    Title = title,
                    Ip = SecurityHelper.GetWebClientIp(),
                };
                LFFK.DataManager.Insert<LogDO>(log);
            });
        }

        public static void WriteErrorLogAsyn(Exception ex, string title = "未知异常")
        {
            new Task(() =>
            {
                var log = new LogDO()
                {
                    Message = ex.Message,
                    Exception = ex.Message + ex.Source + ex.StackTrace,
                    LogTime = DateTime.Now,
                    Level = "ERROR",
                    Title = title,
                    Ip = SecurityHelper.GetWebClientIp(),
                };
                LFFK.DataManager.Insert<LogDO>(log);
            });
        }
        public static void WriteInfoLogAsyn(string msg, string title = "日志记录")
        {
            new Task(() =>
            {
                var log = new LogDO()
                {
                    Message = msg,
                    LogTime = DateTime.Now,
                    Level = "INFO",
                    Title = title,
                    Ip = SecurityHelper.GetWebClientIp(),
                };
                LFFK.DataManager.Insert<LogDO>(log);
            });
        }

        public static void WriteInfoLogAsyn(Exception ex, string title = "日志记录")
        {
            new Task(() =>
            {
                var log = new LogDO()
                {
                    Message = ex.Message,
                    Exception = ex.Message + ex.Source + ex.StackTrace,
                    LogTime = DateTime.Now,
                    Level = "INFO",
                    Ip = SecurityHelper.GetWebClientIp(),
                    Title = title,
                };
                LFFK.DataManager.Insert<LogDO>(log);
            });
        }

        public static void WriteLogAsyn(LogDO log)
        {
            new Task(() =>
            {
                log.LogTime = DateTime.Now;
                log.Ip = SecurityHelper.GetWebClientIp();
                LFFK.DataManager.Insert<LogDO>(log);
            });
        }
    }
}

using System;

namespace LoyalFilial.Framework.Log
{
    public class LogMessage
    {
        public string Type { get; set; }
        public object Message { get; set; }
        public Exception Exception { get; set; }
    }
}

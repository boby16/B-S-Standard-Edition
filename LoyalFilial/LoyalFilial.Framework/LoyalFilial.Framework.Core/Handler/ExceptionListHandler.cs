using System;
using System.Text;
using System.Web;

namespace LoyalFilial.Framework.Core.Handler
{
    public class ExceptionListHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var exceptionList = LFFK.ExceptionList;
            if (exceptionList != null && exceptionList.Count > 0)
            {
                StringBuilder sbException = new StringBuilder();
                sbException.AppendLine(Constants.DevFx_ExceptionList_Tip);
                foreach (Exception ex in exceptionList)
                {
                    sbException.AppendLine("<br/>");
                    sbException.AppendLine(ex.ToString());
                }
                context.Response.Write(sbException.ToString());
                context.Response.End();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyalFilial.Framework.Core.Handler
{
    public class CacheRefreshHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var module = context.Request.Params[Constants.DevFx_Refresh_ModulePara];
            List<IModule> moduleList;
            if (module == null || module == Constants.DevFx_Refresh_ModuleAll)
            {
                moduleList = LFFK.ModuleList;
            }
            else
            {
                var moduleNameList = module.Split(new char[] { ';' });
                moduleList = LFFK.ModuleList.Where(m => (moduleNameList.Contains(m.Name))).ToList();
            }
            if (moduleList != null)
            {
                foreach (IModule moduleInst in moduleList)
                {
                    if (moduleInst != null)
                    {
                        try
                        {
                            moduleInst.RefreshCache();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            context.Response.Write(true.ToString());
            context.Response.End();
        }

        #endregion
    }
}

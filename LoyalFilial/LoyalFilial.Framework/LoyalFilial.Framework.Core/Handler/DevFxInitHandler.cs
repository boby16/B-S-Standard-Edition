using System.Linq;
using System.Web;

namespace LoyalFilial.Framework.Core.Handler
{
    public class DevFxInitHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //TODO: request security (IP)
            var module = context.Request.Params[Constants.DevFx_Refresh_ModulePara];
            if (module == null || module == Constants.DevFx_Refresh_ModuleAll)
            {
                LFFK.Init(true);
            }
            else
            {
                var moduleNameList = module.Split(new char[] { ';' });
                var moduleList = LFFK.ModuleList.Where(m => moduleNameList.Contains(m.Name)).ToList();
                foreach (IModule moduleInst in moduleList)
                {
                    if (moduleInst != null)
                    {
                        try
                        {
                            moduleInst.Config(LFFK.CurrentConfigManager.LoadConfig(moduleInst.Name), true);
                        }
                        catch { continue; }
                    }
                }
            }
            context.Response.Write(true.ToString());
            context.Response.End();
        }

        #endregion
    }
}

using System;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace LoyalFilial.Framework.Core.Config
{
    public class ConfigHelper
    {
        public static T GetSectionFromConfiguration<T>(params string[] sectionNames)
          where T : class
        {
            T section = null;
            if (sectionNames != null && sectionNames.Length > 0)
            {
                var ishttp = HttpContext.Current != null;
                foreach (var sectionName in sectionNames)
                {
                    if (ishttp)
                    {
                        section = WebConfigurationManager.GetSection(sectionName) as T;
                    }
                    else
                    {
                        try
                        {
                            section = ConfigurationManager.GetSection(sectionName) as T;
                        }
                        catch (Exception e)
                        {

                            throw new Exception();
                        }

                    }
                    if (section != null)
                    { break; }
                }

            }
            return section;
        }
    }
}

using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoyalFilial.Framework.Test.WebSample
{
    public partial class RSFKInit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tbInit_Click(object sender, EventArgs e)
        {
            LFFK.Init(true);
        }
    }
}
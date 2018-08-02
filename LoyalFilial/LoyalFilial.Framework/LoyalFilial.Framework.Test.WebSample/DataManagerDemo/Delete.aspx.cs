using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample.DataManagerDemo
{
    public partial class Delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Query a entity
            var entity = LFFK.DataManager.TableQuery<CqsEntity2>().Select().From().Where(t => t.No == 21).Execute();
            if (entity != null)
            {
                //update
                var result = LFFK.DataManager.Delete(entity);
                Response.Write(result.IsSucceed);
            }
        }
    }
}
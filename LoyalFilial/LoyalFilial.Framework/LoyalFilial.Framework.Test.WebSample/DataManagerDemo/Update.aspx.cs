using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample.DataManagerDemo
{
    public partial class Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Query a entity
            var entity = LFFK.DataManager.TableQuery<CqsEntity2>().Select().From().Where(t => t.Name == "方世玉").Execute();
            if (entity != null)
            {
                //Update entity values
                entity.Age += 10;

                //update
                var result = LFFK.DataManager.Update(entity);
                Response.Write(result.IsSucceed);
            }
        }
    }
}
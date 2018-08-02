using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample.DataManagerDemo
{
    public partial class InsertDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            CqsEntity entity = new CqsEntity
                {
                    Age = 100,
                    BoardTime = DateTime.Now,
                    Description = "Description",
                    Name = "Name",
                    OP = "Op",
                    UpdateTime = DateTime.Now
                };

            var result = LFFK.DataManager.Insert(entity);
            Response.Write("Insert return IdentityRowNo:" + result.IdentityRowNo);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample.DataManagerDemo
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnQueryAll_Click(object sender, EventArgs e)
        {
            //查询所有数据
            var list = LFFK.DataManager.TableQuery<CqsEntity>().Select().From().ExecuteList();

            if (list != null && list.Count > 0)
                this.lblResult.Text = "Total count:" + list.Count.ToString();
            else
            {
                this.lblResult.Text = "No Data";
            }

        }

        protected void btnQueryByName_Click(object sender, EventArgs e)
        {
            var name = this.txtName.Text;
            if (!string.IsNullOrEmpty(name))
            {
                //根据姓名查询一条数据
                var entity = LFFK.DataManager.TableQuery<CqsEntity>().Select().From().Where(t => t.Name == name).Execute();

                if (entity != null)
                    this.lblResult1.Text = "Age is:" + entity.Age;
            }
            else
            {
                this.lblResult1.Text = "Please input name";
            }
        }

        protected void btnQueryPage_Click(object sender, EventArgs e)
        {
            var pageIndex = this.txtPageIndex.Text;
            var pageSize = this.txtPageSize.Text;
            if (!string.IsNullOrEmpty(pageIndex) && !string.IsNullOrEmpty(pageSize))
            {
                int totalCount;
                //查询分页
                var list = LFFK.DataManager.TableQuery<CqsEntity>().Select().From().Where().OrderByDesc().ExecuteList(Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), out totalCount);
                if (list != null && list.Count > 0)
                    this.lblResult2.Text = "Totalcount:" + totalCount;
                else
                {
                    this.lblResult2.Text = "No Data";
                }
            }
            else
            {
                this.lblResult2.Text = "Please input";
            }
        }
    }
}
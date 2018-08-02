using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.Framework.Test.WebSample.LogManagerDemo
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLog_Click(object sender, EventArgs e)
        {

            /*
             * 日志提供了三种级别的
             * 请根据不同的场景调用不同的方法
             */


            LFFK.LogManager.Debug("Test");
            LFFK.LogManager.Debug("Test", new Exception("Test"));


            LFFK.LogManager.Info("Test");
            LFFK.LogManager.Info("Test", new Exception("Test"));


            LFFK.LogManager.Error("Test");
            LFFK.LogManager.Error("Test", new Exception("Test"));
        }
    }
}
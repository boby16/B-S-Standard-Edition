using System;
using System.Collections.Generic;
using System.IO; 
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using LoyalFilial.Entity;
using LoyalFilial.BL;
using LoyalFilial.APIService.Com;
using LoyalFilial.Common;
using LoyalFilial.Framework.Core;

namespace LoyalFilial.APIService.SMS
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SMSReceiver : ISMSReceiver
    {
        public Stream SMSStateNotify(string name, string pwd, string sendid, string time, string mobile, string state)
        {
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(""));
        }

        public Stream ReceiveSMS(string name, string pwd, string args)
        {
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(""));
        }
    }
}

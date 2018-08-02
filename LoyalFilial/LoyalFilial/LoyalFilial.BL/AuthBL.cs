using LoyalFilial.DA;
using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.BL
{
    public class AuthBL
    {
        public bool GenerateSMSAuthCode(string phoneNo)
        {
            return DAManager.AuthDAManager.GenerateSMSAuthCode(phoneNo);
        }

        public IActResult Login(string accountId, string password, string ip, string token)
        {
            return DAManager.AuthDAManager.Login(accountId, password, ip, token);
        }
        public IActResult LoginOut(string accountId, string ip)
        {
            return DAManager.AuthDAManager.LoginOut(accountId, ip);
        }
        public bool CheckLoginUser(string accountId, string token)
        {
            return DAManager.AuthDAManager.CheckLoginUser(accountId, token);
        }

        public IActResult Register(string accountId, long mobileNo, string password, int authCode, int userType)
        {
            return DAManager.AuthDAManager.Register(accountId,mobileNo, password, authCode, userType);
        }

        public bool SendCustSMS(string phoneNo, string content)
        {
            return DAManager.AuthDAManager.SendCustSMS(phoneNo, content);
        }
    }
}

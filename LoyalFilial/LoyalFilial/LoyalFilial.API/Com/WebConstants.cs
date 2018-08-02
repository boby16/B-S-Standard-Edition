using LoyalFilial.Framework.Core;
using System;

namespace LoyalFilial.APIService.Com
{
    public class WebConstants
    {
        #region config
        public const string AppUserMemoryCache = "AppUserMemoryCache_{0}";

        public const string ImgAuthCode_RequestCode = "VendorAppImageCheck";
        public const string ImgAuthCode_Key = "";

        public const string CommonFunctionValue = "({0})";
        public const string JsonpCallbackFunction = "josnpCallbackFunctionInWCF";
         
        public const int TimeElaps = 1000;
        public const string CommonVersionConfigNameForApp = "CommonVersionConfigNameForApp";
        #endregion

        #region user info
        public const string Const_User_Login_Error_ForUser = "账户不存在或密码错误!";
        public const string Const_User_Login_Error_ForUserImage = "验证码错误!";
        public const string Const_User_Login_Error_ForUserEmail = "邮箱不存在!";
        public const string Const_User_Login_Error_ForUserSuccess = "新密码已经通过Email形式发送至您的邮箱内，请登录后尽快修改。登录返回中......";
        
                
        public const string Config_OrderMailSender = "OrderConfig.MailSender";
        public const string Const_User_Register_Success = "提交申请成功,我们的工作人员会尽快与您取得联系!";
        #endregion

        #region product info
        public const string Const_User_Product_Error_ForNumber = "产品销售排行加载失败!";
        public const string Const_User_Product_Error_ForMoney = "产品销售金额加载失败!";
        public const string Const_User_Product_Error_ForPersonalMoney = "产品客单价/人均额加载失败!";
        #endregion

        #region order info
        public const string Const_User_Order_Error_ForDetail = "订单详细信息加载失败";
        public const string Const_User_Order_Error_ForList = "订单列表信息加载失败";
        #endregion

        #region notification info
        public const string Const_User_Notification_Error_ForDetail = "公告详细信息加载失败";
        public const string Const_User_Notification_Error_ForList = "公告列表信息加载失败";
        #endregion

        #region order couriercorp
        public const string Const_User_Order_Error_ForCouriercorp = "快递公司加载失败";
        #endregion

        
    }
}
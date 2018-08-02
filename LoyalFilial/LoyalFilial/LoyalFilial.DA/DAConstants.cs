using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.DA
{
    public class DAConstants
    {
        public const string Request_HTTP_POST = "POST";
        public const string Request_ContentType = "application/x-www-form-urlencoded";
        public const string Request_Code = "utf-8";
        public const string SMS_Url = "http://sms.1xinxi.cn/asmx/smsservice.aspx";
        public const string SMS_Params = "name=13817543398&pwd=836BF65FC28CA2E0B13AE7232E00&content={0}&mobile={1}&stime=&sign=1234【京东】&type=pt&extno=";

        public const string Sql_Insert_AuthCode = @"INSERT INTO auth_authcode (MobileNo,AuthCode) 
VALUES ({0},{1})  ON DUPLICATE KEY UPDATE AuthCode={1}; ";

        public const string Sql_Update_UserTargetId = "UPDATE auth_user SET TargetId={0} WHERE UserId='{1}'";

        public const string Sql_Select_CarPartsList = @"SELECT CarPartsId,CarPartsName, Address,MainServices 
            FROM b_carparts WHERE CityName='{0}'";
        public const string Sql_Select_CarPartsList_Cond = " AND MainServices LIKE '%{0}%'";

        public const string Sql_Insert_AuthLogin = @"INSERT INTO auth_login (UserId,LoginIP,LoginTime,Token, ExpiredDate) 
VALUES ('{0}','{1}','{2}','{3}','{4}') ON DUPLICATE KEY UPDATE LoginIP='{1}',LoginTime='{2}',Token='{3}',ExpiredDate='{4}' ";

        public const string Sql_Update_RetryTimes0 = @"Update Auth_User SET ReTryTimes=0 WHERE UserId='{0}'";

        public const string Sql_Update_RetryTimes = @"Update Auth_User SET ReTryTimes=(ReTryTimes+1) WHERE UserId='{0}'";

        public const string Sql_Insert_Favorite = @"INSERT INTO Favorites (UserId,TargetType,TargetId) VALUES ('{0}',{1},{2})";


        public const string Sql_Select_MFCustomerList = @"SELECT MFCustomerId,CustormerName, Address,CarBrand,CarType,MobileNo,BirthDay,State 
            FROM f_customer WHERE MaintainFactoryId={0}";
        public const string Sql_Select_MFCustomerList_Cond_CName = " AND CustormerName LIKE '%{0}%'";
        public const string Sql_Select_MFCustomerList_Cond_CMobile = " AND MobileNo LIKE '%{0}%'";
        public const string Sql_Select_MFCustomerList_Cond_CType = " AND State={0}";

        public const string Sql_Select_MFMaintainList = @"SELECT a.MFMaintainId,b.CustormerName,b.CarBrand,b.CarType,b.MobileNo,b.BirthDay,
a.ServiceItem,a.RemindDate,a.MaintainDate
FROM f_maintain a
INNER JOIN f_customer b on b.MFCustomerId=a.MFCustomerId
WHERE a.MaintainFactoryId={0} AND a.State=1";
        public const string Sql_Select_MFMaintainList_Cond_StartDate = " AND a.RemindDate>='{0}'";
        public const string Sql_Select_MFMaintainList_Cond_EndDate = " AND a.RemindDate<='{0}'";

        public const string Sql_Select_MFReserveList = @"SELECT a.MFReserveId,b.CustormerName,b.CarBrand,b.CarType,b.BirthDay,
a.ServiceItem,a.MobileNo,a.ReservedDate,a.State
FROM f_reserved a
INNER JOIN f_customer b on b.MFCustomerId=a.MFCustomerId
WHERE a.MaintainFactoryId={0}";
        public const string Sql_Select_MFReserveList_Cond_StartDate = " AND a.ReservedDate>='{0}'";
        public const string Sql_Select_MFReserveList_Cond_EndDate = " AND a.ReservedDate<='{0}'";


        public const string Sql_Select_InquiryList = @"SELECT a.*,(select count(1) from p_quotation where InquiryId=a.InquiryId) as QuotationCount
FROM f_inquiry a
WHERE a.MaintainFactoryId={0}";
        public const string Sql_Select_InquiryList_Cond_StartDate = " AND a.InquiryDate>='{0}'";
        public const string Sql_Select_InquiryList_Cond_EndDate = " AND a.InquiryDate<='{0}'";
        public const string Sql_Select_InquiryList_Cond_Id = " AND a.InquiryId={0}";
        public const string Sql_Select_InquiryList_Cond_State = " AND a.State={0}";

        public const string Sql_Select_InquiryQuotationList = @"SELECT a.InquiryId,a.InquiryDate,a.CarBrand,a.CarType,a.VIN,a.PartsImage,a.Quantity,
a.Remark,a.State as InquiryState,a.PurchaseId,b.QuotationId,b.CarPartsId,c.CarPartsName,b.Price
FROM f_inquiry a
INNER JOIN p_quotation b on a.InquiryId=b.InquiryId
INNER JOIN b_carparts c on c.CarPartsId=b.CarPartsId
WHERE a.State>=1 AND a.MaintainFactoryId={0} AND a.InquiryId={1}";


        public const string Sql_Select_QuotationList = @"SELECT a.InquiryId,a.InquiryDate,a.MaintainFactoryId,a.CarBrand,a.CarType,a.VIN,a.MaintainListImage,a.PartsImage,a.PartsName,a.Quantity,
a.Remark,a.Alliance,a.CarPartsId,a.PlanDeliveryDate,a.State as InquiryState,a.IsAnonymity,a.ToCarPartsType,(select count(1) from p_quotation where InquiryId=a.InquiryId) as QuotationCount,b.Price
FROM f_inquiry a
LEFT JOIN p_quotation b on a.InquiryId=b.InquiryId AND b.CarPartsId={0}
WHERE (a.Alliance IS NULL OR a.Alliance='' OR FIND_IN_SET('{1}',a.Alliance)>0 )
AND (a.CarPartsId IS NULL OR a.CarPartsId='' OR FIND_IN_SET('{0}',a.CarPartsId)>0) 
AND FIND_IN_SET(a.PartsName,'{2}')>=0 AND a.State>=1";
        public const string Sql_Select_QuotationList_Cond_StartDate = " AND b.QuotationDate>='{0}'";
        public const string Sql_Select_QuotationList_Cond_EndDate = " AND b.QuotationDate<='{0}'";
        public const string Sql_Select_QuotationList_Cond_InqId = " AND a.InquiryId={0}";
        public const string Sql_Select_QuotationList_Cond_State0 = " AND b.QuotationId IS NULL";
        public const string Sql_Select_QuotationList_Cond_State1 = " AND b.QuotationId > 0";
        public const string Sql_Select_QuotationList_Cond_PartsName = " AND a.PartsName like '%{0}%'";


        public const string Sql_Select_PurchaseList = @"SELECT a.PurchaseId,a.PurchaseDate,a.Price,a.Quantity,a.Amount,a.Remark,a.CarPartsId,c.CarPartsName, b.*
FROM f_purchase a
INNER JOIN f_inquiry b on a.PurchaseId = b.PurchaseId
INNER JOIN b_carparts c on c.CarPartsId=a.CarPartsId
WHERE a.MaintainFactoryId={0}";
        public const string Sql_Select_PurchaseList_Cond_StartDate = " AND a.PurchaseDate>='{0}'";
        public const string Sql_Select_PurchaseList_Cond_EndDate = " AND a.PurchaseDate<='{0}'";
        public const string Sql_Select_PurchaseList_Cond_Id = " AND a.PurchaseId={0}";
        public const string Sql_Select_PurchaseList_Cond_State = " AND a.State={0}";


        public const string Sql_Select_OrderList = @"SELECT a.PurchaseId,a.PurchaseDate,a.Price,a.Quantity,a.Amount,a.Remark,a.DeliverId,
a.MaintainFactoryId,c.MaintainName,d.ActualDeliverID, b.*
FROM f_purchase a
INNER JOIN f_inquiry b on a.PurchaseId = b.PurchaseId
INNER JOIN b_maintainfactory c on c.MaintainFactoryId=a.MaintainFactoryId
LEFT JOIN p_deliver d ON d.PurchaseId=a.PurchaseId AND d.State=1
WHERE a.CarPartsId={0}";
        public const string Sql_Select_OrderList_Cond_StartDate = " AND a.PurchaseDate>='{0}'";
        public const string Sql_Select_OrderList_Cond_EndDate = " AND a.PurchaseDate<='{0}'";
        public const string Sql_Select_OrderList_Cond_Id = " AND a.PurchaseId={0}";
        public const string Sql_Select_OrderList_Cond_State = " AND a.DeliverId>0";
        public const string Sql_Select_OrderList_Cond_State1 = " AND a.DeliverId=0";


        public const string Sql_Select_DeliverList = @"SELECT a.PurchaseId,a.PurchaseDate,a.Price,a.Quantity,a.Amount,a.Remark,
a.MaintainFactoryId,c.MaintainName,d.DeliverId,d.DeliverDate,d.ActualDeliverID,d.DeliverReturnId, b.*
FROM f_purchase a
INNER JOIN f_inquiry b on a.PurchaseId = b.PurchaseId
INNER JOIN b_maintainfactory c on c.MaintainFactoryId=a.MaintainFactoryId
INNER JOIN p_deliver d ON d.PurchaseId=a.PurchaseId AND d.State=1
WHERE a.CarPartsId={0}";
        public const string Sql_Select_DeliverList_Cond_StartDate = " AND d.DeliverDate>='{0}'";
        public const string Sql_Select_DeliverList_Cond_EndDate = " AND d.DeliverDate<='{0}'";
        public const string Sql_Select_DeliverList_Cond_Id = " AND a.PurchaseId={0}";
    }
}

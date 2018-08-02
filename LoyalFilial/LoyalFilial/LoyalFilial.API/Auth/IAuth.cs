using LoyalFilial.Entity;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LoyalFilial.APIService.Auth
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IAuth”。
    [ServiceContract(Namespace = "http://LoyalFilial.api")]
    public interface IAuth
    {
        /// <summary>
        /// check user login api
        /// </summary>
        /// <param name="AccountId">account</param>
        /// <param name="Password">password</param>
        /// <param name="ImgAuthCode">code</param>
        /// <param name="DeviceCode">device</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "Login/?accountId={accountId}&password={password}&imgAuthCode={imgAuthCode}&deviceCode={deviceCode}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream Login(string accountId, string password, string imgAuthCode, string deviceCode);

        [OperationContract]
        [WebInvoke(UriTemplate = "LoginOut/?accountId={accountId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream LoginOut(string accountId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "GenerateSMSAuthCode/?phoneNo={phoneNo}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GenerateSMSAuthCode(string phoneNo);

        [OperationContract]
        [WebInvoke(UriTemplate = "CheckLoginUser/?accountId={accountId}&token={token}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream CheckLoginUser(string accountId, string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "Register/?accountId={accountId}&mobileNo={mobileNo}&password={password}&authCode={authCode}&userType={userType}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream Register(string accountId,long mobileNo, string password, int authCode, int userType);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveMaintainInfo/?saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveMaintainInfo(string saveRequest);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveCatPartsInfo/?saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveCatPartsInfo(string saveRequest);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetCarPartsList/?accountId={accountId}&token={token}&cityName={cityName}&mainServices={mainServices}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetCarPartsList(string accountId, string token, string cityName, string mainServices);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetCarPartsDetail/?accountId={accountId}&token={token}&carPartsId={carPartsId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetCarPartsDetail(string accountId, string token, int carPartsId);

        [OperationContract]
        [WebInvoke(UriTemplate = "FavoriteCarParts/?accountId={accountId}&token={token}&targetId={targetId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream FavoriteCarParts(string accountId, string token, int targetId);

        [OperationContract]
        [WebInvoke(UriTemplate = "CancelFavoriteCarParts/?accountId={accountId}&token={token}&targetId={targetId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream CancelFavoriteCarParts(string accountId, string token, int targetId);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetMFCustomerList/?accountId={accountId}&token={token}&cName={cName}&cMobile={cMobile}&cType={cType}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetMFCustomerList(string accountId, string token, string cName, string cMobile, int cType);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetMFCustomerDetail/?accountId={accountId}&token={token}&cId={cId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetMFCustomerDetail(string accountId, string token, int cId);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveMFCustomer/?accountId={accountId}&token={token}&saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveMFCustomer(string accountId, string token, string saveRequest);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetMFMaintainList/?accountId={accountId}&token={token}&startDate={startDate}&endDate={endDate}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetMFMaintainList(string accountId, string token, string startDate, string endDate);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetMFMaintainDetail/?accountId={accountId}&token={token}&mId={mId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetMFMaintainDetail(string accountId, string token, int mId);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveMFMaintain/?accountId={accountId}&token={token}&saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveMFMaintain(string accountId, string token, string saveRequest);


        [OperationContract]
        [WebInvoke(UriTemplate = "GetMFReserveList/?accountId={accountId}&token={token}&startDate={startDate}&endDate={endDate}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetMFReserveList(string accountId, string token, string startDate, string endDate);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetMFReserveDetail/?accountId={accountId}&token={token}&rId={rId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetMFReserveDetail(string accountId, string token, int rId);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveMFReserve/?accountId={accountId}&token={token}&saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveMFReserve(string accountId, string token, string saveRequest);

        [OperationContract]
        [WebInvoke(UriTemplate = "SendCustSMS/?accountId={accountId}&token={token}&phoneNo={phoneNo}&content={content}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SendCustSMS(string accountId, string token, string phoneNo, string content);


        [OperationContract]
        [WebInvoke(UriTemplate = "GetInquiryList/?accountId={accountId}&token={token}&inqId={inqId}&startDate={startDate}&endDate={endDate}&inqState={inqState}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetInquiryList(string accountId, string token, string inqId, string startDate, string endDate, string inqState);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetInquiryDetail/?accountId={accountId}&token={token}&inqId={inqId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetInquiryDetail(string accountId, string token, int inqId);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveInquiry/?accountId={accountId}&token={token}&saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveInquiry(string accountId, string token, string saveRequest);


        [OperationContract]
        [WebInvoke(UriTemplate = "GetQuotationList/?accountId={accountId}&token={token}&queryRequest={queryRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetQuotationList(string accountId, string token, string queryRequest);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveQuotation/?accountId={accountId}&token={token}&saveRequest={saveRequest}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveQuotation(string accountId, string token, string saveRequest);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetInquiryQuotationList/?accountId={accountId}&token={token}&InqId={InqId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetInquiryQuotationList(string accountId, string token, int InqId);

        [OperationContract]
        [WebInvoke(UriTemplate = "InquiryToPurchase/?accountId={accountId}&token={token}&QuoteId={QuoteId}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream InquiryToPurchase(string accountId, string token, int quoteId);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetPurchaseList/?accountId={accountId}&token={token}&purchaseId={purchaseId}&purState={purState}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetPurchaseList(string accountId, string token, string purchaseId, string purState);


        [OperationContract]
        [WebInvoke(UriTemplate = "GetOrderList/?accountId={accountId}&token={token}&purchaseId={purchaseId}&startDate={startDate}&endDate={endDate}&purState={purState}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetOrderList(string accountId, string token, string purchaseId, string startDate, string endDate, string purState);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveDeliver/?accountId={accountId}&token={token}&purchaseId={purchaseId}&actualDeliverID={actualDeliverID}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveDeliver(string accountId, string token, int purchaseId, string actualDeliverID);

        
        [OperationContract]
        [WebInvoke(UriTemplate = "GetDeliverList/?accountId={accountId}&token={token}&purchaseId={purchaseId}&startDate={startDate}&endDate={endDate}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream GetDeliverList(string accountId, string token, string purchaseId, string startDate, string endDate);


        [OperationContract]
        [WebInvoke(UriTemplate = "SaveDeliverReturn/?accountId={accountId}&token={token}&deliverId={deliverId}&remark={remark}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SaveDeliverReturn(string accountId, string token, int deliverId, string remark);
    }
}

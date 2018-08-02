using LoyalFilial.Entity;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LoyalFilial.APIService.SMS
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISMSReceiver”。
    [ServiceContract(Namespace = "http://LoyalFilial.api")]
    public interface ISMSReceiver
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "SMSStateNotify/?name={name}&pwd={pwd}&sendid={sendid}&time={time}&mobile={mobile}&state={state}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream SMSStateNotify(string name, string pwd, string sendid, string time, string mobile, string state);

        [OperationContract]
        [WebInvoke(UriTemplate = "ReceiveSMS/?name={name}&pwd={pwd}&args={args}", Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        Stream ReceiveSMS(string name, string pwd, string args);

    }
}

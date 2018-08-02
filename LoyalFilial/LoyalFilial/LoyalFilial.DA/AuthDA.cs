using LoyalFilial.BizCommon;
using LoyalFilial.Common;
using LoyalFilial.Entity;
using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyalFilial.DA
{
    public class AuthDA
    {
        public bool GenerateSMSAuthCode(string phoneNo)
        {
            try
            {
                if (!string.IsNullOrEmpty(phoneNo))
                {
                    //编码方式
                    Encoding encode = Encoding.GetEncoding(DAConstants.Request_Code);
                    //组装post参数
                    var authCode = new Random().Next(100000, 999999);
                    string parameter = string.Format(DAConstants.SMS_Params, authCode, phoneNo);

                    byte[] buffer = encode.GetBytes(parameter);
                    //发送请求获取结果
                    var result = RequestHelper.Request(DAConstants.SMS_Url,
                        DAConstants.Request_HTTP_POST,
                        DAConstants.Request_ContentType,
                        buffer,
                        encode);
                    LogManager.WriteInfoLogAsyn(result);
                    if (!string.IsNullOrEmpty(result))
                    {
                        var arrResult = result.Split(',');
                        if (arrResult.Length > 0)
                        {
                            if (arrResult[0] == "0")
                            {
                                LFFK.DataManager.DataProvider.ExecuteNonQuery(System.Data.CommandType.Text, string.Format(DAConstants.Sql_Insert_AuthCode, phoneNo, authCode));
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.WriteErrorLogAsyn(e, "发送验证码错误");
            }
            return false;
        }

        public IActResult Login(string accountId, string password, string ip, string token)
        {
            var result = new ActResult();
            var authUser = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (authUser != null)
            {
                if (authUser.ReTryTimes > 5)
                {
                    result = new ActResult("密码输入错误次数已超过5次，当前账号被锁定，请联系网站管理员！");
                }
                else
                {
                    if (authUser.Password.Equals(password, StringComparison.OrdinalIgnoreCase) && authUser.State == 1)
                    {
                        LFFK.DataManager.DataProvider.ExecuteNonQuery(System.Data.CommandType.Text,
                            String.Format(DAConstants.Sql_Insert_AuthLogin, accountId, ip, 
                            DateHelper.ConvertToString(DateTime.Now, DateType.Second), token,DateHelper.ConvertToString(DateTime.Now.AddDays(5), DateType.Second)));
                        LFFK.DataManager.DataProvider.ExecuteNonQuery(System.Data.CommandType.Text, String.Format(DAConstants.Sql_Update_RetryTimes0,accountId));
                        
                        LFFK.DataManager.Insert<LoginlogDO>(new LoginlogDO()
                        {
                            LoginIP = ip,
                            LoginTime = DateTime.Now,
                            LoginType = "LoginIn",
                            UserId = accountId,
                        });
                        return result;
                    }
                    else
                    {
                        LFFK.DataManager.DataProvider.ExecuteNonQuery(System.Data.CommandType.Text,  String.Format(DAConstants.Sql_Update_RetryTimes, accountId));

                        result = new ActResult(String.Format("用户密码不正确；输入错误密码多于5次用户账号将被锁定，您还有{0}次机会；谢谢！",
                            (4-authUser.ReTryTimes)));
                    }
                }
            }
            else
                result = new ActResult("密码错误或账号不存在");
            return result;
        }

        public IActResult LoginOut(string accountId, string ip)
        {
            var result = new ActResult();
            LFFK.DataManager.Delete<AuthLoginDO>().Where(p => p.UserId == accountId).Execute();
            LFFK.DataManager.Insert<LoginlogDO>(new LoginlogDO()
            {
                LoginIP = ip,
                LoginTime = DateTime.Now,
                LoginType = "LoginOut",
                UserId = accountId,
            });
            return result;
        }

        public bool CheckLoginUser(string accountId, string token)
        {
            var authUser = LFFK.DataManager.TableQuery<AuthLoginDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (authUser != null && authUser.Token.Equals(token, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IActResult Register(string accountId, long mobileNo, string password, int authCode,int userType)
        {
            var result = new ActResult();
            var authUser = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (authUser != null)
            {
                result = new ActResult("账号已存在");
            }
            else
            {
                var authCodeOld = LFFK.DataManager.TableQuery<AuthCodeDO>().Select().From().Where(p => p.MobileNo == mobileNo).Execute();
                if (authCodeOld != null && authCodeOld.AuthCode == authCode)
                {
                    var insResult = LFFK.DataManager.Insert<UserDO>(new UserDO()
                    {
                        UserId = accountId,
                        MobileNo = mobileNo,
                        Password = password,
                        ReTryTimes = 0,
                        State = 1,
                        UserType = userType,
                        CreateTime = DateTime.Now,
                        UserName = accountId.ToString(),
                    });
                    if (insResult.IsSucceed)
                        result = new ActResult();
                    else
                        result = new ActResult("注册失败：" + insResult.ErrorMsg);
                }
                else
                {
                    result = new ActResult("验证码输入错误");
                }
            }
            return result;
        }

        public bool SendCustSMS(string phoneNo, string content)
        {
            var result = false;
            try
            {
                if (!string.IsNullOrEmpty(phoneNo) && !string.IsNullOrEmpty(content))
                {
                    var log = new F_SMSSendLogDO()
                    {
                        Content = content,
                        MobileNo = Convert.ToInt64(phoneNo),
                        SendTime = DateTime.Now,
                        State = 0
                    };
                    var logIns = LFFK.DataManager.Insert<F_SMSSendLogDO>(log);
                    if (logIns.IsSucceed && logIns.IdentityRowNo > 0)
                    {
                        log.SendId = logIns.IdentityRowNo;
                        Encoding encode = Encoding.GetEncoding(DAConstants.Request_Code);
                        string parameter = string.Format(DAConstants.SMS_Params, content, phoneNo);
                        byte[] bufferPara = encode.GetBytes(parameter);
                        var sendResult = RequestHelper.Request(DAConstants.SMS_Url,
                            DAConstants.Request_HTTP_POST,
                            DAConstants.Request_ContentType,
                            bufferPara,
                            encode);
                        log.ReceivedResult = sendResult;
                        LogManager.WriteInfoLogAsyn(sendResult);
                        if (!string.IsNullOrEmpty(sendResult))
                        {
                            var arrResult = sendResult.Split(',');
                            if (arrResult.Length > 0)
                            {
                                if (arrResult[0] == "0")
                                {
                                    log.State = 1;
                                    result = true;
                                }
                                log.ReceivedId = arrResult[0];
                            }
                        }
                        LFFK.DataManager.Update<F_SMSSendLogDO>(log);
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.WriteErrorLogAsyn(e, "发送短信错误");
            }
            return result;
        }
    }
}

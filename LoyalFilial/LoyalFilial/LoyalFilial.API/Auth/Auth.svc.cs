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
using LoyalFilial.Entity.VO;

namespace LoyalFilial.APIService.Auth
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Auth : IAuth
    {
        public Stream Login(string accountId, string password, string imgAuthCode, string deviceCode)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(imgAuthCode))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                try
                {
                    string ip = SecurityHelper.GetWebClientIp();
                    string token = SecurityHelper.EncryptionMD5(accountId + (Guid.NewGuid()).ToString()).ToLower();
                    var user = BLManager.AuthBLManager.Login(accountId, password, ip, token);
                    if (user == null)
                    {
                        result.IsSuccess = false;
                        result.Message = WebConstants.Const_User_Login_Error_ForUser;
                        result.Code = -1;
                    }
                    else
                    {
                        result.IsSuccess = user.IsSucceed;
                        if (user.IsSucceed)
                        {
                            result.Message = token;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = user.ErrorMsg;
                            result.Code = -4;
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("登录用户验证报错:" + ex.Message);
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        public Stream LoginOut(string accountId)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(accountId))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                try
                {
                    string ip = SecurityHelper.GetWebClientIp();
                    var user = BLManager.AuthBLManager.LoginOut(accountId, ip);
                    if (user != null)
                    {
                        result.IsSuccess = user.IsSucceed;
                        result.Message = user.ErrorMsg;
                        result.Code = result.Code;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("登录用户登出报错:" + ex.Message);
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        public Stream CheckLoginUser(string accountId, string token)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(token))
            {
                result.IsSuccess = false;
                result.Message = "请先登录！";
                result.Code = -1;
            }
            else
            {
                try
                {
                    string ip = SecurityHelper.GetWebClientIp();
                    var checkResult = BLManager.AuthBLManager.CheckLoginUser(accountId, token);
                    if (checkResult)
                    {
                        result.IsSuccess = true;
                        result.Message = "";
                        result.Code = 1;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "请先登录!";
                        result.Code = -4;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("登录用户验证失败:" + ex.Message);
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        public Stream GenerateSMSAuthCode(string phoneNo)
        {
            ResponseDTO result = new ResponseDTO();
             var resultTmp = BLManager.AuthBLManager.GenerateSMSAuthCode(phoneNo);
             if (resultTmp)
             {
                 result.IsSuccess = true;
                 result.Code = 1;
                 result.Message = "";
             }
             else
             {
                 result.IsSuccess = false;
                 result.Code = 0;
                 result.Message = "验证码发送失败！";
             }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream Register(string accountId, long mobileNo, string password, int authCode, int userType)
        {
            ResponseDTO result = new ResponseDTO();

            #region check para
            if (string.IsNullOrEmpty(accountId) || mobileNo <= 0 || string.IsNullOrEmpty(password) || authCode <= 0 || userType <= 0)
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                try
                {
                    var regResult = BLManager.AuthBLManager.Register(accountId,mobileNo, password, authCode, userType);
                    if (regResult != null)
                    {
                        result.IsSuccess = regResult.IsSucceed;
                        if (regResult.IsSucceed)
                        {
                            result.Message = string.Empty;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = regResult.ErrorMsg;
                            result.Code = -4;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "注册失败";
                        result.Code = -1;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("注册报错:" + ex.Message);
                }
            }
            #endregion

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        
        public Stream SaveMaintainInfo(string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            #region check para
            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                try
                {
                    var maintainDO = SerializeHelper.JsonToObject<MaintainFactoryDO>(saveRequest);
                    var maintainResult = BLManager.MaintainBLManager.SaveBasic(maintainDO);
                    if (maintainResult != null)
                    {
                        result.IsSuccess = maintainResult.IsSucceed;
                        if (maintainResult.IsSucceed)
                        {
                            result.Message = string.Empty;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = maintainResult.ErrorMsg;
                            result.Code = -4;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "保存失败";
                        result.Code = -1;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }
            }
            #endregion

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        public Stream SaveCatPartsInfo(string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            #region check para
            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                try
                {
                    var carpartsDO = SerializeHelper.JsonToObject<CarPartsDO>(saveRequest);
                    var carpartsResult = BLManager.CarPartsBLManager.SaveBasic(carpartsDO);
                    if (carpartsResult != null)
                    {
                        result.IsSuccess = carpartsResult.IsSucceed;
                        if (carpartsResult.IsSucceed)
                        {
                            result.Message = string.Empty;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = carpartsResult.ErrorMsg;
                            result.Code = -4;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "保存失败";
                        result.Code = -1;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }
            }
            #endregion

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetCarPartsList(string accountId, string token, string cityName, string mainServices)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.CarPartsBLManager.GetCarPartsList(cityName, mainServices);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetCarPartsDetail(string accountId, string token, int carPartsId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.CarPartsBLManager.GetCarPartsDetail(carPartsId);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Code = 0;
                    result.Message = "数据未找到";
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream FavoriteCarParts(string accountId, string token, int targetId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.CarPartsBLManager.Favorite(accountId, 1, targetId);
                result.IsSuccess = resultTmp;
                result.Code = 0;
                result.Message = "";
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        public Stream CancelFavoriteCarParts(string accountId, string token, int targetId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.CarPartsBLManager.CancelFavorite(accountId, 1, targetId);
                result.IsSuccess = resultTmp;
                result.Code = 0;
                result.Message = "";
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }


        public Stream GetMFCustomerList(string accountId, string token, string cName, string cMobile, int cType)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.MaintainBLManager.GetMFCustomerList(accountId, cName, cMobile, cType);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetMFCustomerDetail(string accountId, string token, int cId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.MaintainBLManager.GetMFCustomerDetail(accountId,cId);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Code = 0;
                    result.Message = "数据未找到";
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveMFCustomer(string accountId, string token, string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            #region check para
            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
                {
                    result.IsSuccess = false;
                    result.Code = -1000;
                    result.Message = "请先登录！";
                }
                else
                {
                    try
                    {
                        var saveDO = SerializeHelper.JsonToObject<F_CustomerDO>(saveRequest);
                        var saveResult = BLManager.MaintainBLManager.SaveMFCustomer(accountId, saveDO);
                        if (saveResult != null)
                        {
                            result.IsSuccess = saveResult.IsSucceed;
                            if (saveResult.IsSucceed)
                            {
                                result.Message = string.Empty;
                                result.Code = 1;
                            }
                            else
                            {
                                result.Message = saveResult.ErrorMsg;
                                result.Code = -4;
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "保存失败";
                            result.Code = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                        result.Code = -3;
                        LFFK.LogManager.Error("保存报错:" + ex.Message);
                    }
                }
            }
            #endregion

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetMFMaintainList(string accountId, string token, string startDate, string endDate)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var starDateParm = DateTime.MinValue;
                var endDateParm = DateTime.MinValue;
                try
                {
                    if (!String.IsNullOrEmpty(startDate))
                        DateTime.TryParse(startDate, out starDateParm);
                    if (!String.IsNullOrEmpty(endDate))
                        DateTime.TryParse(endDate, out endDateParm);
                }
                catch (Exception ex)
                {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                        result.Code = -3;
                        LFFK.LogManager.Error("保存报错:" + ex.Message);
                }

                var resultTmp = BLManager.MaintainBLManager.GetMFMaintainList(accountId, starDateParm, endDateParm);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetMFMaintainDetail(string accountId, string token, int mId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.MaintainBLManager.GetMFMaintainDetail(accountId, mId);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Code = 0;
                    result.Message = "数据未找到";
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveMFMaintain(string accountId, string token, string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            #region check para
            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
                {
                    result.IsSuccess = false;
                    result.Code = -1000;
                    result.Message = "请先登录！";
                }
                else
                {
                    try
                    {
                        var saveDO = SerializeHelper.JsonToObject<F_MaintainDO>(saveRequest);
                        var saveResult = BLManager.MaintainBLManager.SaveMFMaintain(accountId, saveDO);
                        if (saveResult != null)
                        {
                            result.IsSuccess = saveResult.IsSucceed;
                            if (saveResult.IsSucceed)
                            {
                                result.Message = string.Empty;
                                result.Code = 1;
                            }
                            else
                            {
                                result.Message = saveResult.ErrorMsg;
                                result.Code = -4;
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "保存失败";
                            result.Code = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                        result.Code = -3;
                        LFFK.LogManager.Error("保存报错:" + ex.Message);
                    }
                }
            }
            #endregion

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetMFReserveList(string accountId, string token, string startDate, string endDate)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var starDateParm = DateTime.MinValue;
                var endDateParm = DateTime.MinValue;
                try
                {
                    if (!String.IsNullOrEmpty(startDate))
                        DateTime.TryParse(startDate, out starDateParm);
                    if (!String.IsNullOrEmpty(endDate))
                        DateTime.TryParse(endDate, out endDateParm);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }

                var resultTmp = BLManager.MaintainBLManager.GetMFReserveList(accountId, starDateParm, endDateParm);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetMFReserveDetail(string accountId, string token, int rId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.MaintainBLManager.GetMFReserveDetail(accountId, rId);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Code = 0;
                    result.Message = "数据未找到";
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveMFReserve(string accountId, string token, string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
                {
                    result.IsSuccess = false;
                    result.Code = -1000;
                    result.Message = "请先登录！";
                }
                else
                {
                    try
                    {
                        var saveDO = SerializeHelper.JsonToObject<F_ReservedDO>(saveRequest);
                        var saveResult = BLManager.MaintainBLManager.SaveMFReserve(accountId, saveDO);
                        if (saveResult != null)
                        {
                            result.IsSuccess = saveResult.IsSucceed;
                            if (saveResult.IsSucceed)
                            {
                                result.Message = string.Empty;
                                result.Code = 1;
                            }
                            else
                            {
                                result.Message = saveResult.ErrorMsg;
                                result.Code = -4;
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "保存失败";
                            result.Code = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                        result.Code = -3;
                        LFFK.LogManager.Error("保存报错:" + ex.Message);
                    }
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }


        public Stream SendCustSMS(string accountId, string token, string phoneNo, string content)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(phoneNo) || string.IsNullOrEmpty(content))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
                {
                    result.IsSuccess = false;
                    result.Code = -1000;
                    result.Message = "请先登录！";
                }
                else
                {
                    var resultTmp = BLManager.AuthBLManager.SendCustSMS(phoneNo,content);
                    if (resultTmp)
                    {
                        result.IsSuccess = true;
                        result.Code = 1;
                        result.Message = "";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Code = 0;
                        result.Message = "短信发送失败！"; 
                    }
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        #region 询价
        public Stream GetInquiryList(string accountId, string token, string inqId, string startDate, string endDate, string inqState)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var starDateParm = DateTime.MinValue;
                var endDateParm = DateTime.MinValue;
                var inqIdParm = 0;
                var inqStateParm = -99;
                try
                {
                    if (!String.IsNullOrEmpty(startDate))
                        DateTime.TryParse(startDate, out starDateParm);
                    if (!String.IsNullOrEmpty(endDate))
                        DateTime.TryParse(endDate, out endDateParm);
                    if (!String.IsNullOrEmpty(inqId))
                        int.TryParse(inqId, out inqIdParm);
                    if (!String.IsNullOrEmpty(inqState))
                        int.TryParse(inqState, out inqStateParm);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("参数错误:" + ex.Message);
                }

                var resultTmp = BLManager.MaintainBLManager.GetInquiryList(accountId, inqIdParm, starDateParm, endDateParm, inqStateParm);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetInquiryDetail(string accountId, string token, int inqId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.MaintainBLManager.GetInquiryDetail(accountId, inqId);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Code = 0;
                    result.Message = "数据未找到";
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveInquiry(string accountId, string token, string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
                {
                    result.IsSuccess = false;
                    result.Code = -1000;
                    result.Message = "请先登录！";
                }
                else
                {
                    try
                    {
                        var saveDO = SerializeHelper.JsonToObject<F_InquiryDO>(saveRequest);
                        var saveResult = BLManager.MaintainBLManager.SaveInquiry(accountId, saveDO);
                        if (saveResult != null)
                        {
                            result.IsSuccess = saveResult.IsSucceed;
                            if (saveResult.IsSucceed)
                            {
                                result.Message = string.Empty;
                                result.Code = 1;
                            }
                            else
                            {
                                result.Message = saveResult.ErrorMsg;
                                result.Code = -4;
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "保存失败";
                            result.Code = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                        result.Code = -3;
                        LFFK.LogManager.Error("保存报错:" + ex.Message);
                    }
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        
        /// <summary>
        /// 转采购
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="token"></param>
        /// <param name="InqId"></param>
        /// <returns></returns>
        public Stream GetInquiryQuotationList(string accountId, string token, int inqId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var resultTmp = BLManager.MaintainBLManager.GetInquiryQuotationList(accountId, inqId);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Code = 0;
                    result.Message = "数据未找到";
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream InquiryToPurchase(string accountId, string token, int quoteId)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                try
                {
                    var saveResult = BLManager.MaintainBLManager.InquiryToPurchase(accountId, quoteId);
                    if (saveResult != null)
                    {
                        result.IsSuccess = saveResult.IsSucceed;
                        if (saveResult.IsSucceed)
                        {
                            result.Message = string.Empty;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = saveResult.ErrorMsg;
                            result.Code = -4;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "保存失败";
                        result.Code = -1;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }


        public Stream GetPurchaseList(string accountId, string token, string purchaseId, string purState)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var starDateParm = DateTime.MinValue;
                var endDateParm = DateTime.MinValue;
                var purchaseIdParm = 0;
                var purStateParm = -99;
                try
                {
                    if (!String.IsNullOrEmpty(purchaseId))
                        int.TryParse(purchaseId, out purchaseIdParm);
                    if (!String.IsNullOrEmpty(purState))
                        int.TryParse(purState, out purStateParm);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }

                var resultTmp = BLManager.MaintainBLManager.GetPurchaseList(accountId, purchaseIdParm, starDateParm, endDateParm, purStateParm);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        #endregion

        #region 报价
        public Stream GetQuotationList(string accountId, string token, string queryRequest)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                QuotationQueryVO queryDO = null;
                try
                {
                    queryDO = SerializeHelper.JsonToObject<QuotationQueryVO>(queryRequest);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }

                var resultTmp = BLManager.CarPartsBLManager.GetQuotationList(accountId, queryDO);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveQuotation(string accountId, string token, string saveRequest)
        {
            ResponseDTO result = new ResponseDTO();

            if (string.IsNullOrEmpty(saveRequest))
            {
                result.IsSuccess = false;
                result.Message = "参数错误";
                result.Code = -1;
            }
            else
            {
                if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
                {
                    result.IsSuccess = false;
                    result.Code = -1000;
                    result.Message = "请先登录！";
                }
                else
                {
                    try
                    {
                        var saveDO = SerializeHelper.JsonToObject<P_QuotationDO>(saveRequest);
                        var saveResult = BLManager.CarPartsBLManager.SaveQuotation(accountId, saveDO);
                        if (saveResult != null)
                        {
                            result.IsSuccess = saveResult.IsSucceed;
                            if (saveResult.IsSucceed)
                            {
                                result.Message = string.Empty;
                                result.Code = 1;
                            }
                            else
                            {
                                result.Message = saveResult.ErrorMsg;
                                result.Code = -4;
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "保存失败";
                            result.Code = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                        result.Code = -3;
                        LFFK.LogManager.Error("保存报错:" + ex.Message);
                    }
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream GetOrderList(string accountId, string token, string purchaseId, string startDate, string endDate, string purState)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var starDateParm = DateTime.MinValue;
                var endDateParm = DateTime.MinValue;
                var purchaseIdParm = 0;
                var purStateParm = -99;
                try
                {
                    if (!String.IsNullOrEmpty(startDate))
                        DateTime.TryParse(startDate, out starDateParm);
                    if (!String.IsNullOrEmpty(endDate))
                        DateTime.TryParse(endDate, out endDateParm);
                    if (!String.IsNullOrEmpty(purchaseId))
                        int.TryParse(purchaseId, out purchaseIdParm);
                    if (!String.IsNullOrEmpty(purState))
                        int.TryParse(purState, out purStateParm);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("参数错误:" + ex.Message);
                }

                var resultTmp = BLManager.CarPartsBLManager.GetOrderList(accountId, purchaseIdParm, starDateParm, endDateParm, purStateParm);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveDeliver(string accountId, string token, int purchaseId, string actualDeliverID)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                try
                {
                    var saveResult = BLManager.CarPartsBLManager.SaveDeliver(accountId, purchaseId, actualDeliverID);
                    if (saveResult != null)
                    {
                        result.IsSuccess = saveResult.IsSucceed;
                        if (saveResult.IsSucceed)
                        {
                            result.Message = string.Empty;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = saveResult.ErrorMsg;
                            result.Code = -4;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "保存失败";
                        result.Code = -1;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }


        public Stream GetDeliverList(string accountId, string token, string purchaseId, string startDate, string endDate)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                var starDateParm = DateTime.MinValue;
                var endDateParm = DateTime.MinValue;
                var purchaseIdParm = 0;
                var purStateParm = -99;
                try
                {
                    if (!String.IsNullOrEmpty(startDate))
                        DateTime.TryParse(startDate, out starDateParm);
                    if (!String.IsNullOrEmpty(endDate))
                        DateTime.TryParse(endDate, out endDateParm);
                    if (!String.IsNullOrEmpty(purchaseId))
                        int.TryParse(purchaseId, out purchaseIdParm);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("参数错误:" + ex.Message);
                }

                var resultTmp = BLManager.CarPartsBLManager.GetDeliverList(accountId, purchaseIdParm, starDateParm, endDateParm);
                if (resultTmp != null)
                {
                    result.IsSuccess = true;
                    result.Code = 1;
                    result.Message = SerializeHelper.ObjectToJson(resultTmp);
                }
            }
            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }

        public Stream SaveDeliverReturn(string accountId, string token, int purchaseId, string remark)
        {
            ResponseDTO result = new ResponseDTO();
            if (!BLManager.AuthBLManager.CheckLoginUser(accountId, token))
            {
                result.IsSuccess = false;
                result.Code = -1000;
                result.Message = "请先登录！";
            }
            else
            {
                try
                {
                    var saveResult = BLManager.CarPartsBLManager.SaveDeliverReturn(accountId, purchaseId, remark);
                    if (saveResult != null)
                    {
                        result.IsSuccess = saveResult.IsSucceed;
                        if (saveResult.IsSucceed)
                        {
                            result.Message = string.Empty;
                            result.Code = 1;
                        }
                        else
                        {
                            result.Message = saveResult.ErrorMsg;
                            result.Code = -4;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "保存失败";
                        result.Code = -1;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    result.Code = -3;
                    LFFK.LogManager.Error("保存报错:" + ex.Message);
                }
            }

            return Basic.FunctionReturnDeal(SerializeHelper.ObjectToJson(result));
        }
        #endregion
    }
}

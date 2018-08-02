using LoyalFilial.Common;
using LoyalFilial.Entity;
using LoyalFilial.Entity.VO;
using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace LoyalFilial.DA
{
    public class MaintainDA
    {
        public IActResult SaveBasic(MaintainFactoryDO maintain)
        {
            maintain.CreateTime = DateTime.Now;
            if (maintain.MaintainFactoryId > 0)
            {
                return LFFK.DataManager.Update(maintain);
            }
            else
            {   
                var insResult = LFFK.DataManager.Insert(maintain);
                if (insResult.IsSucceed && insResult.IdentityRowNo > 0)
                {
                    LFFK.DataManager.DataProvider.ExecuteNonQuery(System.Data.CommandType.Text,
                        String.Format(DAConstants.Sql_Update_UserTargetId, insResult.IdentityRowNo, maintain.UserId));
                }
                return insResult;
            }
        }

        public List<F_CustomerDO> GetMFCustomerList(string accountId, string cName, string cMobile, int cType)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_MFCustomerList, mfDO.TargetId);
                if (!String.IsNullOrEmpty(cName))
                {
                    sql += String.Format(DAConstants.Sql_Select_MFCustomerList_Cond_CName, cName);
                }
                if (!String.IsNullOrEmpty(cMobile))
                {
                    sql += String.Format(DAConstants.Sql_Select_MFCustomerList_Cond_CMobile, cName);
                }
                if (cType >= 0)
                {
                    sql += String.Format(DAConstants.Sql_Select_MFCustomerList_Cond_CType, cType);
                }
                return LFFK.DataManager.ExecuteList<F_CustomerDO>(sql);
            }
            return new List<F_CustomerDO>();
        }

        public F_CustomerDO GetMFCustomerDetail(string accountId, int cId)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                return LFFK.DataManager.TableQuery<F_CustomerDO>().Select().From().Where(p => p.MaintainFactoryId == mfDO.TargetId && p.MFCustomerId == cId).Execute();
            }
            return null;
        }

        public IActResult SaveMFCustomer(string accountId, F_CustomerDO saveData)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                saveData.CreateTime = DateTime.Now;
                saveData.MaintainFactoryId = mfDO.TargetId;
                if (saveData.MFCustomerId > 0)
                {
                    return LFFK.DataManager.Update(saveData);
                }
                else
                {
                    return LFFK.DataManager.Insert(saveData);
                }
            }
            return new ActResult("当前用户无权新增客户");
        }

        public List<F_MaintainVO> GetMFMaintainList(string accountId, DateTime startDate, DateTime endDate)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_MFMaintainList, mfDO.TargetId);
                if (startDate > DateTime.MinValue)
                {
                    sql += String.Format(DAConstants.Sql_Select_MFMaintainList_Cond_StartDate, DateHelper.ConvertToString(startDate, DateType.Day));
                }
                if (endDate > DateTime.MinValue)
                {
                    sql += String.Format(DAConstants.Sql_Select_MFMaintainList_Cond_EndDate, DateHelper.ConvertToString(endDate, DateType.Day));
                }
                return LFFK.DataManager.ExecuteList<F_MaintainVO>(sql);
            }
            return new List<F_MaintainVO>();
        }

        public F_MaintainVO GetMFMaintainDetail(string accountId, int mId)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var maintainDo = LFFK.DataManager.TableQuery<F_MaintainDO>().Select().From().Where(p => p.MFMaintainId == mId).Execute();
                if (maintainDo != null)
                {
                    var customer = LFFK.DataManager.TableQuery<F_CustomerDO>().Select().From().Where(p => p.MaintainFactoryId == mfDO.TargetId && p.MFCustomerId == maintainDo.MFCustomerId).Execute();

                    if (customer != null)
                    {
                        var result = new F_MaintainVO()
                        {
                            Address = customer.Address,
                            BirthDay = customer.BirthDay,
                            CarBrand = customer.CarBrand,
                            CarType = customer.CarType,
                            CustormerName = customer.CustormerName,
                            MaintainDate = maintainDo.MaintainDate,
                            MaintainFactoryId = maintainDo.MaintainFactoryId,
                            MFCustomerId = maintainDo.MFCustomerId,
                            MFMaintainId = maintainDo.MFMaintainId,
                            MobileNo = customer.MobileNo,
                            PlateNO = customer.PlateNO,
                            RemindDate = maintainDo.RemindDate,
                            ServiceItem = maintainDo.ServiceItem,
                            VIN = customer.VIN
                        };
                        return result;
                    }
                }
            }
            return null;
        }

        public IActResult SaveMFMaintain(string accountId, F_MaintainDO saveData)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                saveData.CreateTime = DateTime.Now;
                saveData.MaintainFactoryId = mfDO.TargetId;
                saveData.State = 1;
                if (saveData.MFMaintainId > 0)
                {
                    return LFFK.DataManager.Update(saveData);
                }
                else
                {
                    return LFFK.DataManager.Insert(saveData);
                }
            }
            return new ActResult("当前用户无权限");
        }

        public List<F_ReservedVO> GetMFReserveList(string accountId, DateTime startDate, DateTime endDate)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_MFReserveList, mfDO.TargetId);
                if (startDate > DateTime.MinValue)
                {
                    sql += String.Format(DAConstants.Sql_Select_MFReserveList_Cond_StartDate, DateHelper.ConvertToString(startDate, DateType.Day));
                }
                if (endDate > DateTime.MinValue)
                {
                    sql += String.Format(DAConstants.Sql_Select_MFReserveList_Cond_EndDate, DateHelper.ConvertToString(endDate, DateType.Day));
                }
                return LFFK.DataManager.ExecuteList<F_ReservedVO>(sql);
            }
            return new List<F_ReservedVO>();
        }

        public F_ReservedVO GetMFReserveDetail(string accountId, int rId)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var baseDo = LFFK.DataManager.TableQuery<F_ReservedDO>().Select().From().Where(p => p.MFReserveId == rId).Execute();
                if (baseDo != null)
                {
                    var customer = LFFK.DataManager.TableQuery<F_CustomerDO>().Select().From().Where(p => p.MaintainFactoryId == mfDO.TargetId && p.MFCustomerId == baseDo.MFCustomerId).Execute();

                    if (customer != null)
                    {
                        var result = new F_ReservedVO()
                        {
                            Address = customer.Address,
                            BirthDay = customer.BirthDay,
                            CarBrand = customer.CarBrand,
                            CarType = customer.CarType,
                            CustormerName = customer.CustormerName,
                            ReservedDate = baseDo.ReservedDate,
                            MaintainFactoryId = baseDo.MaintainFactoryId,
                            MFCustomerId = baseDo.MFCustomerId,
                            MFReserveId = baseDo.MFReserveId,
                            MobileNo = customer.MobileNo,
                            PlateNO = customer.PlateNO,
                            ReserveType = baseDo.ReserveType,
                            ServiceItem = baseDo.ServiceItem,
                            VIN = customer.VIN,
                            State = baseDo.State,
                            FeedBack = baseDo.FeedBack,
                            Remark = baseDo.Remark,
                        };
                        return result;
                    }
                }
            }
            return null;
        }

        public IActResult SaveMFReserve(string accountId, F_ReservedDO saveData)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                saveData.CreateTime = DateTime.Now;
                saveData.MaintainFactoryId = mfDO.TargetId;
                if (saveData.MFReserveId > 0)
                {
                    return LFFK.DataManager.Update(saveData);
                }
                else
                {
                    return LFFK.DataManager.Insert(saveData);
                }
            }
            return new ActResult("当前用户无权限");
        }

        public List<F_InquiryVO> GetInquiryList(string accountId, int inqId, DateTime startDate, DateTime endDate, int inqState)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_InquiryList, mfDO.TargetId);
                if (inqId > 0)
                {
                    sql += String.Format(DAConstants.Sql_Select_InquiryList_Cond_Id, inqId);
                }
                else
                {
                    if (inqState > -99)
                    {
                        sql += String.Format(DAConstants.Sql_Select_InquiryList_Cond_State, inqState);
                    }
                    if (startDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_InquiryList_Cond_StartDate, DateHelper.ConvertToString(startDate, DateType.Day));
                    }
                    if (endDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_InquiryList_Cond_EndDate, DateHelper.ConvertToString(endDate, DateType.Day));
                    }
                }
                return LFFK.DataManager.ExecuteList<F_InquiryVO>(sql);
            }
            return new List<F_InquiryVO>();
        }

        public F_InquiryVO GetInquiryDetail(string accountId, int inqId)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var baseDo = LFFK.DataManager.TableQuery<F_InquiryDO>().Select().From().Where(p => p.InquiryId == inqId).Execute();
                if (baseDo != null)
                {
                    var result = new F_InquiryVO()
                    {
                        InquiryId = baseDo.InquiryId,
                        InquiryDate = baseDo.InquiryDate,
                        CarBrand = baseDo.CarBrand,
                        CarType = baseDo.CarType,
                        Alliance = baseDo.Alliance,
                        CarPartsId = baseDo.CarPartsId,
                        PlanDeliveryDate = baseDo.PlanDeliveryDate,
                        MaintainFactoryId = baseDo.MaintainFactoryId,
                        MaintainListImage = baseDo.MaintainListImage,
                        PartsName = baseDo.PartsName,
                        Quantity = baseDo.Quantity,
                        VIN = baseDo.VIN,
                        State = baseDo.State,
                        IsAnonymity = baseDo.IsAnonymity,
                        ToCarPartsType = baseDo.ToCarPartsType,
                        Remark = baseDo.Remark,
                    };
                    var quotList = LFFK.DataManager.TableQuery<P_QuotationDO>().Select().From().Where(p => p.InquiryId == inqId).ExecuteList();
                    if (quotList != null)
                    {
                        result.QuotationCount = quotList.Count;
                    }
                    return result;
                }
            }
            return null;
        }

        public IActResult SaveInquiry(string accountId, F_InquiryDO saveData)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                if (saveData.InquiryId > 0)
                {
                    saveData.UpdateTime = DateTime.Now;
                    saveData.UpdateUser = accountId;
                    var updateColumns = ExpressionBuilder.AssembleColumns<F_InquiryDO>(
                            p => p.Alliance,
                            p => p.CarBrand,
                            p => p.CarPartsId,
                            p => p.CarType,
                            p => p.PlanDeliveryDate,
                            p => p.MaintainListImage,
                            p => p.PartsName,
                            p => p.Quantity,
                            p => p.Remark,
                            p => p.State,
                            p => p.UpdateTime,
                            p => p.UpdateUser,
                            p => p.VIN,
                            p => p.IsAnonymity,
                            p => p.ToCarPartsType
                            );
                    return LFFK.DataManager.Update(saveData, updateColumns);
                }
                else
                {
                    saveData.MaintainFactoryId = mfDO.TargetId;
                    saveData.CreateTime = DateTime.Now;
                    saveData.CreateUser = accountId;
                    return LFFK.DataManager.Insert(saveData);
                }
            }
            return new ActResult("当前用户无权限");
        }

        public List<P_QuotationVO> GetInquiryQuotationList(string accountId, int inqId)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var result = LFFK.DataManager.ExecuteList<P_QuotationVO>(string.Format(DAConstants.Sql_Select_InquiryQuotationList, mfDO.TargetId, inqId));
                return result;
            }
            return null;
        }

        public IActResult InquiryToPurchase(string accountId, int quoteId)
        {
            IActResult result = new ActResult();
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var quoteDO = LFFK.DataManager.TableQuery<P_QuotationDO>().Select().From().Where(p => p.QuotationId == quoteId).Execute();
                if (quoteDO == null || quoteDO.State != 1)
                {
                    result = new ActResult("报价单不存在！");
                }
                else
                {
                    if (quoteDO.PurchaseId > 0)
                    {
                        result = new ActResult("已转采购单，不能重复操作！");
                    }
                    else if (quoteDO.InquiryId > 0)
                    {
                        var inquiryDO = LFFK.DataManager.TableQuery<F_InquiryDO>().Select().From().Where(p => p.InquiryId == quoteDO.InquiryId).Execute();
                        if (inquiryDO == null || inquiryDO.State != 1)
                        {
                            result = new ActResult("报价单不存在！");
                        }
                        else
                        {
                            quoteDO.UpdateTime = DateTime.Now;
                            quoteDO.UpdateUser = accountId;
                            quoteDO.PurchaseId = 0;

                            var updateQuoteColumns = ExpressionBuilder.AssembleColumns<P_QuotationDO>(
                                    p => p.PurchaseId,
                                    p => p.UpdateTime,
                                    p => p.UpdateUser
                                    );

                            inquiryDO.UpdateTime = DateTime.Now;
                            inquiryDO.UpdateUser = accountId;
                            inquiryDO.PurchaseId = 0;
                            inquiryDO.State = 2;

                            var updateInquiryColumns = ExpressionBuilder.AssembleColumns<F_InquiryDO>(
                                    p => p.PurchaseId,
                                    p => p.State,
                                    p => p.UpdateTime,
                                    p => p.UpdateUser
                                    );
                            var purchaseDo = new F_PurchaseDO()
                            {
                                PurchaseId = 0,
                                Amount = quoteDO.Amount,
                                CreateTime = DateTime.Now,
                                CreateUser = accountId,
                                InquiryId = inquiryDO.InquiryId,
                                MaintainFactoryId = inquiryDO.MaintainFactoryId,
                                CarPartsId = quoteDO.CarPartsId,
                                PartsName = quoteDO.PartsName,
                                Price = quoteDO.Price,
                                PurchaseDate = DateTime.Now,
                                Quantity = quoteDO.Quantity,
                                QuotationId = quoteDO.QuotationId,
                                Remark = quoteDO.Remark,
                                State = 1
                            };
                            using (var tran = new TransactionScope())
                            {
                                try
                                {
                                    var resultSave = LFFK.DataManager.Insert<F_PurchaseDO>(purchaseDo);
                                    if (resultSave.IsSucceed)
                                    {
                                        var purchaseId = Convert.ToInt32(resultSave.IdentityRowNo);
                                        quoteDO.PurchaseId = purchaseId;
                                        inquiryDO.PurchaseId = purchaseId;
                                        resultSave = LFFK.DataManager.Update(quoteDO, updateQuoteColumns);
                                        if (resultSave.IsSucceed)
                                            resultSave = LFFK.DataManager.Update(inquiryDO, updateInquiryColumns);
                                    }
                                    if (resultSave.IsSucceed)
                                    {
                                        tran.Complete();
                                    }
                                    
                                    result = resultSave;
                                }
                                catch (Exception ex)
                                {
                                    LFFK.LogManager.Error(ex);
                                    result = new ActResult(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            else
                result = new ActResult("当前用户无权限");

            return result;
        }

        /// <summary>
        /// 获取采购单列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="inqId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="purState"></param>
        /// <returns></returns>
        public List<F_PurchaseVO> GetPurchaseList(string accountId, int purchaseId, DateTime startDate, DateTime endDate, int purState)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 1 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_PurchaseList, mfDO.TargetId);
                if (purchaseId > 0)
                {
                    sql += String.Format(DAConstants.Sql_Select_PurchaseList_Cond_Id, purchaseId);
                }
                else
                {
                    if (purState > -99)
                    {
                        sql += String.Format(DAConstants.Sql_Select_PurchaseList_Cond_State, purState);
                    }
                    if (startDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_PurchaseList_Cond_StartDate, DateHelper.ConvertToString(startDate, DateType.Day));
                    }
                    if (endDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_PurchaseList_Cond_EndDate, DateHelper.ConvertToString(endDate, DateType.Day));
                    }
                }
                return LFFK.DataManager.ExecuteList<F_PurchaseVO>(sql);
            }
            return new List<F_PurchaseVO>();
        }
    }
}

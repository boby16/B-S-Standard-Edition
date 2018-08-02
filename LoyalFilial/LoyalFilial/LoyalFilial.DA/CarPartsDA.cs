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
    public class CarPartsDA
    {
        public IActResult SaveBasic(CarPartsDO carParts)
        {
            if (carParts.CarPartsId > 0)
            {
                return LFFK.DataManager.Update(carParts);
            }
            else
                return LFFK.DataManager.Insert(carParts);
        }
        public List<CarPartsDO> GetCarPartsList(string cityName, string mainServices)
        {
            var sql = String.Format(DAConstants.Sql_Select_CarPartsList, cityName);
            if (!String.IsNullOrEmpty(mainServices))
            {
                sql += String.Format(DAConstants.Sql_Select_CarPartsList_Cond, mainServices);
            }
            return LFFK.DataManager.ExecuteList<CarPartsDO>(sql);
        }

        public bool Favorite(string accountId, int targetType, int targetId)
        {
            var favorite = LFFK.DataManager.TableQuery<FavoritesDO>().Select().From().Where(p => p.UserId == accountId && p.TargetType == targetType && p.TargetId == targetId).Execute();
            if (favorite == null)
            {
                var result = LFFK.DataManager.DataProvider.ExecuteNonQuery(System.Data.CommandType.Text,
                    String.Format(DAConstants.Sql_Insert_Favorite, accountId, targetType, targetId));
                return result > 0;
            }
            return true;
        }
        public bool CancelFavorite(string accountId, int targetType, int targetId)
        {
            var result = LFFK.DataManager.Delete<FavoritesDO>().Where(p => p.UserId == accountId && p.TargetType == targetType && p.TargetId == targetId).Execute();
            return result.IsSucceed;
        }
        public CarPartsDO GetCarPartsDetail(int carPartsId)
        {
            return LFFK.DataManager.TableQuery<CarPartsDO>().Select().From().Where(p => p.CarPartsId == carPartsId).Execute();
        }

        public List<P_QuotationVO> GetQuotationList(string accountId, QuotationQueryVO query)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 2 && mfDO.TargetId > 0)
            {
                var cpVo = LFFK.DataManager.TableQuery<CarPartsDO>()
                    .Select(p => p.MainServices, p => p.Alliance, p => p.CarPartsId).From().Where(p => p.CarPartsId == mfDO.TargetId).Execute();
                if (cpVo != null && !String.IsNullOrEmpty(cpVo.MainServices))
                {
                    var sql = String.Format(DAConstants.Sql_Select_QuotationList, mfDO.TargetId,cpVo.Alliance, cpVo.MainServices);
                    if (query.InquiryId > 0)
                    {
                        sql += String.Format(DAConstants.Sql_Select_QuotationList_Cond_InqId, query.InquiryId);
                    }
                    else
                    {
                        if (query.State > -99)
                        {
                            if (query.State == 0)
                                sql += DAConstants.Sql_Select_QuotationList_Cond_State0;
                            else if (query.State == 0)
                                sql += DAConstants.Sql_Select_QuotationList_Cond_State1;
                        }
                        if (query.QuoteStartDate > DateTime.MinValue)
                        {
                            sql += String.Format(DAConstants.Sql_Select_InquiryList_Cond_StartDate, DateHelper.ConvertToString(query.QuoteStartDate, DateType.Day));
                        }
                        if (query.QuoteEndDate > DateTime.MinValue)
                        {
                            sql += String.Format(DAConstants.Sql_Select_InquiryList_Cond_EndDate, DateHelper.ConvertToString(query.QuoteEndDate, DateType.Day));
                        }
                        if (!string.IsNullOrEmpty(query.PartsName))
                        {
                            sql += String.Format(DAConstants.Sql_Select_QuotationList_Cond_PartsName, query.PartsName);

                        }
                    }
                    return LFFK.DataManager.ExecuteList<P_QuotationVO>(sql);
                }
            }
            return new List<P_QuotationVO>();
        }
        public IActResult SaveQuotation(string accountId, P_QuotationDO saveData)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 2 && mfDO.TargetId > 0)
            {
                saveData.Amount = saveData.Quantity * saveData.Price;

                var quoteDO = LFFK.DataManager.TableQuery<P_QuotationDO>().Select().From().Where(p => p.InquiryId == saveData.InquiryId).Execute();
                if (quoteDO != null && quoteDO.QuotationId > 0)
                {
                    saveData.UpdateTime = DateTime.Now;
                    saveData.UpdateUser = accountId;
                    saveData.QuotationId = quoteDO.QuotationId;
                    saveData.State = 1;
                    var updateColumns = ExpressionBuilder.AssembleColumns<P_QuotationDO>(
                            p => p.PartsName,
                            p => p.Quantity,
                            p => p.Remark,
                            p => p.State,
                            p => p.UpdateTime,
                            p => p.UpdateUser,
                            p => p.Price,
                            p => p.Amount
                            );
                    return LFFK.DataManager.Update(saveData, updateColumns);
                }
                else
                {
                    saveData.CarPartsId = mfDO.TargetId;
                    saveData.QuotationDate = DateTime.Now;
                    saveData.CreateTime = DateTime.Now;
                    saveData.CreateUser = accountId;
                    saveData.State = 1;
                    return LFFK.DataManager.Insert(saveData);
                }
            }
            return new ActResult("当前用户无权限");
        }

        public List<F_PurchaseVO> GetOrderList(string accountId, int purchaseId, DateTime startDate, DateTime endDate, int purState)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 2 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_OrderList, mfDO.TargetId);
                if (purchaseId > 0)
                {
                    sql += String.Format(DAConstants.Sql_Select_OrderList_Cond_Id, purchaseId);
                }
                else
                {
                    if (purState > -99)
                    {
                        if (purState == 1)
                            sql += DAConstants.Sql_Select_OrderList_Cond_State;
                        else
                            sql += DAConstants.Sql_Select_OrderList_Cond_State1;
                    }
                    if (startDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_OrderList_Cond_StartDate, DateHelper.ConvertToString(startDate, DateType.Day));
                    }
                    if (endDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_OrderList_Cond_EndDate, DateHelper.ConvertToString(endDate, DateType.Day));
                    }
                }
                return LFFK.DataManager.ExecuteList<F_PurchaseVO>(sql);
            }
            return new List<F_PurchaseVO>();
        }

        public IActResult SaveDeliver(string accountId, int purchaseId, string actualDeliverID)
        {
            IActResult result = new ActResult();
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 2 && mfDO.TargetId > 0)
            {
                var purchaseDO = LFFK.DataManager.TableQuery<F_PurchaseDO>().Select().From().Where(p => p.PurchaseId == purchaseId && p.CarPartsId == mfDO.TargetId).Execute();
                if (purchaseDO == null || purchaseDO.State != 1)
                {
                    result = new ActResult("采购单不存在！");
                }
                else
                {
                    if (purchaseDO.DeliverId > 0)
                    {
                        var deliverDO = LFFK.DataManager.TableQuery<P_DeliverDO>().Select().From().Where(p => p.DeliverId == purchaseDO.DeliverId).Execute();
                        if (deliverDO != null)
                        {
                            #region 修改
                            deliverDO.UpdateTime = DateTime.Now;
                            deliverDO.UpdateUser = accountId;
                            deliverDO.PurchaseId = purchaseDO.PurchaseId;
                            deliverDO.ActualDeliverID = actualDeliverID;

                            var updateDeliverColumns = ExpressionBuilder.AssembleColumns<P_DeliverDO>(
                                    p => p.PurchaseId,
                                    p => p.ActualDeliverID,
                                    p => p.UpdateTime,
                                    p => p.UpdateUser
                                    );

                            using (var tran = new TransactionScope())
                            {
                                try
                                {
                                    var resultSave = LFFK.DataManager.Update<P_DeliverDO>(deliverDO, updateDeliverColumns);
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
                            #endregion
                        }
                        else
                        {
                            #region 新增
                            deliverDO = new P_DeliverDO()
                            {
                                ActualDeliverID = actualDeliverID,
                                Amount = purchaseDO.Amount,
                                CarPartsId = mfDO.TargetId,
                                CreateTime = DateTime.Now,
                                CreateUser = accountId,
                                DeliverDate = DateTime.Now,
                                DeliverId = 0,
                                PartsName = purchaseDO.PartsName,
                                Price = purchaseDO.Price,
                                PurchaseId = purchaseDO.PurchaseId,
                                Quantity = purchaseDO.Quantity,
                                QuotationId = purchaseDO.QuotationId,
                                Remark = purchaseDO.Remark,
                                State = 1
                            };

                            purchaseDO.UpdateTime = DateTime.Now;
                            purchaseDO.UpdateUser = accountId;

                            var updatePurchaseColumns = ExpressionBuilder.AssembleColumns<F_PurchaseDO>(
                                    p => p.DeliverId,
                                    p => p.UpdateTime,
                                    p => p.UpdateUser
                                    );

                            using (var tran = new TransactionScope())
                            {
                                try
                                {
                                    var resultSave = LFFK.DataManager.Insert<P_DeliverDO>(deliverDO);
                                    if (resultSave.IsSucceed)
                                    {
                                        var deliverId = Convert.ToInt32(resultSave.IdentityRowNo);
                                        purchaseDO.DeliverId = deliverId;
                                        resultSave = LFFK.DataManager.Update(purchaseDO, updatePurchaseColumns);
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
                            #endregion
                        }
                    }
                    else
                    {
                        #region 新增
                        var deliverDO = new P_DeliverDO()
                        {
                            ActualDeliverID = actualDeliverID,
                            Amount = purchaseDO.Amount,
                            CarPartsId = mfDO.TargetId,
                            CreateTime = DateTime.Now,
                            CreateUser = accountId,
                            DeliverDate = DateTime.Now,
                            DeliverId = 0,
                            PartsName = purchaseDO.PartsName,
                            Price = purchaseDO.Price,
                            PurchaseId = purchaseDO.PurchaseId,
                            Quantity = purchaseDO.Quantity,
                            QuotationId = purchaseDO.QuotationId,
                            Remark = purchaseDO.Remark,
                            State = 1
                        };

                        purchaseDO.UpdateTime = DateTime.Now;
                        purchaseDO.UpdateUser = accountId;

                        var updatePurchaseColumns = ExpressionBuilder.AssembleColumns<F_PurchaseDO>(
                                p => p.DeliverId,
                                p => p.UpdateTime,
                                p => p.UpdateUser
                                );

                        using (var tran = new TransactionScope())
                        {
                            try
                            {
                                var resultSave = LFFK.DataManager.Insert<P_DeliverDO>(deliverDO);
                                if (resultSave.IsSucceed)
                                {
                                    var deliverId = Convert.ToInt32(resultSave.IdentityRowNo);
                                    purchaseDO.DeliverId = deliverId;
                                    resultSave = LFFK.DataManager.Update(purchaseDO, updatePurchaseColumns);
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
                        #endregion
                    }

                }
            }
            else
                result = new ActResult("当前用户无权限");

            return result;
        }


        public List<F_PurchaseVO> GetDeliverList(string accountId, int purchaseId, DateTime startDate, DateTime endDate)
        {
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 2 && mfDO.TargetId > 0)
            {
                var sql = String.Format(DAConstants.Sql_Select_DeliverList, mfDO.TargetId);
                if (purchaseId > 0)
                {
                    sql += String.Format(DAConstants.Sql_Select_DeliverList_Cond_Id, purchaseId);
                }
                else
                {
                    if (startDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_DeliverList_Cond_StartDate, DateHelper.ConvertToString(startDate, DateType.Day));
                    }
                    if (endDate > DateTime.MinValue)
                    {
                        sql += String.Format(DAConstants.Sql_Select_DeliverList_Cond_EndDate, DateHelper.ConvertToString(endDate, DateType.Day));
                    }
                }
                return LFFK.DataManager.ExecuteList<F_PurchaseVO>(sql);
            }
            return new List<F_PurchaseVO>();
        }

        public IActResult SaveDeliverReturn(string accountId, int deliverId, string remarks)
        {
            IActResult result = new ActResult();
            var mfDO = LFFK.DataManager.TableQuery<UserDO>().Select().From().Where(p => p.UserId == accountId).Execute();
            if (mfDO != null && mfDO.UserType == 2 && mfDO.TargetId > 0)
            {
                var deliverDO = LFFK.DataManager.TableQuery<P_DeliverDO>().Select().From().Where(p => p.DeliverId == deliverId && p.CarPartsId == mfDO.TargetId).Execute();
                if (deliverDO == null || deliverDO.State != 1)
                {
                    result = new ActResult("发货单不存在！");
                }
                else
                {
                    if (deliverDO.DeliverReturnId > 0)
                    {
                        result = new ActResult("已退货，不能重复操作！");
                    }
                    else
                    {
                        #region 新增
                        var deliverReturnDO = new P_DeliverReturnDO()
                         {
                             Amount = deliverDO.Amount,
                             CarPartsId = mfDO.TargetId,
                             CreateTime = DateTime.Now,
                             CreateUser = accountId,
                             DeliverReturnDate = DateTime.Now,
                             DeliverId = deliverDO.DeliverId,
                             DeliverReturnId = 0,
                             PartsName = deliverDO.PartsName,
                             Price = deliverDO.Price,
                             PurchaseId = deliverDO.PurchaseId,
                             Quantity = deliverDO.Quantity,
                             Remark = remarks,
                             State = 1
                         };

                        deliverDO.UpdateTime = DateTime.Now;
                        deliverDO.UpdateUser = accountId;

                        var updateColumns = ExpressionBuilder.AssembleColumns<P_DeliverDO>(
                                p => p.DeliverReturnId,
                                p => p.UpdateTime,
                                p => p.UpdateUser
                                );

                        using (var tran = new TransactionScope())
                        {
                            try
                            {
                                var resultSave = LFFK.DataManager.Insert<P_DeliverReturnDO>(deliverReturnDO);
                                if (resultSave.IsSucceed)
                                {
                                    var deliverReturnId = Convert.ToInt32(resultSave.IdentityRowNo);
                                    deliverDO.DeliverReturnId = deliverReturnId;
                                    resultSave = LFFK.DataManager.Update(deliverDO, updateColumns);
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
                        #endregion
                    }

                }
            }
            else
                result = new ActResult("当前用户无权限");

            return result;
        }
    }
}

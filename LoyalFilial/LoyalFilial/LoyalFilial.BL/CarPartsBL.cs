using LoyalFilial.DA;
using LoyalFilial.Entity;
using LoyalFilial.Entity.VO;
using LoyalFilial.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoyalFilial.BL
{
    public class CarPartsBL
    {
        public IActResult SaveBasic(CarPartsDO carparts)
        {
            return DAManager.CarPartsDAManager.SaveBasic(carparts);
        }
        public List<CarPartsDO> GetCarPartsList(string cityName, string mainServices)
        {
            return DAManager.CarPartsDAManager.GetCarPartsList(cityName, mainServices);
        }

        public bool Favorite(string accountId, int targetType, int targetId)
        {
            return DAManager.CarPartsDAManager.Favorite(accountId, targetType, targetId);
        }
        public bool CancelFavorite(string accountId, int targetType, int targetId)
        {
            return DAManager.CarPartsDAManager.CancelFavorite(accountId, targetType, targetId);
        }

        public CarPartsDO GetCarPartsDetail(int carPartsId)
        {
            return DAManager.CarPartsDAManager.GetCarPartsDetail(carPartsId);
        }
        
        public List<P_QuotationVO> GetQuotationList(string accountId, QuotationQueryVO query)
        {
            return DAManager.CarPartsDAManager.GetQuotationList(accountId, query);
        }


        public IActResult SaveQuotation(string accountId, P_QuotationDO saveData)
        {
            return DAManager.CarPartsDAManager.SaveQuotation(accountId, saveData);
        }

        public List<F_PurchaseVO> GetOrderList(string accountId, int purchaseId, DateTime startDate, DateTime endDate, int purState)
        {
            return DAManager.CarPartsDAManager.GetOrderList(accountId, purchaseId, startDate, endDate, purState);
        }

        public IActResult SaveDeliver(string accountId, int purchaseId, string actualDeliverID)
        {
            return DAManager.CarPartsDAManager.SaveDeliver(accountId, purchaseId, actualDeliverID);
        }


        public List<F_PurchaseVO> GetDeliverList(string accountId, int purchaseId, DateTime startDate, DateTime endDate)
        {
            return DAManager.CarPartsDAManager.GetDeliverList(accountId, purchaseId, startDate, endDate);
        }

        public IActResult SaveDeliverReturn(string accountId, int deliverId, string remark)
        {
            return DAManager.CarPartsDAManager.SaveDeliverReturn(accountId, deliverId, remark);
        }
    }
}

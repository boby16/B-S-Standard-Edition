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
    public class MaintainBL
    {
        public IActResult SaveBasic(MaintainFactoryDO maintain)
        {
            return DAManager.MaintainDAManager.SaveBasic(maintain);
        }

        public List<F_CustomerDO> GetMFCustomerList(string accountId, string cName, string cMobile, int cType)
        {
            return DAManager.MaintainDAManager.GetMFCustomerList(accountId, cName, cMobile, cType);
        }

        public F_CustomerDO GetMFCustomerDetail(string accountId, int cId)
        {
            return DAManager.MaintainDAManager.GetMFCustomerDetail(accountId, cId);
        }

        public IActResult SaveMFCustomer(string accountId, F_CustomerDO saveData)
        {
            return DAManager.MaintainDAManager.SaveMFCustomer(accountId, saveData);
        }

        public List<F_MaintainVO> GetMFMaintainList(string accountId, DateTime startDate, DateTime endDate)
        {
            return DAManager.MaintainDAManager.GetMFMaintainList(accountId, startDate, endDate);
        }

        public F_MaintainDO GetMFMaintainDetail(string accountId, int mId)
        {
            return DAManager.MaintainDAManager.GetMFMaintainDetail(accountId, mId);
        }

        public IActResult SaveMFMaintain(string accountId, F_MaintainDO saveData)
        {
            return DAManager.MaintainDAManager.SaveMFMaintain(accountId, saveData);
        }

        public List<F_ReservedVO> GetMFReserveList(string accountId, DateTime startDate, DateTime endDate)
        {
            return DAManager.MaintainDAManager.GetMFReserveList(accountId, startDate, endDate);
        }

        public F_ReservedVO GetMFReserveDetail(string accountId, int rId)
        {
            return DAManager.MaintainDAManager.GetMFReserveDetail(accountId, rId);
        }

        public IActResult SaveMFReserve(string accountId, F_ReservedDO saveData)
        {
            return DAManager.MaintainDAManager.SaveMFReserve(accountId, saveData);
        }

        public List<F_InquiryVO> GetInquiryList(string accountId, int inqId, DateTime startDate, DateTime endDate, int inqState)
        {
            return DAManager.MaintainDAManager.GetInquiryList(accountId, inqId, startDate, endDate, inqState);
        }

        public F_InquiryVO GetInquiryDetail(string accountId, int inqId)
        {
            return DAManager.MaintainDAManager.GetInquiryDetail(accountId, inqId);
        }

        public IActResult SaveInquiry(string accountId, F_InquiryDO saveData)
        {
            return DAManager.MaintainDAManager.SaveInquiry(accountId, saveData);
        }
        public List<P_QuotationVO> GetInquiryQuotationList(string accountId, int inqId)
        {
            return DAManager.MaintainDAManager.GetInquiryQuotationList(accountId, inqId);
        }

        public IActResult InquiryToPurchase(string accountId, int quoteId)
        {
            return DAManager.MaintainDAManager.InquiryToPurchase(accountId, quoteId);
        }
        
        /// <summary>
        /// 获取采购单列表
        /// </summary>
        /// <returns></returns>
        public List<F_PurchaseVO> GetPurchaseList(string accountId, int purchaseId, DateTime startDate, DateTime endDate, int purState)
        {
            return DAManager.MaintainDAManager.GetPurchaseList(accountId, purchaseId, startDate, endDate, purState);
        }
    }
}

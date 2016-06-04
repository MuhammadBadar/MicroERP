using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.BDM;
using QST.ERP.Domain.AlKhair;

namespace QST.ERP.Services
{
    public interface IAlKhairServiceContract
    {
        #region GiftType
        int AddGiftType(GiftTypeBE mod);
        bool ModifyGiftType(GiftTypeBE mod);
        bool DeleteGiftType(string siteCode, string GiftTypeTypeCode, int id);
        GiftTypeBE GetGiftTypeById(string siteCode, string GiftTypeTypeCode, int id);
        List<GiftTypeBE> GetAllGiftTypes(string siteCode, string giftTypeCode);
        List<GiftTypeView> GetViewOfAllGiftTypes(string siteCode, string giftTypeCode);
        //List<GiftTypeView> GetViewOfAllGiftTypes(string siteCode, string GiftTypeTypeCode);
        //IQueryable<GiftTypeBE> GetAllQuerableGiftTypes(string siteCode);

        #endregion

        #region Donor

        int AddDonor(DonorBE mod);
        bool ModifyDonor(DonorBE mod);
        bool DeleteDonor(string siteCode, int id);
        DonorBE GetDonorById(string siteCode, int id);
        //List<DonorBE> GetAllDonors(string siteCode);
        List<DonorBE> GetViewOfAllDonors(string siteCode);

        #endregion

        #region Voucher

        int AddVoucher(VoucherBE mod);
        bool ModifyVoucher(VoucherBE mod);
        bool DeleteVoucher(string siteCode, int id);
        VoucherBE GetVoucherById(string siteCode, int id);
        List<VoucherBE> GetAllVouchers(string siteCode);
        List<VoucherBE> GetViewOfAllVouchers(string siteCode);
        string GetNextVoucherNo(string userName);

        #endregion

    }
}

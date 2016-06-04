using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using QST.ERP.Domain.GroceryKit;
using QST.ERP.Services;
using System.IO;
using System.Linq;

namespace QST.ERP.Services.Tests
{
    [TestClass]
    public class GroceryKit
    {
        private IImportExportServiceContract _impSvc;

        [TestMethod]
        public void AddNewMember()
        {
            MemberDE mod = new MemberDE();
            
            // Entity Fields
            mod.SiteCode = "QST";
            mod.EntityTypeCode = "MGR";
            mod.EntityName = "Kamran Ahmad";

            // Person Fields
            mod.MeritalStatusId = 3;
            mod.DOB = DateTime.Now;
            mod.Age = 45;
            mod.Picture = "abc.jpb";
            mod.Gender = "M";
            mod.Signature = "ImranKhan#123";
            mod.ThumbImpression = "ThumbImpressionPicPath";
            mod.NationalId = "34201-8888377-2";

            // Form Fields
            mod.FormNo = "FRM-001";
            mod.FormType = "GKT";
            mod.FatherOrHusbandName = "Jameel Ahmad";
            mod.MedicalProblemId = 10;
            mod.MemberStatusId = 10;
            mod.OccupationId = 10;
            mod.HouseStatus = "P";
            mod.ZakatAcceptable = true;
            mod.FamilySize = 10;
            mod.Salary = 3000;
            mod.Donations = 2000;
            mod.OtherIncome = 5000;
            mod.ShortFallInCash = 8000;
            mod.Remarks = "Test Remarks";
            mod.IsDeserving = true;
            mod.ReferredBy = 20;
            mod.DataCollectedBy = 10;
            mod.DataCollectedBySignatures = "Test Signs";
            mod.ApprovalByDataCollectedPerson = true;
            mod.AuthorizedBy = 9;
            mod.ApprovalByDataAuthorizedPerson = true;
            
            // Member Fields
            mod.RegDate = DateTime.Now;
            mod.RegNo = "REG-001";
            mod.DistributionPointId = 5;
            mod.FamilyPackageId = 4;
            
            IGroceryKit grocSvc = new GroceryKitService();
            decimal memberId = grocSvc.AddMember(mod);
            if (memberId > decimal.Zero)
            {
                Assert.AreNotEqual(memberId, decimal.Zero);
            }
        }

        [TestMethod]
        public void Import_GroceryKit_Cards_From_Excel()
        {
            string strDoc = @"C:\Telenoc\Docs\Learning\_docs\Cards.xlsx";
            Stream stream = File.Open(strDoc, FileMode.Open);
            _impSvc = new ImportExportService();
            CardSheet sheet = _impSvc.ImportCardsFromExcel(stream);

            int processedRecords = sheet.CardSheetLines.Where(m => m.ExcelSheetError == ExcelSheetImportErrors.NoError).Count();

        }
    }

    
}

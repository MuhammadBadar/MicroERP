using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain;
using QST.ERP.Domain.GroceryKit;
using System.IO;
using OfficeOpenXml;

namespace QST.ERP.Services
{
    public class ImportExportService : IImportExportServiceContract
    {

        public CardSheet ImportCardsFromExcel(Stream stream)
        {
             CardSheet cardSheet = new CardSheet();
             try
             {
                 using (var xlPackage = new ExcelPackage(stream))
                 {
                     // get the first worksheet in the workbook
                     var worksheets = xlPackage.Workbook.Worksheets; //.FirstOrDefault();
                     var worksheet = worksheets["Sign_Sheet"];
                     //if (worksheet == null)
                     //    throw new NopException("No worksheet found");

                     //the columns
                     var properties = new[]
                {
                    //Reg. S.#	Form No	Name in English

                    "Reg. S.#",
                    "Form No",
                    "Name in English",
                };


                     int iRow = 4;
                     //int CardId = 1;
                     //_empSvc = new CardService();
                     //var Cards = _empSvc.GetAllThinCards();
                     while (true)
                     {
                         bool allColumnsAreEmpty = true;
                         for (var i = 1; i <= properties.Length; i++)
                             if (worksheet.Cells[iRow, i].Value != null && !String.IsNullOrEmpty(worksheet.Cells[iRow, i].Value.ToString()))
                             {
                                 allColumnsAreEmpty = false;
                                 break;
                             }
                         if (allColumnsAreEmpty)
                             break;

                         var line = new CardSheetLine();
                         cardSheet.CardSheetLines.Add(line);

                         try
                         {
                             int columnIndex = 1;

                             #region Month Days

                             // S.#	Reg. S.#	Form No	Name in English	Name in Urdu	Relation	Relation	Father / Husband Name	Father / Husband Name in urdu	
                             //CNIC No.	Family Members	Flour	Package	Dist. Id	City	Dist. Point	Dist. Point	 PICTURE 	 

                             line.SrNo = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;

                             line.RegSrNo = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FormNo = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Name = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.NameInUrdu = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Relation = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.RelationInUrdu = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FatherOrHusbandName = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FatherOrHusbandNameInUrdu = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.CNICNo = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMembers = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Flour = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Package = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.DistId = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.City = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.DistPoint = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.DistPointInUrdu = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Picture = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             //Alternate 	Active	DOB	Medical Problem	Contact No.	Contact Type	Occupation	Status	Reffered by	Gender	House Status	Name	Relation	Age	Status	Name	Relation	Age	Status	Name	Relation	Age	Status

                             line.Alternate = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Active = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.DOB = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.MedicalProblem = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.ContactNo = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.ContactType = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Occupation = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Status = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.ReferredBy = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.Gender = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.HouseStatus = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;

                             line.FamilyMember1Name = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember1Relation = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember1Age = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember1Status = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;

                             line.FamilyMember2Name = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember2Relation = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember2Age = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember2Status = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;

                             line.FamilyMember3Name = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember3Relation = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember3Age = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;
                             line.FamilyMember3Status = Convert.ToString(worksheet.Cells[iRow, columnIndex].Value); columnIndex += 1;

                             #endregion
                         }
                         catch (Exception ex)
                         {
                             line.ExcelSheetError = ExcelSheetImportErrors.ReadRowError;
                         }

                         #region Write Lines into DB

                         //try
                         //{
                         //    bool recordAlreadyExists = false;
                         //    if (!string.IsNullOrEmpty(line.CardCode) && Cards != null)
                         //    {
                         //        if (Cards.Where(m => m.CardCode == line.CardCode).FirstOrDefault() != null)
                         //        {
                         //            line.ExcelSheetError = ExcelSheetImportErrors.RecordAlreadyExistsInDB;
                         //            recordAlreadyExists = true;
                         //        }
                         //    }

                         //    if (!recordAlreadyExists)
                         //    {
                         //        CardBE emp = line.Translate();
                         //        _empRepo.Insert(emp);
                         //        _empUow.Commit();
                         //    }

                         //}
                         //catch (Exception ex)
                         //{
                         //    line.ExcelSheetError = ExcelSheetImportErrors.InsertRowInDBError;
                         //}

                         #endregion
                         iRow++;
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

            stream.Close();
            return cardSheet;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain;

namespace QST.ERP.Domain.GroceryKit
{
    public class CardSheet
    {
        public CardSheet()
        {
            CardSheetLines = new List<CardSheetLine>();
        }

        public string Msg
        {
            get
            {
                string _msg = string.Empty;
                int totalRecords = this.CardSheetLines.Count;
                if (totalRecords == 0)
                {
                    _msg = "No records could be read from Excel File";
                    return _msg;
                }
                else if (totalRecords == this.CardSheetLines.Where(m => m.ExcelSheetError == ExcelSheetImportErrors.NoError).Count())
                {
                    _msg = string.Format("Sheet Processed Successfully. Total Records: {0}", totalRecords);
                    return _msg;
                }

                int totalErrors = this.CardSheetLines.Where(m => m.ExcelSheetError != ExcelSheetImportErrors.NoError).Count();
                int alreadyExistsInDb = this.CardSheetLines.Where(m => m.ExcelSheetError != ExcelSheetImportErrors.RecordAlreadyExistsInDB).Count();
                if (totalErrors > 0)
                {
                    string ids = string.Empty;
                    foreach (var line in this.CardSheetLines)
                    {
                        if (line.ExcelSheetError != ExcelSheetImportErrors.NoError)
                            ids += line.RegSrNo + ",";
                    }
                    _msg = string.Format("Total Records: {0}, Processed: {1}, UnProcessed: {2}, UnProcessed Record Ids: [{3}], Records already in the Database: {4}", totalRecords, totalRecords - totalErrors, totalErrors, ids, alreadyExistsInDb);
                }


                return _msg;
            }

        }

        public List<CardSheetLine> CardSheetLines { get; set; }
    }
}

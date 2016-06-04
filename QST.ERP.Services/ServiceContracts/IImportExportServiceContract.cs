using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.GroceryKit;
using System.IO;

namespace QST.ERP.Services
{
    public interface IImportExportServiceContract
    {
         CardSheet ImportCardsFromExcel(Stream stream);

    }
}

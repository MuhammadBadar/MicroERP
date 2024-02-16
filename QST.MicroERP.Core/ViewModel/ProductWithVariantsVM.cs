using QST.MicroERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Core.ViewModel
{
    public class ProductWithVariantsVM
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? ProdName { get; set; }
        public bool? IsVariant { get; set; }
        public List<ProductTaxesDE> ProductTaxes { get; set; }
        public ProductWithVariantsVM()
        {
            ProductTaxes = new List<ProductTaxesDE> ();
        }
    }
}

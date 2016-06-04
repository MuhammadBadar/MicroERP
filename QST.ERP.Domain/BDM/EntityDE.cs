using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain.BDM
{
    public class EntityDE : BaseDomain
    {

        public EntityDE()
        {
            //EntityCode = string.Empty;
            EntityName = string.Empty;
            EntityTypeCode = EntityTypes.CMP.ToString();

            Addresses = new List<AddressDE>();
            Contacts = new List<ContactDE>();
            Code = string.Empty;
        }

        public EntityDE(EntityTypes type)
        {
            //EntityCode = string.Empty;
            EntityName = string.Empty;
            EntityTypeCode = type.ToString();

            Addresses = new List<AddressDE>();
            Contacts = new List<ContactDE>();
            Code = string.Empty;
        }
        //public decimal ParentEntityID { get; set; }
        //public string EntityCode { get; set; }
        public string Code { get; set; }
        public string EntityName { get; set; }
        public string EntityTypeCode { get; set; }
        
        public virtual IList<AddressDE> Addresses { get; set; }
        public virtual IList<ContactDE> Contacts { get; set; }

        public List<EntityDE> Entities { get; set; }
    }
}

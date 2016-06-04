using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.ERP.Domain
{
    public interface IBase // Marker Interface
    { 
    
    }
    //Base Entity Class for shared fields and functionalities.
    public abstract partial class BaseDomain // : IBase
    {
        public BaseDomain()
        {
            //CreatedBy = decimal.Zero;
            //CreatedOn = DateTime.Now;
            //ModifiedBy = null;
            //ModifiedOn = null;
            IsActive = true;
            SiteCode = "QST";// string.Empty;
        }

        public string SiteCode { get; set; }
        public string Code { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }
        //public virtual decimal CreatedBy { get; set; }
        //public virtual DateTime CreatedOn { get; set; }
        //public virtual decimal? ModifiedBy { get; set; }
        //public virtual DateTime? ModifiedOn { get; set; }
        public virtual bool IsActive { get; set; }
        

        /// <summary>
        /// It would Encode Domain Object to Key:Value pair Form
        /// e.g. ID:100|Name:Ali|Gender:M|Cell:03233483484 ...
        /// </summary>
        /// <returns></returns>
        public virtual string Encode() { return null; }
        public virtual string Encode(BaseDomain obj) { return null; }

        /// <summary>
        /// It is reverse of Encode ... It would translate back the Encoded string into Object Form 
        /// </summary>
        /// <returns></returns>
        protected virtual IBase Decode() { return null; }
    }
}

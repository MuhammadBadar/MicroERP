using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QST.ERP.WebApi.Models
{   
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
            IsActive = true;
            IsValid = true;
            IsAuthenticated = false;
            Mode = "Add";
            ViewName = "Distribution Name";
        }
        public string ViewName { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsValid { get; set; }
        public virtual bool Validate(){ return true;}
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public static bool IsAuthenticated;
        public string FieldId { get; set; }
        public string Mode { get; set; }

        public void TranslateException(Exception ex)
        {
            this.IsValid = false;
            this.Message = ex.Message;
        }
    }
}
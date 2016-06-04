using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using QST.ERP.Services;
using System.Collections.Generic;
using System.Linq;

namespace QST.ERP.ngClient.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IBDMServiceContract _bdmSvc;
        
        //[TestMethod]
        public void AddEntity()
        {
            _bdmSvc = new BDMService();
            EntityDE ety = new EntityDE();
            ety.EntityName = "Imran Farooq";
            ety.EntityTypeCode = EntityTypes.MGR.ToString();
            ety.SiteCode = AppConstants.SITE_CODE;

            AddressDE add = new AddressDE(AddressTypes.Personal.ToString());
            add.AddressLine1 = "St. Ahmad Din Sipahi";
            add.AddressLine2 = "Moh. Fatu Pura, Gujrat";


            ContactDE contact = new ContactDE();
            contact.Mobile1 = "0333-348343499";

            ety.Addresses.Add(add);
            ety.Contacts.Add(contact);

            decimal retVal = _bdmSvc.AddEntity(ety);


        }

        [TestMethod]
        public void ModifyEntity()
        {
            _bdmSvc = new BDMService();
            List<EntityDE> entities = _bdmSvc.GetAllEntities(AppConstants.SITE_CODE);
            var ety = entities.LastOrDefault();
            
            ety.EntityName = "Imran Farooq ...";
            ety.EntityTypeCode = EntityTypes.DOC.ToString();
            ety.SiteCode = AppConstants.SITE_CODE;

            AddressDE add = ety.Addresses.FirstOrDefault();
            add.AddressLine1 = "St. Ahmad Din Sipahi ...";
            add.AddressLine2 = "Moh. Fatu Pura, Gujrat ..";

            ContactDE contact = ety.Contacts.FirstOrDefault();
            contact.Mobile1 = "0333-348343499 ...";

            _bdmSvc.ModifyEntity(ety);
            ety = _bdmSvc.GetEntityById(AppConstants.SITE_CODE, ety.ID);

        }
    }
}

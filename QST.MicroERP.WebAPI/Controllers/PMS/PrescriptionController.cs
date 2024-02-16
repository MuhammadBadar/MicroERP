
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        #region Class Variables
        private PrescriptionService _PrescriptionSVC;
        private readonly IBaseService<AppointmentDE> _baseSvc;
        private readonly IBaseService<DoctorDE> _docSvc;

        #endregion
        #region Constructor
        public PrescriptionController ( IBaseService<AppointmentDE> baseSvc, IBaseService<DoctorDE> docSvc )
        {
            _PrescriptionSVC = new PrescriptionService (baseSvc,docSvc);
            _baseSvc = baseSvc;
            _docSvc = docSvc;
        }
        #endregion
        #region Http Verbs

        [HttpGet]
        public ActionResult Get ( )
        {
            PrescriptionDE Prescription = new PrescriptionDE ();
            List<PrescriptionDE> values = _PrescriptionSVC.SearchPrescriptions (Prescription);
            return Ok (values);
        }
        [HttpGet ("{id}")]
        public ActionResult GetPrescriptionById ( int id )
        {
            PrescriptionDE Prescription = new PrescriptionDE { Id = id };
            var values = _PrescriptionSVC.SearchPrescriptions (Prescription);
            return Ok (values);
        }
        [HttpPost ("{Search}")]
        public ActionResult Search ( PrescriptionDE Search )
        {
            //Search.IsActive = true;
            List<PrescriptionDE> values = _PrescriptionSVC.SearchPrescriptions (Search);
            return Ok (values);
        }
        [HttpPost]
        public ActionResult Post ( PrescriptionDE rx )
        {
            rx.DBoperation = DBoperations.Insert;
            foreach (RxMedicineDE line in rx.RxMedicines)
            {
                line.DBoperation = rx.DBoperation;
            }
            PrescriptionDE Prescription = _PrescriptionSVC.ManagementPrescriptionAsync (rx);
            return Ok (Prescription);
        }
        [HttpPut ("{Update}/{Activate}")]
        public ActionResult Activate ( PrescriptionDE rx )
        {
            rx.DBoperation = DBoperations.Activate;
            _PrescriptionSVC.ManagementPrescriptionAsync (rx);
            return Ok ();
        }
        [HttpPut ("{DeActivate}")]
        public ActionResult DeActivate ( PrescriptionDE rx )
        {
            rx.DBoperation = DBoperations.DeActivate;
            _PrescriptionSVC.ManagementPrescriptionAsync (rx);
            return Ok ();
        }
        [HttpPut]
        public ActionResult Put ( PrescriptionDE rx )
        {
            rx.DBoperation = DBoperations.Update;
            PrescriptionDE Prescription = _PrescriptionSVC.ManagementPrescriptionAsync (rx);
            return Ok (Prescription);
        }
        [HttpDelete ("{id}")]
        public void Delete ( int id )
        {
            PrescriptionDE Prescription = new PrescriptionDE { Id = id, DBoperation = DBoperations.Delete };
            _PrescriptionSVC.ManagementPrescriptionAsync (Prescription);
        }

        #endregion
    }
}

using MicroERP.Core.Entities;
using MicroERP.Core.Enums;
using MicroERP.Service;
using MicroERP.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        #region Data Members
        //private AppointmentService _appointSvc;
        private readonly IBaseService<AppointmentDE> _baseSvc;
        private readonly IApptService _apptSvc;
        #endregion
        #region Constructor        
        public AppointmentController ( IBaseService<AppointmentDE> baseSvc , IApptService apptSvc )
        {
            _baseSvc = baseSvc;
            _apptSvc = apptSvc;
        }
        #endregion
        #region HTTP Verbs
        [HttpGet]
        public IActionResult GetAppointment()
        {
            List<AppointmentDE> list = new List<AppointmentDE>();
            list = _baseSvc.SearchData(new AppointmentDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchAppointment(AppointmentDE appointment)
        {
            List<AppointmentDE> list = _baseSvc.SearchData (appointment);
            return Ok(list);
        }
        [HttpPost ("NextAppt")]
        public IActionResult SearchNextAppt ( AppointmentDE appointment )
        {
            List<AppointmentDE> list = _apptSvc.SearchAdjacentAppts (appointment);
            return Ok (list);
        }
        [HttpPost ("ApptMinTime")]
        public IActionResult GetTime ( AppointmentDE appointment )
        {
            var time= _apptSvc.ApptMinTime (appointment);
            return Ok ("\"" + time + "\"");
        }
        [HttpPost ("NextTknNo")]
        public IActionResult GetTokenNo ( AppointmentDE appointment )
        {
            var nmbr = _apptSvc.GetNextTokenNo (appointment);
            return Ok (nmbr);
        }
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            List<AppointmentDE> list = new List<AppointmentDE>();
            list = _baseSvc.SearchData (new AppointmentDE { Id = id });
            return Ok(list[0]);
        }
        [HttpPost]
        public IActionResult PostAppointment(AppointmentDE appointment)
        {
            appointment.DBoperation = DBoperations.Insert;
            _baseSvc.ManageData(appointment);
            return Ok();
        }
        [HttpPut]
        public IActionResult PutAppointment ( AppointmentDE appointment)
        {
            appointment.DBoperation = DBoperations.Update;
            _baseSvc.ManageData (appointment);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            AppointmentDE appointmentDe = new AppointmentDE();
            appointmentDe.DBoperation = DBoperations.Delete;
            appointmentDe.Id = id;
            _baseSvc.ManageData (appointmentDe);
            return Ok();
        }
        #endregion
        #region HTTP Verbs
        //[HttpGet]
        //public IActionResult GetAppointment ( )
        //{
        //    List<AppointmentDE> list = new List<AppointmentDE> ();
        //    list = _appointSvc.SearchAppointment (new AppointmentDE ());
        //    return Ok (list);
        //}
        //[HttpPost ("{Search}")]
        //public IActionResult SearchAppointment ( AppointmentDE appointment )
        //{
        //    List<AppointmentDE> list = _appointSvc.SearchAppointment (appointment);
        //    return Ok (list);
        //}
        //[HttpPost ("NextAppt")]
        //public IActionResult SearchNextAppt ( AppointmentDE appointment )
        //{
        //    List<AppointmentDE> list = _appointSvc.SearchNextAppt (appointment);
        //    return Ok (list);
        //}
        //[HttpGet ("{id}")]
        //public IActionResult GetAppointmentById ( int id )
        //{
        //    List<AppointmentDE> list = new List<AppointmentDE> ();
        //    list = _appointSvc.SearchAppointment (new AppointmentDE { Id = id });
        //    return Ok (list[0]);
        //}
        //[HttpPost]
        //public IActionResult PostAppointment ( AppointmentDE appointment )
        //{
        //    appointment.DBoperation = DBoperations.Insert;
        //    _appointSvc.ManageAppointment (appointment);
        //    return Ok ();
        //}
        //[HttpPut]
        //public IActionResult PutAppointment ( AppointmentDE appointment )
        //{
        //    appointment.DBoperation = DBoperations.Update;
        //    _appointSvc.ManageAppointment (appointment);
        //    return Ok ();
        //}
        //[HttpDelete ("{id}")]
        //public IActionResult DeleteAppointment ( int id )
        //{
        //    AppointmentDE appointmentDe = new AppointmentDE ();
        //    appointmentDe.DBoperation = DBoperations.Delete;
        //    appointmentDe.Id = id;
        //    _appointSvc.ManageAppointment (appointmentDe);
        //    return Ok ();
        //}
        #endregion
    }
}

using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QST.MicroERP.Service.CTL;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : BaseController
    {
        //private CatalogueService _catSvc;
        public ClientsController()
        {
            //_catSvc = new CatalogueService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetClients()
        {
            List<ClientsDE> list = new List<ClientsDE>();
            list = _catSvc.SearchClients(new ClientsDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchClients(ClientsDE clients)
        {
            List<ClientsDE> list = _catSvc.SearchClients(clients);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetClientsById(int id)
        {
            List<ClientsDE> list = new List<ClientsDE>();
            list = _catSvc.SearchClients(new ClientsDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostClients(ClientsDE clients)
        {
            clients.DBoperation = DBoperations.Insert;
            ClientsDE client = _catSvc.ManageClients (clients);
            return Ok (client);
        }

        [HttpPut]
        public IActionResult PutClients(ClientsDE clients)
        {
            clients.DBoperation = DBoperations.Update;
           ClientsDE client= _catSvc.ManageClients(clients);
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClients(int id)
        {
            ClientsDE clientsDe = new ClientsDE();
            clientsDe.DBoperation = DBoperations.Delete;
            clientsDe.Id = id;
            ClientsDE client = _catSvc.ManageClients (clientsDe);
            return Ok (client);
        }
    }
}

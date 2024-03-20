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
    public class ClientController : BaseController
    {
        //private CatalogueService _catSvc;
        public ClientController()
        {
            //_catSvc = new CatalogueService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetClients()
        {
            List<ClientDE> list = new List<ClientDE>();
            list = _catSvc.SearchClients(new ClientDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchClients(ClientDE clients)
        {
            List<ClientDE> list = _catSvc.SearchClients(clients);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetClientsById(int id)
        {
            List<ClientDE> list = new List<ClientDE>();
            list = _catSvc.SearchClients(new ClientDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostClients(ClientDE clients)
        {
            clients.DBoperation = DBoperations.Insert;
            ClientDE client = _catSvc.ManageClients (clients);
            return Ok (client);
        }

        [HttpPut]
        public IActionResult PutClients(ClientDE clients)
        {
            clients.DBoperation = DBoperations.Update;
           ClientDE client= _catSvc.ManageClients(clients);
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClients(int id)
        {
            ClientDE clientsDe = new ClientDE();
            clientsDe.DBoperation = DBoperations.Delete;
            clientsDe.Id = id;
            ClientDE client = _catSvc.ManageClients (clientsDe);
            return Ok (client);
        }
    }
}

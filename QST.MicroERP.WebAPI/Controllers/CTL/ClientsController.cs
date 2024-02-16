using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QST.MicroERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private ClientsService _clientsSvc;
        public ClientsController()
        {
            _clientsSvc = new ClientsService();
        }
        // HTTP Methods 
        [HttpGet]
        public IActionResult GetClients()
        {
            List<ClientsDE> list = new List<ClientsDE>();
            list = _clientsSvc.SearchClients(new ClientsDE());
            return Ok(list);
        }

        [HttpPost("{Search}")]
        public IActionResult SearchClients(ClientsDE clients)
        {
            List<ClientsDE> list = _clientsSvc.SearchClients(clients);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetClientsById(int id)
        {
            List<ClientsDE> list = new List<ClientsDE>();
            list = _clientsSvc.SearchClients(new ClientsDE { Id = id });
            return Ok(list[0]);

        }

        [HttpPost]
        public IActionResult PostClients(ClientsDE clients)
        {
            clients.DBoperation = DBoperations.Insert;
            ClientsDE client = _clientsSvc.ManageClients (clients);
            return Ok (client);
        }

        [HttpPut]
        public IActionResult PutClients(ClientsDE clients)
        {
            clients.DBoperation = DBoperations.Update;
           ClientsDE client= _clientsSvc.ManageClients(clients);
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClients(int id)
        {
            ClientsDE clientsDe = new ClientsDE();
            clientsDe.DBoperation = DBoperations.Delete;
            clientsDe.Id = id;
            ClientsDE client = _clientsSvc.ManageClients (clientsDe);
            return Ok (client);
        }
    }
}

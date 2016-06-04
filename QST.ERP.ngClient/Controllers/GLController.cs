using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using QST.ERP.Domain;
using QST.ERP.Services;
using QST.ERP.WebApi.Models;

namespace QST.ERP.ngClient.Controllers
{
    public class GLController : ApiController
    {
        private IPQSServiceContract _pqsSvc;
        private IBDMServiceContract _bdmSvc;
        private bool _mockingFlag;


        public GLController()
        {
            _pqsSvc = new PQSService();
            _bdmSvc = new BDMService();
            //_mockingFlag = true;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace QST.ERP.ngClient.Controllers
{
    public class HomeController : Controller
    {
        //private bool _authenticated;

        public ActionResult Index()
        {
            
            ViewBag.Title = "Home Page";

           

            if (!this.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

            
        }

        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            this.IsAuthenticated = true;
            if(userName == "Aftab" && password == "123")
            {
                Session["UserName"] = userName;
                return RedirectToAction("Index");
            }
            
            return View();
        }

        
        public bool IsAuthenticated {
            get
            {
                if (Session["IsAuthenticated"] != null)
                {
                   return (bool) Session["IsAuthenticated"];
                }
                else
                {
                    return false;
                }
            }
            set { Session["IsAuthenticated"] = value; } 
        }

    }
}

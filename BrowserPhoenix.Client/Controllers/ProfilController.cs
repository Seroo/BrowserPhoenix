using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrowserPhoenix.Client.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {

            using (var db = DatabasePortal.Open())
            {
                    
            }

            return View();
        }
    }
}
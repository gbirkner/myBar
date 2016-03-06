using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBar.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize( Roles = "Administrator, Backoffice" )]
        public ActionResult Index()
        {
            return View();
        }
    }
}
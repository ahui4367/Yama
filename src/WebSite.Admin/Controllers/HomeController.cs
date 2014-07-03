using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var user = Session["cuser"];
            if (user == null)
            {
                return Redirect("~/account/login");
            }
            ViewBag.User = user;
            return View();
        }
    }
}

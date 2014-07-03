using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Facade;

namespace WebSite.Admin.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Role/List
        public ActionResult List()
        {
            var list = ServiceFacade.UserSvc.Roles();
            var data = new 
            {
                total = list.Count(),
                rows = list.ToArray(),
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}

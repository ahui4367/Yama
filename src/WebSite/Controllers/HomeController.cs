namespace WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using View.Model;

    using YesHJ.Fx.Pattern;

    public class HomeController : Controller
    {
        #region Methods

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return Redirect("~/index.html");
        }

        //
        // GET: /Home/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // Post: /Home/Register
        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            try
            {
                //AccountFacade facade = new AccountFacade();
                //facade.RegisterUser(model);
                ViewBag.SubmitMessage = "提交成功";
            }
            catch (Exception ex)
            {
                ViewBag.SubmitMessage = "提交失败";
            }

            return View();
        }

        #endregion Methods
    }
}
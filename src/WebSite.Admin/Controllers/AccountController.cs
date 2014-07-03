using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WebSite.Admin.Filters;
using WebSite.Admin.Models;
using WebCore.Service;
using View.Model;
using Service.Facade;

namespace WebSite.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginModel());
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        { 
            string code = string.Empty;
            using (var stream = CaptchaBuilder.Instance.MakeCaptcha(ref code))
            {
                Session["captcha"] = code;
                return File(stream.ToArray(), "image/gif");
            }
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            var captcha = Session["captcha"] as string;
            if (!string.IsNullOrEmpty(captcha))
            {
                if (captcha.Equals(model.Captcha, StringComparison.OrdinalIgnoreCase))
                {
                    var user = ServiceFacade.UserSvc.Login(model);
                    if (user != null && user.Uid > 0)
                    {
                        Session["cuser"] = user;
                        FormsAuthentication.SetAuthCookie(model.UserCode, false);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        model.Error = "您的登录账号及密码不正确!";
                    }
                }
                else
                {
                    model.Error = "验证码不正确!";
                }
            }
            else 
            {
                model.Error = "请重新输入验证码!";
            }
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}

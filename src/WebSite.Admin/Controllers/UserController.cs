using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Facade;
using View.Model;
using YesHJ.Fx.Caching;

namespace WebSite.Admin.Controllers
{
    public class UserController : Controller
    {
        private ICache<string, object> Cache
        {
            get
            {
                return CacheProvider.Instance.GetCache<string, object>(CacheProvider.RUNTIME_CACHE);
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(PagingModel paging)
        {
            paging = paging ?? new PagingModel();
            var list = ServiceFacade.UserSvc.Load(paging);
            if (list != null)
            {
                var organlist = Cache.Get(OrganController.CACHE_KEY) as IEnumerable<OrganModel>;
                if (organlist == null)
                {
                    organlist = ServiceFacade.OrganSvc.LoadAll();
                    Cache.Add(OrganController.CACHE_KEY, organlist);
                }

                foreach (var item in list)
                {
                    if (item.Oid > 0)
                        item.Organ = organlist.FirstOrDefault(o => o.Oid == item.Oid).Orgname;
                }
                return Json(new 
                {
                    total = list.Count(),
                    rows = list.ToArray()
                }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        // ~/user/search
        public ActionResult Search(FormCollection condition)
        {
            return null;
        }

        // ~/user/get/5
        public ActionResult Get(int id)
        {
            return null;
        }

        // ~/user/save
        public ActionResult Save(UserModel user)
        {
            try
            {
                if (user.Uid > 0)
                {
                    ServiceFacade.UserSvc.Update(user);
                }
                else
                {
                    ServiceFacade.UserSvc.Add(user);
                }
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // ~/user/delete/5
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                ServiceFacade.UserSvc.Remove(new UserModel { Uid = id });
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "无效的用户ID" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

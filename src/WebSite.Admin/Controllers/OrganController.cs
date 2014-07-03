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
    public class OrganController : Controller
    {
        internal static readonly string CACHE_KEY = "organ.list";
        private ICache<string, object> Cache
        {
            get
            {
                return CacheProvider.Instance.GetCache<string, object>(CacheProvider.RUNTIME_CACHE);
            }
        }

        //
        // GET: /Organ/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Organ/All
        public ActionResult All()
        {
            IEnumerable<OrganModel> list = Cache.Get(CACHE_KEY) as IEnumerable<OrganModel>;
            if (list == null)
            {
                list = ServiceFacade.OrganSvc.LoadAll();
                Cache.Add(CACHE_KEY, list);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Organ/List
        public ActionResult List()
        {
            IEnumerable<OrganModel> list = Cache.Get(CACHE_KEY) as IEnumerable<OrganModel>;
            if (list == null)
            {
                list = ServiceFacade.OrganSvc.LoadAll();
                Cache.Add(CACHE_KEY, list);
            }

            var data = new
            {
                total = list.Count(),
                rows = list.ToArray(),
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(OrganModel model)
        {
            if (model != null)
            {
                try
                {
                    if (model.Oid > 0)
                    {
                        ServiceFacade.OrganSvc.Save(model);
                    }
                    else
                    {
                        ServiceFacade.OrganSvc.Add(model);
                    }
                    Cache.Remove(CACHE_KEY);
                    return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { error = "无效的参数" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(OrganModel model)
        {
            if (model != null && model.Oid > 0)
            {
                try
                {
                    ServiceFacade.OrganSvc.Delete(model);
                    Cache.Remove(CACHE_KEY);
                    return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { error = "无效的参数" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

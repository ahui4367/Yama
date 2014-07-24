using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Facade;
using View.Model;
using WebCore.Service;

namespace WebSite.Admin.Controllers
{
    public class MediaController : Controller
    {
        //
        // GET: /Media/

        public ActionResult Video()
        {
            return View();
        }

        public ActionResult Doc()
        {
            return View();
        }

        public ActionResult VSearch(VideoSearchModel paging)
        {
            paging.ValidateParameters();
            var videos = ServiceFacade.MediaSvc.LoadVideos(paging);
            if (videos != null)
            {
                return Json(new { total = paging.Total, rows = videos.ToArray() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { total = 0, rows = new VideoModel[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DSearch(DocSearchModel paging)
        {
            paging.ValidateParameters();
            var videos = ServiceFacade.MediaSvc.LoadDocs(paging);
            if (videos != null)
            {
                return Json(new { total = paging.Total, rows = videos.ToArray() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { total = 0, rows = new VideoModel[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult VDelete(int id)
        {
            try
            {
                ServiceFacade.MediaSvc.DeleteVideo(new VideoModel { VideoID = id });
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "删除视频失败！" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DDelete(int id)
        {
            try
            {
                ServiceFacade.MediaSvc.DeleteDoc(new DocumentModel { DocID = id });
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "删除文档失败！" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Uploady(HttpPostedFileBase file)
        {
            if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength < 1)
            {
                return this.HttpNotFound();
            }
            try
            {
                Guid key = Guid.NewGuid();
                FileInfo fi = new FileInfo(file.FileName);
                var newfile = key.ToString() + fi.Extension;
                file.SaveAs(Path.Combine(AppConfig.UploadRoot, newfile));

                return Json(new { error = "", file = newfile, origin = file.FileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult VSave(string file, string origin, string cmt)
        {
            VideoModel viModel = new VideoModel 
            {
                VideoName = origin,
                Comment = cmt,
                Media = file,
            };
            int mediatype = 0;
            string fullPath = Path.Combine(AppConfig.UploadRoot, file);
            FileInfo fi = new FileInfo(fullPath);
            List<string> output = new List<string>();
            if (fi.Exists)
            {
                if (".ppt".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    output = PptConverter.PPT(fi.FullName, ImageFormat.Jpeg);
                }
                else if (".pptx".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    output = PptConverter.PPTX(fi.FullName, ImageFormat.Jpeg, 0);
                }
                else if (".mp4".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    VideoConverter.Convert(fi.FullName);
                    mediatype = 1;
                }

                ServiceFacade.MediaSvc.SaveVideo(viModel);
                return Json(new { error = "", type = mediatype, count = output.Count }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "转化文件失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DSave(int? id, string file, string origin, string cmt)
        {
            DocumentModel viModel = new DocumentModel
            {
                DocID = id.HasValue ? id.Value : -1,
                DocName = origin,
                Comment = cmt,
                Media = file,
            };

            int mediatype = 0;
            string fullPath = Path.Combine(AppConfig.UploadRoot, file);
            FileInfo fi = new FileInfo(fullPath);
            List<string> output = new List<string>();
            if (fi.Exists)
            {
                if (viModel.DocID < 1)
                {
                    if (".ppt".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                    {
                        output = PptConverter.PPT(fi.FullName, ImageFormat.Jpeg);
                    }
                    else if (".pptx".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                    {
                        output = PptConverter.PPTX(fi.FullName, ImageFormat.Jpeg, 0);
                    }
                    else if (".mp4".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                    {
                        VideoConverter.Convert(fi.FullName);
                        mediatype = 1;
                    }
                }

                viModel.Count = output.Count;
                ServiceFacade.MediaSvc.SaveDoc(viModel);
                return Json(new { error = "", type = mediatype, count = output.Count }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "转化文件失败！" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

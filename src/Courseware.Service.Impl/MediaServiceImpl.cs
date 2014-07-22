using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Access;
using DbModel.AspnetDb;
using View.Model;
using YesHJ.Fx.Pattern;

namespace Courseware.Service.Impl
{
    public class MediaServiceImpl : MediaService
    {
        public RepositoryFactory DbFactory
        {
            get
            {
                return ServiceLocator.Current.Find<RepositoryFactory>();
            }
        }

        public override IEnumerable<VideoModel> LoadVideos(VideoSearchModel model)
        {
            model.ValidateParameters();
            string condition = string.Empty;
            IEnumerable<VideoModel> result = new List<VideoModel>();
            IRepository<Video_T> expr = null;
            using (var repo = DbFactory.Create<Video_T>())
            {
                expr = repo.GetAll();
                if (!string.IsNullOrEmpty(model.SearchText))
                {
                    switch (model.SearchType)
                    {
                        case 1:
                            expr = expr.GetFiltered(string.Format(" vname LIKE '%{0}%'", model.SearchText));
                            break;
                        case 2:
                            expr = expr.GetFiltered(string.Format(" comment LIKE '%{0}%'", model.SearchText));
                            break;
                    }
                }
                model.Total = expr.Count();
            }

            using (var repo = DbFactory.Create<Video_T>())
            {
                expr = repo.GetAll();
                if (!string.IsNullOrEmpty(model.SearchText))
                {
                    switch (model.SearchType)
                    {
                        case 1:
                            expr = expr.GetFiltered(string.Format(" vname LIKE '%{0}%'", model.SearchText));
                            break;
                        case 2:
                            expr = expr.GetFiltered(string.Format(" comment LIKE '%{0}%'", model.SearchText));
                            break;
                    }
                }
                //model.Total = repo.GetFiltered(
                result = expr.GetPaged<Video_T>(model.Page, model.Rows, " LastModified DESC")
                    .Select(q => new VideoModel
                    {
                        VideoID = q.Vid,
                        VideoName = q.Vname,
                        Comment = q.Comment,
                        Media = q.Media,
                        Created = q.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                        LastModified = q.Lastmodified.ToString("yyyy-MM-dd HH:mm:ss"),
                    })
                    .ToList();
            }

            return result;
        }

        public override IEnumerable<DocumentModel> LoadDocs(DocSearchModel model)
        {
            model.ValidateParameters();
            string condition = string.Empty;
            IEnumerable<DocumentModel> result = new List<DocumentModel>();
            IRepository<Document_T> expr = null;
            using (var repo = DbFactory.Create<Document_T>())
            {
                expr = repo.GetAll();
                if (!string.IsNullOrEmpty(model.SearchText))
                {
                    switch (model.SearchType)
                    {
                        case 1:
                            expr = expr.GetFiltered(string.Format(" dname LIKE '%{0}%'", model.SearchText));
                            break;
                        case 2:
                            expr = expr.GetFiltered(string.Format(" comment LIKE '%{0}%'", model.SearchText));
                            break;
                    }
                }
                model.Total = expr.Count();
            }

            using (var repo = DbFactory.Create<Document_T>())
            {
                expr = repo.GetAll();
                if (!string.IsNullOrEmpty(model.SearchText))
                {
                    switch (model.SearchType)
                    {
                        case 1:
                            expr = expr.GetFiltered(string.Format(" dname LIKE '%{0}%'", model.SearchText));
                            break;
                        case 2:
                            expr = expr.GetFiltered(string.Format(" comment LIKE '%{0}%'", model.SearchText));
                            break;
                    }
                }
                //model.Total = repo.GetFiltered(
                result = expr.GetPaged<Video_T>(model.Page, model.Rows, " LastModified DESC")
                    .Select(q => new DocumentModel
                    {
                        DocID = q.Did,
                        DocName = q.Dname,
                        Comment = q.Comment,
                        Media = q.Media,
                        Created = q.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                        LastModified = q.Lastmodified.ToString("yyyy-MM-dd HH:mm:ss"),
                    })
                    .ToList();
            }

            return result;
        }

        public override void SaveVideo(VideoModel model)
        {
            using (var repo = DbFactory.Create<Video_T>())
            {
                if (model.VideoID > 0)
                {
                    repo.Save(new Video_T
                    {
                        Vid = model.VideoID,
                        Vname = model.VideoName,
                        Media = model.Media,
                        Comment = model.Comment,
                    });
                }
                else
                {
                    repo.Add(new Video_T 
                    {
                        Vname = model.VideoName,
                        Media = model.Media,
                        Comment = model.Comment,
                    });
                }
            }
        }

        public override void SaveDoc(DocumentModel model)
        {
            using (var repo = DbFactory.Create<Document_T>())
            {
                if (model.DocID > 0)
                {
                    repo.Save(new Document_T
                    {
                        Did = model.DocID,
                        Dname = model.DocName,
                        Media = model.Media,
                        Comment = model.Comment,
                    });
                }
                else
                {
                    repo.Add(new Document_T
                    {
                        Did = model.DocID,
                        Dname = model.DocName,
                        Media = model.Media,
                        Comment = model.Comment,
                    });
                }
            }
        }

        public override void DeleteVideo(VideoModel model)
        {
            using (var repo = DbFactory.Create<Video_T>())
            {
                repo.Remove(new Video_T { Vid = model.VideoID });
            }
        }

        public override void DeleteDoc(DocumentModel model)
        {
            using (var repo = DbFactory.Create<Document_T>())
            {
                repo.Remove(new Document_T { Did = model.DocID });
            }
        }
    }
}

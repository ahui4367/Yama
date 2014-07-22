using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Model;

namespace Courseware.Service
{
    public abstract class MediaService
    {
        public abstract IEnumerable<VideoModel> LoadVideos(VideoSearchModel model);

        public abstract IEnumerable<DocumentModel> LoadDocs(DocSearchModel model);

        public abstract void SaveVideo(VideoModel model);

        public abstract void SaveDoc(DocumentModel model);

        public abstract void DeleteVideo(VideoModel model);

        public abstract void DeleteDoc(DocumentModel model);
    }
}

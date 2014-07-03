using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Access;
using DbModel.AspnetDb;
using View.Model;
using YesHJ.Fx.Pattern;

namespace Membership.Service.Impl
{
    public class OrganServiceImpl : OrganService
    {
        public RepositoryFactory DbFactory
        {
            get
            {
                return ServiceLocator.Current.Find<RepositoryFactory>();
            }
        }

        public override IEnumerable<OrganModel> LoadAll()
        {
            using (var repo = DbFactory.Create<Organ_T>())
            {
                return repo.GetAll().Select(o => new OrganModel
                {
                    Oid = o.Oid,
                    Orgname = o.Orgname,
                    Created = o.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                    Lastmodified = o.Lastmodified.ToString("yyyy-MM-dd HH:mm:ss"),
                }).ToList();
            }
        }

        public override void Add(View.Model.OrganModel model)
        {
            var item = new Organ_T 
            {
                Orgname = model.Orgname,
                Active = true,
                Created = DateTime.Now,
                Lastmodified = DateTime.Now
            };

            using (var repo = DbFactory.Create<Organ_T>())
            {
                repo.Add(item);
            }
        }

        public override void Save(View.Model.OrganModel model)
        {
            var item = new Organ_T
            {
                Oid = model.Oid,
                Orgname = model.Orgname,
                Active = true,
                Created = DateTime.Now,
                Lastmodified = DateTime.Now
            };

            using (var repo = DbFactory.Create<Organ_T>())
            {
                repo.Save(item);
            }
        }

        public override void Delete(View.Model.OrganModel model)
        {
            var item = new Organ_T
            {
                Oid = model.Oid
            };

            using (var repo = DbFactory.Create<Organ_T>())
            {
                repo.Remove(item);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Model;

namespace Membership.Service
{
    public abstract class OrganService
    {
        public abstract IEnumerable<OrganModel> LoadAll();

        public abstract void Add(OrganModel model);

        public abstract void Save(OrganModel model);

        public abstract void Delete(OrganModel model);
    }
}

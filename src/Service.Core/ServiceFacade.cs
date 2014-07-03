using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courseware.Service;
using Membership.Service;
using YesHJ.Fx.Pattern;

namespace Service.Facade
{
    public class ServiceFacade
    {
        private static TService Load<TService>()
            where TService : class
        {
            return ServiceLocator.Current.Find<TService>();
        }

        public static UserService UserSvc 
        {
            get
            {
                return Load<UserService>();
            }
        }

        public static OrganService OrganSvc
        {
            get
            {
                return Load<OrganService>();
            }
        }

        public static CourseService CourseSvc
        {
            get
            {
                return Load<CourseService>();
            }
        }
    }
}

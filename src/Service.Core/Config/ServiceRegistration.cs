/***************************************************************************************
 *
 * 功能说明：服务注册类
 *
 * 当前文件：ServiceRegistration.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace Service.Core.Config
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Courseware.Service;
    using Data.Access;
    using Membership.Service;
    using Microsoft.Practices.Unity;
    using Service.Core.Extension;
    using Service.Core.Locator;
    using YesHJ.Fx;
    using YesHJ.Fx.Constant;
    using YesHJ.Fx.Pattern;
    using YesHJ.Fx.Ref;

    public static class ServiceRegistration
    {
        #region Fields

        private static readonly IDictionary<Type, List<Type>> g_inheritCache = new Dictionary<Type, List<Type>>();

        #endregion Fields

        #region Properties

        private static string ConfigFile
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service_config.xml");
            }
        }

        #endregion Properties

        #region Methods

        [Warmup("Register ServiceRegistration")]
        private static void RegDependencies()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterTypeAsSingleton(typeof(UserService),
                Type.GetType("Membership.Service.Impl.UserServiceImpl, Membership.Service.Impl"))

                .RegisterTypeAsSingleton(typeof(OrganService),
                Type.GetType("Membership.Service.Impl.OrganServiceImpl, Membership.Service.Impl"))

                .RegisterTypeAsSingleton(typeof(RepositoryFactory),
                Type.GetType("Data.Access.Impl.RepositoryFactoryImpl, Data.Access.Impl"))

                .RegisterTypeAsSingleton(typeof(CourseService),
                Type.GetType("Courseware.Service.Impl.CourseServiceImpl, Courseware.Service.Impl"));


            ServiceLocator locator = new UnityLocator(container);
            //注册ServiceLocator到当前上下文中
            EngineContext.Instance.Items.Add(ContextConstants.SERVICE_LOCATOR_REGKEY, locator);
        }

        #endregion Methods
    }
}
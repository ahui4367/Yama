/***************************************************************************************
 *
 * 功能说明：基于Unity实现的ServiceLocator
 *
 * 当前文件：
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace Service.Core.Locator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using YesHJ.Fx.Pattern;

    public class UnityLocator : ServiceLocator
    {
        #region Fields

        private readonly IUnityContainer container;

        #endregion Fields

        #region Constructors

        public UnityLocator(IUnityContainer container)
        {
            this.container = container;
        }

        #endregion Constructors

        #region Methods

        public override TService Find<TService>()
        {
            TService result = default(TService);

            Type type = typeof(TService);

            if (!type.IsClass)
            {
                return null;
            }

            if (!type.IsAbstract || container.IsRegistered<TService>())
            {
                result = container.Resolve<TService>();
            }

            return result;
        }

        public override IEnumerable<TService> FindMultiple<TService>()
        {
            List<TService> result = new List<TService>();

            Type type = typeof(TService);

            if (type.IsClass && (!type.IsAbstract || container.IsRegistered<TService>()))
            {
                result.AddRange(container.ResolveAll<TService>());
            }

            return result;
        }

        #endregion Methods
    }
}
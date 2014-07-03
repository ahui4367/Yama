/***************************************************************************************
 *
 * 功能说明：通用服务定位器，用于IOC服务加载。
 *
 * 当前文件：ServiceLocator.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Pattern
{
    using System;
    using System.Collections.Generic;

    using YesHJ.Fx.Constant;

    /// <summary>
    /// 服务定位器基类
    /// 具体实现参考<see cref="YesHJ.Fx.Pattern.UnityServiceLocator"/>
    /// </summary>
    public abstract class ServiceLocator
    {
        #region Properties

        public static ServiceLocator Current
        {
            get
            {
                return EngineContext.Instance.Items.Get(ContextConstants.SERVICE_LOCATOR_REGKEY) as ServiceLocator;
            }
        }

        #endregion Properties

        #region Methods

        public abstract TService Find<TService>()
            where TService : class;

        public abstract IEnumerable<TService> FindMultiple<TService>()
            where TService : class;

        #endregion Methods
    }
}
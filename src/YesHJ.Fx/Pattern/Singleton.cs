/***************************************************************************************
 *
 * 功能说明：单例模式实现，实现类需要Private/Public/Protected无参构造函数
 *
 * 当前文件：Singleton.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 10:59 2012/11/6
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Pattern
{
    using System;
    using System.Linq;
    using System.Reflection;

    using YesHJ.Fx.Error;

    /// <summary>
    /// 单例模式实现，实现类需要Private/Public/Protected无参构造函数
    /// </summary>
    /// <typeparam name="TSingleton"></typeparam>
    public abstract class Singleton<TSingleton>
        where TSingleton : class
    {
        #region Fields

        private static readonly Lazy<TSingleton> g_instance;

        #endregion Fields

        #region Constructors

        static Singleton()
        {
            g_instance = new Lazy<TSingleton>(() =>
            {
                var ctors = typeof(TSingleton).GetConstructors(
                    BindingFlags.Instance
                    | BindingFlags.NonPublic
                    | BindingFlags.Public);
                ConstructorInfo ctor = null;
                if (ctors.Count() > 0)
                {
                    ctor = ctors.SingleOrDefault(c => c.GetParameters().Count() == 0 && c.IsPrivate);
                    if (ctor == null)
                        Assert.Throw<InvalidOperationException>(
                            SR.SingletonCreateFormat, typeof(TSingleton));
                }
                return (TSingleton)ctor.Invoke(null);
            });
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 构造静态实例
        /// </summary>
        public static TSingleton Instance
        {
            get
            {
                return g_instance.Value;
            }
        }

        #endregion Properties
    }
}
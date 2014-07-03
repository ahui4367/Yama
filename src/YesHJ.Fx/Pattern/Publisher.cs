/***************************************************************************************
 *
 * 功能说明：发布/订阅模式
 *
 * 当前文件：Publisher.cs
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Publisher
    {
        #region Methods

        public abstract bool Publish(int topic, params object[] parms);

        #endregion Methods
    }
}
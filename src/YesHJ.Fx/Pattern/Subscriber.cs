/***************************************************************************************
 *
 * 功能说明：发布/订阅模式
 *
 * 当前文件：Subscriber.cs
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

    #region Delegates

    public delegate void TopicChangeHandler(object sender, EventArgs e);

    #endregion Delegates

    public abstract class Subscriber
    {
        #region Events

        public event TopicChangeHandler OnChange;

        #endregion Events

        #region Methods

        public abstract void Subscribe(int topic);

        public abstract void Unsubscribe(int topic);

        #endregion Methods
    }
}
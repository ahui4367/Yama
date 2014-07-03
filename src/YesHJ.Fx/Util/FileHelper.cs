/***************************************************************************************
 *
 * 功能说明：文件帮助类，提供文件基本操作
 *
 * 当前文件：
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Utils.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using YesHJ.Fx.Error;

    public sealed class FileHelper
    {
        #region Fields

        private string _filePath;

        #endregion Fields

        #region Constructors

        private FileHelper(string path)
        {
            Assert.ThrowIfNullOrEmpty("path", path);

            this._filePath = path;
        }

        #endregion Constructors

        #region Methods

        public static FileHelper Create(string filePath)
        {
            return new FileHelper(filePath);
        }

        #endregion Methods
    }
}
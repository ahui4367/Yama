namespace YesHJ.Fx
{
    using System;

    public static class SR
    {
        #region Fields

        public static readonly string AUTH_TOKEN = "YunKey";

        /// <summary>
        /// Cache item not found
        /// </summary>
        public static readonly string CacheItemNotFoundFormat = "Item[key={0}] not found";
        public static readonly string DEBUG_RESOURCE_FORMAT = "{0}[{1},{2}]";

        /// <summary>
        /// 默认缓存容量，用于提高性能
        /// </summary>
        public static readonly int DEFAULT_CACHE_CAPACITY = 50;

        /// <summary>
        /// 默认缓存失效时间
        /// </summary>
        public static readonly TimeSpan DEFAULT_CACHE_EXPIRE = TimeSpan.FromSeconds(30);

        /// <summary>
        /// 默认缓存最大个数
        /// </summary>
        public static readonly uint DEFAULT_CACHE_MAX_SIZE = 500;
        public static readonly string DEFAULT_GENERICKEY_FORMAT = "{0}_NoName";

        /// <summary>
        /// 默认模块所在目录
        /// </summary>
        public static readonly string DEFAULT_MODULE_FOLDER = "Modules";

        /// <summary>
        /// Common exception output format.
        /// </summary>
        public static readonly string GENERAL_EXCEPTION_FORMAT = "{0}: {1}";
        public static readonly string MONGO_CONN_STRING = "HJYunMongo";
        public static readonly string MONGO_DEFAULT_DATABASE = "HJYun";
        public static readonly string MONGO_PK_COLLECTION = "YunPK";

        /// <summary>
        /// NullFormat
        /// </summary>
        public static readonly string NullFormat = "A parameter of {0} is null.";

        /// <summary>
        /// NullOrEmptyFormat
        /// </summary>
        public static readonly string NullOrEmptyFormat = "A parameter of {0} is null or empty.";

        /// <summary>
        /// RangeFormat
        /// </summary>
        public static readonly string RangeFormat = "A parameter of {0} is out of range.";
        public static readonly string REGISTION_EXIST_EXCEPTION_FORMAT = "Registion {0} already exist!";

        //public static readonly string DEFAULT_GENERICKEY_FORMAT = "{0}_NoName";
        public static readonly string RESOURCE_NOT_FOUND_FORMAT = "[Resource Not Found {0}]";

        /// <summary>
        /// 单例异常
        /// </summary>
        public static readonly string SingletonCreateFormat = "Type {0} must have exactly one constructor.";

        #endregion Fields
    }
}
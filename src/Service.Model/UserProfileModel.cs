namespace View.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using YesHJ.Fx.Caching;
    using YesHJ.Fx.Extension;

    public class UserProfileModel
    {
        #region Fields

        ICache<string, string> cache;

        #endregion Fields

        #region Constructors

        public UserProfileModel(ICache<string, string> cache)
        {
            this.cache = cache;
        }

        #endregion Constructors

        #region Methods

        public bool GetBoolean(string key)
        {
            return GetString(key).AsBoolean();
        }

        public DateTime GetDateTime(string key)
        {
            return GetString(key).AsDateTime();
        }

        public double GetDouble(string key)
        {
            return GetString(key).AsDouble();
        }

        public int GetInt(string key)
        {
            return GetString(key).AsInteger();
        }

        public string GetString(string key)
        {
            return cache.Get(key);
        }

        #endregion Methods
    }
}
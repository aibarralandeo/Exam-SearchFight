using System.Configuration;

namespace SearchFight.Shared.Config
{
    public class ConfigurationAccessor
    {
        #region Google Configuration

        public static string GoogleUri => GetSettings<string>("GoogleUrl");
        public static string GoogleKey => GetSettings<string>("GoogleApi");
        public static string GoogleCustom => GetSettings<string>("GoogleKey");

        #endregion

        #region Bing Configuration

        public static string BingUri => GetSettings<string>("BingUrl");
        public static string BingKey => GetSettings<string>("BingApi");
        public static string BingSubscriptionTag => GetSettings<string>("BingSubscriptionTag");

        #endregion

        public static T GetSettings<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            return string.IsNullOrWhiteSpace(value) ? default : (T) (object) (value);
        }
    }
}

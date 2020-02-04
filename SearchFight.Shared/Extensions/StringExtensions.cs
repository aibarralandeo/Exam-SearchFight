using System.Web.Script.Serialization;

namespace SearchFight.Shared.Extensions
{
    public static class StringExtensions
    {
        public static T DeserializeJson<T>(this string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
    }
}

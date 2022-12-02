using Newtonsoft.Json;
using System.Text.Json;

namespace MiniSurveys.Web.Helpers
{
    public static class SessionHelpers
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            var model = JsonConvert.DeserializeObject<T>(value);
            return model;
        }
    }
}

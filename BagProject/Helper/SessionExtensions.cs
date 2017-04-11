using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace BagProject.Helper
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            var setting = new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            
            session.SetString(key, JsonConvert.SerializeObject(value, setting));
        }
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
            ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}

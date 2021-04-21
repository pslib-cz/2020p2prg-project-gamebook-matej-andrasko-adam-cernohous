using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;

namespace gamebook.Services
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession sess, string key, T value)
        {
            JsonSerializer.SetDefaultResolver(StandardResolver.AllowPrivateCamelCase);
            var s = JsonSerializer.ToJsonString(value);
            sess.SetString(key, s);
        }

        public static T Get<T>(this ISession sess, string key)
        {
            var value = sess.GetString(key);
            if (value == null)
                return default;
            return JsonSerializer.Deserialize<T>(value);
        }
    }
}

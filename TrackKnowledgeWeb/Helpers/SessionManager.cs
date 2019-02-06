using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackKnowledgeWeb.Helpers
{
    public static class SessionManager
    {
        public static void SetSession(string key, object value)
        {
            System.Web.HttpContext.Current.Session[key] = value;
        }
        public static object GetSession(string key)
        {
            return System.Web.HttpContext.Current.Session[key];
        }
        public static bool HasSession(string key)
        {
            return GetSession(key) != null;
        }
        public static void Login(string api_key)
        {
            SetSession("api_key", api_key);
        }
        public static void Logout()
        {
            SetSession("api_key", null);
        }
    }
}
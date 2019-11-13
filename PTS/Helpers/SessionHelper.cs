using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTS.Helpers
{
    public class SessionHelper<T>
    {
        public static T GetSessionItem(string item)
        {
            return (T)HttpContext.Current.Session[item];
        }

        public static void SetSessionItem(string item,T nesne)
        {
            HttpContext.Current.Session[item]=nesne;
        }

    }
}
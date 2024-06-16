
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PainterWebsite.Logic
{
    public class Profile
    {
        public static class ReturnSessionData
        {
            public static List<Hashtable> GetSessionList(string key)
            {
                return HttpContext.Current.Session[key] as List<Hashtable>;
            }
        }
    }
}
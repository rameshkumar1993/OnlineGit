using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Yogiram.core.Models;
using System.Web.SessionState;

namespace Yogiram.core.Extensions
{
    public static class SessionExtensions
    {
        public static String GetDisplayName(this HttpSessionStateBase Session)
        {
            return Session != null ? (Session[session.UserName] as String) : String.Empty;
        }
    }
}

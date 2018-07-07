using EIM.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Mvc;

namespace EIM.Controllers
{
    public class EIMController : BaseController
    {
        [MenuAuthorize("7EF5A940-0C11-4304-8964-65ECD865FEA5")]
        public ActionResult EIMIndex()
        {
            return View();
        }

        public static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        protected String M(String Key)
        {
            ResourceManager rm = new ResourceManager(typeof(EIMResource));
            string ModuleString = rm.GetString(Key);
            return ModuleString;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yogiram.core.Extensions;

namespace Yogiram.core.Mvc
{
    public class NewtonsoftJsonResult : JsonResult
    {
        public JsonSerializer Serializer { get; set; }

        public NewtonsoftJsonResult()
            : base()
        {
            JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;
            Serializer = JsonHelper.GetSerializer();
        }
    }
}

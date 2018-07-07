using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogiram.core.Context;

namespace Yogiram.core.Extensions
{
    public static class JsonHelper
    {
        public static JsonSerializer GetSerializer()
        {
            return JsonSerializer.Create(GetSettings());
        }

        public static JsonSerializerSettings GetSettings()
        {
            JsonSerializerSettings Serializer = new JsonSerializerSettings();

            Serializer.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            Serializer.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Unspecified;

            return Serializer;
        }
        public class TypeNameSerializer : DefaultSerializationBinder
        {
            public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                assemblyName = null;
                typeName = serializedType.Name;
            }
        }
    }
}

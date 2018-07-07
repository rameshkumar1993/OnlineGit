using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Yogiram.core.Handler
{
    public class ModuleHandler 
    {
        public ResourceManager ResourceManagerType { get;set; }

        public String R(string Key)
        {
            string GlobalString = ResourceManagerType.GetString(Key);
            S(GlobalString);
            return GlobalString;
        }

        public static String S(string Key)
        {
            return Key;
        }
        
    }



}

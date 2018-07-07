using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using Yogiram.core.Resources;

namespace Attandance.Resource
{
    /// <summary>
    /// Return to the view Resource
    /// </summary>
    /// This is value return for view resource
    /// 
    public static class R
    {
        /// <summary>
        /// Return to the view module Resource
        /// </summary>
        /// This is value return for view resource
        /// 
        public static string M(string Key)
        {
            ResourceManager rm = new ResourceManager(typeof(AttandanceResource));
            string ModuleString = rm.GetString(Key);
            return ModuleString;
        }

        /// <summary>
        /// Return to the view global Resource
        /// </summary>
        /// This is value return for view resource
        /// 

        public static string G(string Key)
        {
            ResourceManager rm = new ResourceManager(typeof(Global));
            string ModuleString = rm.GetString(Key);
            return ModuleString;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogiram.core.Models
{
    public class SuccessJsonModel
    {
        public string msg { get; set; }
    }

    public class SuccessJsonDataModel : SuccessJsonModel
    {
        public Object data { get; set; }
    }

    public class ErrorJsonModel
    {
        public string error { get; set; }
    }

    public class ErrorSessionExpiredModel : ErrorJsonModel
    {
        public bool sessionExpired
        {
            get
            {
                return true;
            }
        }
    }
}

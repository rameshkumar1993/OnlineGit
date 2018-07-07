using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogiram.core.Handler
{
    public class UserDetails
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public String UserName { get; set; }
        public string Roles { get; set; }
        public bool PasswordExpired { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
    }
}

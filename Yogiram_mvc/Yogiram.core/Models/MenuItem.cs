using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogiram.core.Models
{
    public class MenuItem
    {
        public Guid MenuId { get; set; }

        public String MenuName { get; set; }

        public String FontAwesomeIcon { get; set; }

        public List<MenuItem> ChildMenus { get; set; }

        public String Url { get; set; }

        public String Target { get; set; }

        public Guid? ParentId { get; set; }
    }
}

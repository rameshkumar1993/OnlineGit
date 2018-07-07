using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogiram.core.Context
{
    [MetadataAttribute]
    public class ExportContextContainerAttribute : ExportAttribute
    {
        public String Name { get; set; }

        public ExportContextContainerAttribute(String Name)
            : base(typeof(ContextContainer))
        {
            this.Name = Name;
        }
    }

    public class ExportContextContainerView
    {
        public String Name { get; private set; }

        public ExportContextContainerView(IDictionary<string, object> aDict)
        {
            Name = (aDict["Name"] as String);
        }
    }
}

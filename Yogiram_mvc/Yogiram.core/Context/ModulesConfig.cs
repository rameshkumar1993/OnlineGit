using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogiram.core.Context
{
    public class ModulesConfig
    {
        private static CompositionContainer _CompositionContainer;

        public static CompositionContainer CompositionContainer
        {
            get { return _CompositionContainer; }
            private set { _CompositionContainer = value; }
        }
    }
}

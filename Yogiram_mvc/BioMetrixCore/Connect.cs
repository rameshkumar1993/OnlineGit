using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BioMetrixCore
{
    public partial class Connect : ApplicationContext 
    {
        public ZkemClient objZkeeper;
        private bool isDeviceConnected = false;

        public Connect()
        {
            objZkeeper = new ZkemClient(RaiseDeviceEvent);
            bool IsDeviceConnected = objZkeeper.Connect_Net("192.168.1.222", 4370);
        }

        private void RaiseDeviceEvent(object sender, string actionType)
        {

        }

    }
}

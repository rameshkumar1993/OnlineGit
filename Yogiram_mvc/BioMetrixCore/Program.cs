using System;
using System.Threading;
using System.Windows.Forms;

namespace BioMetrixCore
{
    public static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread TT = new Thread(() =>
            {
                Connect ct = new Connect();
                Application.Run();
            });

            TT.IsBackground = true;
            TT.SetApartmentState(ApartmentState.STA);
            TT.Start();

            //objZkeeper = new ZkemClient(RaiseDeviceEvent);
            //bool IsDeviceConnected = objZkeeper.Connect_Net("192.168.1.222", 4370);
            
            //Application.Run(new Connect());
            //Application.+= new ;
        }

    }
}

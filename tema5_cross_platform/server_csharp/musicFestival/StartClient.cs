using csharpMusicFestival;
using Networking;
using Services;
using System;
using System.Windows.Forms;

namespace musicFestival
{
    static class StartClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IService server = new ServerProxy("127.0.0.1", 55555);
            ClientController ctrl = new ClientController(server);
            LoginForm win = new LoginForm(ctrl);
            Application.Run(win);
        }
    }
}

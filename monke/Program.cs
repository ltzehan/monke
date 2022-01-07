using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monke
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            /*GlobalKeyboardEvents.Instance.KeyClickedEvents += (s, e) =>
            {
                StandaloneAudioPlayer.Instance.PlaySound(AssetSelector.Instance.PressSoundPath);
            };*/

            Application.Run(new Form1());
        }
    }
}

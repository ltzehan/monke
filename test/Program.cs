using monke;
using System;
using System.IO;

namespace test
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");
            var assets = AssetSelector.Instance;
            assets.Keyboard = KeyboardModel.models[0];
            var press = assets.PressSoundPath;
            Console.WriteLine(press);

            // var wtf = monke.Properties.Resources.ResourceManager.GetString(press);
            var wut = typeof(Form1).Assembly.GetManifestResourceStream(press);
            var mem = new MemoryStream();
            wut.CopyTo(mem);

            while(true)
            {
                mem.Seek(0, SeekOrigin.Begin);
                StandaloneAudioPlayer.Instance.PlaySound(mem);
            }
            // var inst = GlobalKeyboardEvents.Instance;

            Console.ReadKey();
        }
    }
}
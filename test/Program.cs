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

            while(true)
            {
                press.Seek(0, SeekOrigin.Begin);
                StandaloneAudioPlayer.Instance.PlaySound(press);
            }
            // var inst = GlobalKeyboardEvents.Instance;

            Console.ReadKey();
        }
    }
}
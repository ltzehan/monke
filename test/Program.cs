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
            AssetSelector.Keyboard = KeyboardModel.models[0];
            var press = assets.PressSoundPath;

            while(true)
            {
                press.Generic.Seek(0, SeekOrigin.Begin);
                StandaloneAudioPlayer.Instance.PlaySound(press.Generic);
            }
            // var inst = GlobalKeyboardEvents.Instance;

            Console.ReadKey();
        }
    }
}
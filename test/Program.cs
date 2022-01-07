using monke;
using System;

namespace test
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");
            // StandaloneAudioPlayer.Instance.PlaySound("");
            var inst = GlobalKeyboardEvents.Instance;
            Console.ReadKey();
        }
    }
}
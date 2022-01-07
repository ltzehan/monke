using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monke
{
    record KeySoundPath(string Generic, string Backspace, string Enter, string Space)
    {

    }

    // Service to load assets based on keyboard selected
    sealed internal class AssetSelector
    {
        const string PRESS_FOLDER_NAME = "press";
        const string RELEASE_FOLDER_NAME = "released";

        public KeySoundPath PressSoundPath { get; private set; }
        public KeySoundPath ReleaseSoundPath { get; private set; }

        private static KeyboardModel keyboard = KeyboardModel.models[0];

        public static KeyboardModel Keyboard
        {
            set 
            { 
                if (value.DisplayName != keyboard.DisplayName)
                {
                    keyboard = value;
                    Instance = LoadAssets(value);
                }   
            }
        }

        public static AssetSelector Instance { get; private set; } = LoadAssets(KeyboardModel.models[0]);

        private AssetSelector(KeySoundPath Press, KeySoundPath Release)
        {
            PressSoundPath = Press;
            ReleaseSoundPath = Release;
        }


        // Load assets from selected keyboard
        private static AssetSelector LoadAssets(KeyboardModel keyboard)
        {
            List<string> resourceNames = GetResourceNames(keyboard.Path);
            // TODO: Parse and map to specific keys
            Debug.WriteLine("dafuq");

            // Press sounds
            IEnumerable<string> pressResourceNames = resourceNames.Where(x => x.Contains(PRESS_FOLDER_NAME));
            string genericPressSound = pressResourceNames.First(x => x.Contains("GENERIC"));
            string backspacePressSound = pressResourceNames.FirstOrDefault(x => x.Contains("BACKSPACE")) ?? genericPressSound;
            string enterPressSound = pressResourceNames.FirstOrDefault(x => x.Contains("ENTER")) ?? genericPressSound;
            string spacePressSound = pressResourceNames.FirstOrDefault(x => x.Contains("BACKSPACE")) ?? genericPressSound;

            KeySoundPath pressSoundPath = new(
                Generic: genericPressSound,
                Backspace: backspacePressSound,
                Enter: enterPressSound,
                Space: spacePressSound
            );

            IEnumerable<string> releaseResourceNames = resourceNames.Where(x => x.Contains(RELEASE_FOLDER_NAME));
            string genericReleaseSound = releaseResourceNames.First(x => x.Contains("GENERIC"));
            string backspaceReleaseSound = releaseResourceNames.FirstOrDefault(x => x.Contains("BACKSPACE")) ?? genericReleaseSound;
            string enterReleaseSound = releaseResourceNames.FirstOrDefault(x => x.Contains("ENTER")) ?? genericReleaseSound;
            string spaceReleaseSound = releaseResourceNames.FirstOrDefault(x => x.Contains("BACKSPACE")) ?? genericReleaseSound;

            KeySoundPath releaseSoundPath = new(
                Generic: genericReleaseSound,
                Backspace: backspaceReleaseSound,
                Enter: enterReleaseSound,
                Space: spaceReleaseSound
            );

            return new(
                Press: pressSoundPath,
                Release: releaseSoundPath
            );
        }

        private static List<string> GetResourceNames(string path)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return new List<string>(assembly.GetManifestResourceNames()).Where(x => x.Contains(path)).ToList();
        }
    }
}

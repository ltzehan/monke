using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monke
{
    // Service to load assets based on keyboard selected
    sealed internal class AssetSelector
    {
        const string PRESS_PATH = "press";
        const string RELEASE_PATH = "released";

        private static KeyboardModel keyboard = KeyboardModel.models[0];
        // TODO: Map diff keys to diff sounds
        private static string pressSoundPath = "";
        private static string releaseSoundPath = "";

        public string PressSoundPath => pressSoundPath;
        public string ReleaseSoundPath => releaseSoundPath;

        public KeyboardModel Keyboard
        {
            set 
            { 
                if (value.DisplayName != keyboard.DisplayName)
                {
                    keyboard = value;
                    loadAssets();
                } 
            }
        }

        private AssetSelector() {
            // Load default assets
            loadAssets();
        }
        private static readonly Lazy<AssetSelector> lazy = new Lazy<AssetSelector>(() => new AssetSelector());
        public static AssetSelector Instance
        {
            get { return lazy.Value; }
        }

        // Load assets from selected keyboard
        private static void loadAssets()
        {
            List<string> resourceNames = GetResourcesNames(keyboard.Path);
            // TODO: Parse and map to specific keys
            pressSoundPath = resourceNames.First(x => x.Contains(PRESS_PATH) && x.Contains("GENERIC"));
            releaseSoundPath = resourceNames.First(x => x.Contains(RELEASE_PATH) && x.Contains("GENERIC"));
        }

        private static List<string> GetResourcesNames(string path)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return new List<string>(assembly.GetManifestResourceNames()).Where(x => x.Contains(path)).ToList();
        }
    }
}

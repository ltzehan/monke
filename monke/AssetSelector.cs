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
        // Default keyboard
        KeyboardModel keyboard = KeyboardModel.models[0];

        private AssetSelector() { }
        private static readonly Lazy<AssetSelector> lazy = new Lazy<AssetSelector>(() => new AssetSelector());
        public static AssetSelector Instance
        {
            get { return lazy.Value; }
        }

        

    }
}

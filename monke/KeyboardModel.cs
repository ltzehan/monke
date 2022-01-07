using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monke
{
    // Basically enums
    class KeyboardModel
    {
        private string displayName;
        private string path;
        public string DisplayName => displayName;
        public string Path => path;

        public KeyboardModel(string displayName, string path)
        {
            this.displayName = displayName;
            this.path = path;
        }

        public static readonly KeyboardModel[] models = {
            new KeyboardModel("Alpaca", "alpaca"),
            new KeyboardModel("Black Ink", "blackink"),
            new KeyboardModel("Blue Alps", "bluealps"),
            new KeyboardModel("Box Navy", "boxnavy"),
            new KeyboardModel("Buckling", "buckling"),
            new KeyboardModel("Cream", "cream"),
            new KeyboardModel("Holy Panda", "holypanda"),
            new KeyboardModel("MX Black", "mxblack"),
            new KeyboardModel("MX Blue", "mxblue"),
            new KeyboardModel("MX Brown", "mxbrown"),
            new KeyboardModel("Red Ink", "redink"),
            new KeyboardModel("Topre", "topre"),
            new KeyboardModel("Turqoise", "turqoise")
        };

    }
}

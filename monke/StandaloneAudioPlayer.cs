using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monke
{
    internal class StandaloneAudioPlayer
    {
        public static StandaloneAudioPlayer Instance { get; private set; } = new StandaloneAudioPlayer();

        public void PlaySound(string resource)
        {
            // TODO
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

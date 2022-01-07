using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monke
{
    public class StandaloneAudioPlayer
    {
        public static StandaloneAudioPlayer Instance { get; private set; } = new StandaloneAudioPlayer();

        public void PlaySound(string resource)
        {
            var speakers = Enumerable.Range(0, WaveOut.DeviceCount)
                .Where(x => WaveOut.GetCapabilities(x).ProductName.Contains("Speaker"))
                .ToArray();
            var waveOut = speakers[0];
            using var mp3 = new Mp3FileReader(resource);
            var player = new WaveOutEvent
            {
                DeviceNumber = waveOut
            };
            player.Init(mp3);
            player.Play();
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

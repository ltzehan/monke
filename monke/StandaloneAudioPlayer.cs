using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace monke
{
    public class StandaloneAudioPlayer
    {
        public static StandaloneAudioPlayer Instance { get; private set; } = new StandaloneAudioPlayer();

        public void PlaySound(Stream stream)
        {
            var speakers = Enumerable.Range(0, WaveOut.DeviceCount)
                .Where(x => WaveOut.GetCapabilities(x).ProductName.Contains("Speaker"))
                .ToArray();
            var waveOut = speakers[0];

            using var mp3 = new Mp3FileReader(stream);
            var player = new WaveOutEvent
            {
                DeviceNumber = waveOut,
                Volume = 1.0F
            };

            ManualResetEvent ev = new ManualResetEvent(false);
            player.PlaybackStopped += (s, e) => ev.Set();
            player.Init(mp3);
            player.Play();
            ev.WaitOne();
            // Console.WriteLine("played!");
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

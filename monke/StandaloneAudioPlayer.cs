using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly IWavePlayer player;
        private readonly MixingSampleProvider mixer;

        public StandaloneAudioPlayer()
        {
            var speakers = Enumerable.Range(0, WaveOut.DeviceCount)
                .Where(x => WaveOut.GetCapabilities(x).ProductName.Contains("Speaker"))
                .ToArray();
            var waveOut = speakers[0];
            player = new WaveOutEvent
            {
                DeviceNumber = waveOut,
                Volume = 1.0F,
            };
            Debug.WriteLine(WaveOut.GetCapabilities(speakers[0]).ProductName);
            int sampleRate = 44100;
            int channelCount = 1;

            mixer = new(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            player.Init(mixer);
            player.Play();
        }

        public void PlaySound(Stream stream)
        {
            AutoDisposeFileReader mp3 = new(new Mp3FileReader(stream));
            mixer.AddMixerInput(mp3);
            /*ManualResetEvent ev = new ManualResetEvent(false);
            player.PlaybackStopped += (s, e) => ev.Set();
            player.Init(mp3);
            player.Play();
            ev.WaitOne();*/
            Console.WriteLine("played!");
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

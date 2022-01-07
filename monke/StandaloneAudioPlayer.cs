using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace monke
{
    public class StandaloneAudioPlayer
    {
        public static StandaloneAudioPlayer Instance { get; private set; } = new StandaloneAudioPlayer();

        private readonly int deviceId;
        private readonly IWavePlayer player;
        private readonly MixingSampleProvider mixer;

        public StandaloneAudioPlayer()
        {
            deviceId = Enumerable.Range(0, WaveOut.DeviceCount)
                .First(idx => WaveOut.GetCapabilities(idx).ProductName.Contains("Speaker"));
            
            player = new WaveOutEvent
            {
                DeviceNumber = deviceId,
                Volume = 1.0F
            };

            int sampleRate = 44100;
            int channelCount = 1;

            mixer = new(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;

            player.Init(mixer);
            player.Play();
        }

        public void PlaySound(WaveStream keySound)
        {
            // mixer.AddMixerInput(keySound);
            keySound.Seek(0, SeekOrigin.Begin);

            var waveOut = new WaveOutEvent();
            waveOut.DeviceNumber = deviceId;
            waveOut.Init(keySound);
            waveOut.Play();
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

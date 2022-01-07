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

        public void PlaySound(Stream stream)
        {
            AutoDisposeFileReader mp3 = new(new Mp3FileReader(stream));
            // mixer.AddMixerInput(o);

            using var waveOut = new WaveOut();
            waveOut.DeviceNumber = deviceId;
            waveOut.Init(mp3);
            waveOut.Play();
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

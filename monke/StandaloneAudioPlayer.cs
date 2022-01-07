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

        private int deviceId;
        private readonly IWavePlayer player;

        public StandaloneAudioPlayer()
        {
            RecalcSpeakerId();

            var client = new CustomMMNotificationClient(this);
            var enumerator = new MMDeviceEnumerator();
            enumerator.RegisterEndpointNotificationCallback(client);
        }

        private int GetSpeakerId()
        {
            return Enumerable.Range(0, WaveOut.DeviceCount)
                .First(idx => WaveOut.GetCapabilities(idx).ProductName.Contains("Speaker"));
        }

        public void RecalcSpeakerId()
        {
            this.deviceId = GetSpeakerId();
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

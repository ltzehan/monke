using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace monke
{
    public class StandaloneAudioPlayer
    {
        public static StandaloneAudioPlayer Instance { get; private set; } = new StandaloneAudioPlayer();

        private DirectSoundDeviceInfo speakerDevice;
        private readonly IWavePlayer player;

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

        private DirectSoundDeviceInfo GetSpeakerId()
        {
            DirectSoundOut.Devices.ToList().ForEach(device => Debug.WriteLine(device.Description));
            return DirectSoundOut.Devices.First(dev => dev.Description.Contains("Speaker"));
        }

        public void RecalcSpeakerId()
        {
            this.speakerDevice = GetSpeakerId();
        }

        public void PlaySound(WaveStream keySound)
        {
            keySound.Seek(0, SeekOrigin.Begin);

            var waveOut = new DirectSoundOut(speakerDevice.Guid, 50);
            waveOut.Init(keySound);
            waveOut.Play();
        }

        
    }
}

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

        public StandaloneAudioPlayer() {
            speakerDevice = DirectSoundOut.Devices.First(dev => dev.Description.Contains("Speaker"));
        }

        public void PlaySound(WaveStream keySound)
        {
            keySound.Seek(0, SeekOrigin.Begin);

            DirectSoundOut waveOut = new(speakerDevice.Guid, 100);
            waveOut.Init(keySound);
            waveOut.Play();
        }

        
    }
}

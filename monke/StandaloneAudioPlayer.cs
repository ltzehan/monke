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

        private readonly DirectSoundDeviceInfo speakerDevice;

        private readonly IWavePlayer player;
        private readonly MixingSampleProvider mixer;

        public StandaloneAudioPlayer()
        {
            DirectSoundOut.Devices.ToList().ForEach(device => Debug.WriteLine(device.Description));
            speakerDevice = DirectSoundOut.Devices.First(dev => dev.Description.Contains("Speaker"));
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

using NAudio.CoreAudioApi;
using NAudio.Wave;
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

        public StandaloneAudioPlayer()
        {
            deviceId = Enumerable.Range(0, WaveOut.DeviceCount)
                .Select(idx => WaveOut.GetCapabilities(idx).ProductName)
                .ToList()
                .FindIndex(x => x.Contains("Speaker"));
        }

        public void PlaySound(Stream stream)
        {
            AutoDisposeFileReader mp3 = new(new Mp3FileReader(stream));

            var waveOut = new WaveOut();
            waveOut.DeviceNumber = deviceId;
            waveOut.Volume = 1.0F;
            waveOut.Init(mp3);
            waveOut.Play();
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

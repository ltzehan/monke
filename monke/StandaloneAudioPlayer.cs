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

        public void PlaySound(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var mp3 = new Mp3FileReader(stream);

            var deviceId = Enumerable.Range(0, WaveOut.DeviceCount)
                .Select(idx => WaveOut.GetCapabilities(idx).ProductName)
                .ToList()
                .FindIndex(x => x.Contains("Speaker"));

            var waveOut = new WaveOut();
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

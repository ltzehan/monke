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
            var enumerator = new MMDeviceEnumerator();

            var dev = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            var speakers = dev.Where(x => x.FriendlyName.Contains("Speaker")).ToArray();
            var outSpeaker = speakers[0];

            foreach (var o in dev)
            {

            // outSpeaker.AudioSessionManager.AudioSessionControl.
            var outSpeakerInit = new WasapiOut(o, AudioClientShareMode.Exclusive, true, 100);
            var ev = new ManualResetEvent(false);
            outSpeakerInit.PlaybackStopped += (s, e) => ev.Set();
            outSpeakerInit.Init(mp3);
            outSpeakerInit.Play();
            ev.WaitOne();
            }
           

/*
            var k = WaveOut.GetCapabilities(waveOut);

            var player = new WaveOutEvent
            {
                DeviceNumber = waveOut,
                Volume = 1.0F
            };

            ManualResetEvent ev = new ManualResetEvent(false);
            player.PlaybackStopped += (s, e) => ev.Set();
            player.Init(mp3);
            player.Play();
            ev.WaitOne();*/
            // Console.WriteLine("played!");
        }

        public void CancelCurrentSound()
        {
            // TODO
        }
    }
}

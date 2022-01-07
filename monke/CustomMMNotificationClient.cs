using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using System.Runtime.InteropServices;

namespace monke
{
    internal class CustomMMNotificationClient : IMMNotificationClient
    {
        private StandaloneAudioPlayer standaloneAudioPlayer;

        public CustomMMNotificationClient(StandaloneAudioPlayer standaloneAudioPlayer)
        {
            this.standaloneAudioPlayer = standaloneAudioPlayer;
        }

        public void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, [MarshalAs(UnmanagedType.I4)] DeviceState newState)
        {
            standaloneAudioPlayer.RecalcSpeakerId();
        }

        public void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId)
        {
            standaloneAudioPlayer.RecalcSpeakerId();
        }

        public void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string deviceId)
        {
            standaloneAudioPlayer.RecalcSpeakerId();
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, [MarshalAs(UnmanagedType.LPWStr)] string defaultDeviceId)
        {
            standaloneAudioPlayer.RecalcSpeakerId();
        }

        public void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PropertyKey key)
        {
            // don't care
            standaloneAudioPlayer.RecalcSpeakerId();
        }

    }
}
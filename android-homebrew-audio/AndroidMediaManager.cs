using System;
using Android.Media;
using Android.Content;
using Android.App;
using Android.Bluetooth;
using Android.Runtime;
using Java.Lang;

namespace android_homebrew_audio
{
    public class AndroidMediaManager : IDisposable
    {
        private static Lazy<AndroidMediaManager> instance = new Lazy<AndroidMediaManager>(() => new AndroidMediaManager((AudioManager)Application.Context.GetSystemService(Context.AudioService)));
        public static AndroidMediaManager SharedInstance => instance.Value;

        protected MediaPlayer mediaPlayer;
        public AudioManager AudioManager;
        public MyNotificationManager NotificationManager;

        public bool IsPlaying => mediaPlayer?.IsPlaying == true;

        #region Bluetooth Controls

        private MyBroadcastReceiver receiver;

        private IntentFilter intentFilter;
        private string[] intents = new string[]
        {
            Intent.ActionMediaButton,
            "this.is.a.TEST",
            BluetoothAdapter.ActionConnectionStateChanged,
            "music-controls-media-button",
            "music-controls-previous",
            "music-controls-pause",
            "music-controls-play",
            "music-controls-next",
            Android.Bluetooth.BluetoothHeadset.ActionVendorSpecificHeadsetEvent,
        };

        #endregion

        private AndroidMediaManager(AudioManager audioManager)
        {
            this.AudioManager = audioManager;
            AudioManager.RegisterMediaButtonEventReceiver(PendingIntent.GetBroadcast(Application.Context, 0, new Intent("music-controls-media-button"), PendingIntentFlags.UpdateCurrent));

            receiver = new MyBroadcastReceiver();
            intentFilter = new IntentFilter();
            foreach (var intent in intents)
            {
                intentFilter.AddAction(intent);
            }

            Application.Context.RegisterReceiver(receiver, intentFilter);

            
        }

        public void LoadMediaItem(string mediaUrl)
        {
            if(mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer();
            }
            else
            {
                mediaPlayer.Reset();
            }

            mediaPlayer.SetDataSource(mediaUrl);
            mediaPlayer.Prepare();
            mediaPlayer.Start();
            NotificationManager = new MyNotificationManager();
        }

        public void PlayPause()
        {
            if (mediaPlayer.IsPlaying)
            {
                mediaPlayer.Pause();
            }
            else
            {
                mediaPlayer.Start();
            }
        }

        public void StepForward()
        {
            mediaPlayer.SeekTo(mediaPlayer.CurrentPosition + 10000);
        }

        public void StepBackward()
        {
            mediaPlayer.SeekTo(mediaPlayer.CurrentPosition - 10000);
        }

        public void Dispose()
        {
            mediaPlayer.Release();
        }
    }
}

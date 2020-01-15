using System;
using Android.Media;
using Android.Content;

namespace android_homebrew_audio
{
    public class AndroidMediaManager : IDisposable
    {
        protected MediaPlayer mediaPlayer;
        protected AudioManager audioManager;

        public AndroidMediaManager(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public void Start(String mediaUrl)
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

        public void Dispose()
        {
            mediaPlayer.Release();
        }
    }
}

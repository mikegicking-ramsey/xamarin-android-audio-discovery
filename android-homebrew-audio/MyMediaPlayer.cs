using System;
using Android.Media;

namespace android_homebrew_audio
{
    public class MyMediaPlayer : MediaPlayer
    {
        public MyMediaPlayer()
        {
        }

        public override void Pause()
        {
            base.Pause();
        }
    }
}

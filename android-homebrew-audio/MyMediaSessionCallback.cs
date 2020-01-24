using System;
using Android.Content;
using Android.Media.Session;
using Android.Views;

namespace android_homebrew_audio
{
    public class MyMediaSessionCallback : MediaSession.Callback
    {
        public MyMediaSessionCallback()
        {
        }

        public override bool OnMediaButtonEvent(Intent mediaButtonIntent)
        {
            //return base.OnMediaButtonEvent(mediaButtonIntent);
            var buttonPressed = (KeyEvent)mediaButtonIntent.GetParcelableExtra(Intent.ExtraKeyEvent);
            if (buttonPressed.Action == KeyEventActions.Up)
            {
                switch (buttonPressed.KeyCode)
                {
                    case Keycode.MediaPause:
                    case Keycode.MediaPlay:
                    case Keycode.MediaPlayPause:
                        AndroidMediaManager.SharedInstance.PlayPause();
                        break;
                    case Keycode.MediaNext:
                    case Keycode.MediaFastForward:
                    case Keycode.MediaSkipForward:
                    case Keycode.MediaStepForward:
                        AndroidMediaManager.SharedInstance.StepForward();
                        break;
                    case Keycode.MediaPrevious:
                    case Keycode.MediaRecord:
                    case Keycode.MediaSkipBackward:
                    case Keycode.MediaStepBackward:
                        AndroidMediaManager.SharedInstance.StepBackward();
                        break;

                }
                Console.WriteLine(buttonPressed.KeyCode);
            }
            return true;
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnPlay()
        {
            base.OnPlay();
        }

        public override void OnFastForward()
        {
            base.OnFastForward();
        }

        public override void OnRewind()
        {
            base.OnRewind();
        }

        public override void OnSkipToNext()
        {
            base.OnSkipToNext();
        }

        public override void OnSkipToPrevious()
        {
            base.OnSkipToPrevious();
        }
    }
}

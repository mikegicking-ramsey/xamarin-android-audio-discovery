using System;
using System.Linq;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Views;

namespace android_homebrew_audio
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class MyBroadcastReceiver : BroadcastReceiver
    {
        public MyBroadcastReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action == "music-controls-media-button")
            {
                var buttonPressed = (KeyEvent) intent.GetParcelableExtra(Intent.ExtraKeyEvent);
                if(buttonPressed.Action == KeyEventActions.Up)
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
                }
            }
            else
            {
                Console.WriteLine("OnReceive hit!");
            }
        }
    }
}

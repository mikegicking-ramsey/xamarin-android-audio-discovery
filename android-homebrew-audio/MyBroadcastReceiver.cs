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
            if (intent.Action == "music-controls-media-button")
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
                    Console.WriteLine(buttonPressed.KeyCode);
                }
            }
            else if (intent.Action == "music-controls-next")
            {
                AndroidMediaManager.SharedInstance.StepForward();
            }
            else if (intent.Action == "music-controls-pause")
            {
                AndroidMediaManager.SharedInstance.PlayPause();
            }
            else if (intent.Action == "music-controls-play")
            {
                AndroidMediaManager.SharedInstance.PlayPause();
            }
            else if (intent.Action == "music-controls-previous")
            {
                AndroidMediaManager.SharedInstance.StepBackward();
            }
            else
            {
                Console.WriteLine("OnReceive hit!");
            }
        }
    }
}

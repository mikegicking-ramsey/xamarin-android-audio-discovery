using System;
using Android.App;
using Android.Bluetooth;
using Android.Content;
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
                Console.WriteLine(buttonPressed.KeyCode);
            }
            else
            {
                Console.WriteLine("OnReceive hit!");
            }
        }
    }
}

using System;
using Android.Bluetooth;
using Android.Content;

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
            if (intent.Action == "music-controls-next")
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
            else if (intent.Action == BluetoothAdapter.ActionConnectionStateChanged)
            {
                var connectionState = intent.GetIntExtra(BluetoothAdapter.ExtraConnectionState, -1);
                if(connectionState == (int)State.Disconnected && AndroidMediaManager.SharedInstance.IsPlaying)
                {
                    AndroidMediaManager.SharedInstance.Pause();
                }
            }
            else
            {
                Console.WriteLine("OnReceive hit!");
            }
        }
    }
}

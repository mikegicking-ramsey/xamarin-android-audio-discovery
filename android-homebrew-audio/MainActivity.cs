using System;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace android_homebrew_audio
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Bluetooth Controls
        MyBroadcastReceiver receiver;

        private IntentFilter intentFilter;
        private string[] intents = new string[]
        {
            Intent.ActionMediaButton,
            "this.is.a.TEST",
            "this.is.a.SECOND_TEST",
            BluetoothAdapter.ActionConnectionStateChanged,
            Android.Bluetooth.BluetoothDevice.ActionAclConnected,
            BluetoothDevice.ActionFound,
            "music-controls-media-button",
            Android.Bluetooth.BluetoothHeadset.ActionVendorSpecificHeadsetEvent,
            "music-controls-next"
        };
        #endregion

        #region Media Playback
        private AndroidMediaManager mediaManager;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            mediaManager = new AndroidMediaManager((AudioManager)GetSystemService(AudioService));

            receiver = new MyBroadcastReceiver();
            intentFilter = new IntentFilter();
            foreach(var intent in intents)
            {
                intentFilter.AddAction(intent);
            }

        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(receiver, intentFilter);
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterReceiver(receiver);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent message = new Intent("this.is.a.SECOND_TEST");
            SendBroadcast(message);
            return true;
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            Intent message = new Intent("this.is.a.TEST");
            SendBroadcast(message);

            mediaManager.Start("https://traffic.libsyn.com/secure/draudioarchives/07312019_the_dave_ramsey_show_archive_1.mp3");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}


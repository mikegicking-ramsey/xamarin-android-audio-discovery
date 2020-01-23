using System;
using Android.App;
using Android.Content;
using Java.Lang;

namespace android_homebrew_audio
{
    public class MyNotificationManager
    {
        public NotificationManager notificationManager;
		private Notification.Builder notificationBuilder;
		private Notification notification;

        private string channelId = "audio-manager-channel-id";

        public MyNotificationManager()
        {
            this.notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);

            ICharSequence name = new Java.Lang.String(Application.Context.ApplicationInfo.Name);
            var notificationChannel = new NotificationChannel(channelId, name, Android.App.NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(notificationChannel);

			CreateBuilder();

			notification = notificationBuilder.Build();
			notificationManager.Notify(0, notification);
        }

        public void Dispose()
        {
			notificationManager.Cancel(0);
        }

		private void CreateBuilder()
		{
			var context = Application.Context;
			var builder = new Notification.Builder(context, channelId);

			builder.SetChannelId(channelId);

			//Configure builder
			builder.SetContentTitle("track title goes here");
			builder.SetContentText("artist goes here");
			builder.SetWhen(0);

			//TODO: set if the notification can be destroyed by swiping
			//if (infos.dismissable)
			//{
			//	builder.setOngoing(false);
			//	Intent dismissIntent = new Intent("music-controls-destroy");
			//	PendingIntent dismissPendingIntent = PendingIntent.getBroadcast(context, 1, dismissIntent, 0);
			//	builder.setDeleteIntent(dismissPendingIntent);
			//}
			//else
			//{
			//	builder.setOngoing(true);
			//}
			builder.SetOngoing(true);

			//If 5.0 >= set the controls to be visible on lockscreen
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
			{
				builder.SetVisibility(NotificationVisibility.Public);
			}

			//Set SmallIcon (Notification Icon in top bar)
			builder.SetSmallIcon(Android.Resource.Drawable.IcMediaPlay);

			//TODO: Set LargeIcon
			// builder.SetLargeIcon(AlbumArt.jpg);

			//Open app if tapped
			//Intent resultIntent = new Intent(context, typeof(Application));
			//resultIntent.SetAction(Intent.ActionMain);
			//resultIntent.AddCategory(Intent.CategoryLauncher);
			//PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, 0, resultIntent, 0);
			//builder.SetContentIntent(resultPendingIntent);

			//Controls
			var controlsCount = 0;

            /* Previous  */
            controlsCount++;
            Intent previousIntent = new Intent("music-controls-previous");
            PendingIntent previousPendingIntent = PendingIntent.GetBroadcast(context, 1, previousIntent, 0);
			Notification.Action previousAction = new Notification.Action(Android.Resource.Drawable.IcMediaRew, "", previousPendingIntent);
            builder.AddAction(previousAction);

            /* Play/Pause */
            if (AndroidMediaManager.SharedInstance.IsPlaying)
            {
				controlsCount++;
				Intent pauseIntent = new Intent("music-controls-pause");
				PendingIntent pausePendingIntent = PendingIntent.GetBroadcast(context, 1, pauseIntent, 0);
				Notification.Action pauseAction = new Notification.Action(Android.Resource.Drawable.IcMediaPause, "", pausePendingIntent);
				builder.AddAction(pauseAction);
			}
            else
            {
				controlsCount++;
				Intent playIntent = new Intent("music-controls-play");
				PendingIntent playPendingIntent = PendingIntent.GetBroadcast(context, 1, playIntent, 0);
				Notification.Action playAction = new Notification.Action(Android.Resource.Drawable.IcMediaRew, "", playPendingIntent);
				builder.AddAction(playAction);
			}

			/* Next */
			controlsCount++;
			Intent nextIntent = new Intent("music-controls-next");
			PendingIntent nextPendingIntent = PendingIntent.GetBroadcast(context, 1, nextIntent, 0);
			Notification.Action nextAction = new Notification.Action(Android.Resource.Drawable.IcMediaFf, "", nextPendingIntent);
			builder.AddAction(nextAction);

			///* Close */
			//if (infos.hasClose)
			//{
			//	nbControls++;
			//	Intent destroyIntent = new Intent("music-controls-destroy");
			//	PendingIntent destroyPendingIntent = PendingIntent.getBroadcast(context, 1, destroyIntent, 0);
			//	builder.addAction(this.getResourceId(infos.closeIcon, android.R.drawable.ic_menu_close_clear_cancel), "", destroyPendingIntent);
			//}

			//If 5.0 >= use MediaStyle
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
			{
				int[] args = new int[controlsCount];
				for (int i = 0; i < controlsCount; ++i)
				{
					args[i] = i;
				}
				var style = new Notification.MediaStyle();
				style.SetShowActionsInCompactView(args);
				builder.SetStyle(style);
				//builder.SetStyle(new Notification.MediaStyle().SetShowActionsInCompactView(args));
			}
			this.notificationBuilder = builder;
		}
	}
}

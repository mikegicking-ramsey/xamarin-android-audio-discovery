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

		#region Media Control Actions

		private Notification.Action previousAction;
		private Notification.Action nextAction;
		private Notification.Action skipBackwardAction;
		private Notification.Action skipForwardAction;
		private Notification.Action playAction;
		private Notification.Action pauseAction;
		private Notification.Action destroyAction;

        #endregion

        public MyNotificationManager()
        {
            notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
        }

        public void InitializeIfNeeded()
        {
			if (notificationBuilder == null || notification == null)
			{
				ICharSequence name = new Java.Lang.String(Application.Context.ApplicationInfo.Name);
				var notificationChannel = new NotificationChannel(channelId, name, Android.App.NotificationImportance.Default);
				notificationManager.CreateNotificationChannel(notificationChannel);

				CreateActions();

				CreateBuilder();

				notification = notificationBuilder.Build();
			}
		}

        public void Notify()
        {
			notificationManager.Notify(0, notification);
		}

        public void Dispose()
        {
			notificationManager.Cancel(0);
        }

        //TODO: Toggle play/pause icons

        private void CreateActions()
        {
			var context = Application.Context;

			var previousIntent = new Intent("music-controls-previous");
			var previousPendingIntent = PendingIntent.GetBroadcast(context, 1, previousIntent, 0);
			previousAction = new Notification.Action(Android.Resource.Drawable.IcMediaPrevious, "", previousPendingIntent);

			var nextIntent = new Intent("music-controls-next");
			var nextPendingIntent = PendingIntent.GetBroadcast(context, 1, nextIntent, 0);
			nextAction = new Notification.Action(Android.Resource.Drawable.IcMediaNext, "", nextPendingIntent);

			var skipBackwardIntent = new Intent("music-controls-skip-backward");
			var skipBackwardPendingIntent = PendingIntent.GetBroadcast(context, 1, skipBackwardIntent, 0);
			skipBackwardAction = new Notification.Action(Android.Resource.Drawable.IcMediaRew, "", skipBackwardPendingIntent);

			var skipForwardIntent = new Intent("music-controls-skip-forward");
			var skipForwardPendingIntent = PendingIntent.GetBroadcast(context, 1, skipForwardIntent, 0);
			skipForwardAction = new Notification.Action(Android.Resource.Drawable.IcMediaFf, "", skipForwardPendingIntent);

			var playIntent = new Intent("music-controls-play");
			var playPendingIntent = PendingIntent.GetBroadcast(context, 1, playIntent, 0);
			playAction = new Notification.Action(Android.Resource.Drawable.IcMediaRew, "", playPendingIntent);

			var pauseIntent = new Intent("music-controls-pause");
			var pausePendingIntent = PendingIntent.GetBroadcast(context, 1, pauseIntent, 0);
			pauseAction = new Notification.Action(Android.Resource.Drawable.IcMediaPause, "", pausePendingIntent);

			var destroyIntent = new Intent("music-controls-destroy");
			var destroyPendingIntent = PendingIntent.GetBroadcast(context, 1, destroyIntent, 0);
			destroyAction = new Notification.Action(Android.Resource.Drawable.IcMenuCloseClearCancel, "", destroyPendingIntent);
		}

		private void CreateBuilder()
		{
			var builder = new Notification.Builder(Application.Context, channelId);

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

			//TODO: Open app if tapped
			//Intent resultIntent = new Intent(context, typeof(Application));
			//resultIntent.SetAction(Intent.ActionMain);
			//resultIntent.AddCategory(Intent.CategoryLauncher);
			//PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, 0, resultIntent, 0);
			//builder.SetContentIntent(resultPendingIntent);

			//Controls
			var controlsCount = 0;

            /* Previous  */
            controlsCount++;
            builder.AddAction(previousAction);

            /* Play/Pause */
            if (AndroidMediaManager.SharedInstance.IsPlaying)
            {
				controlsCount++;
				builder.AddAction(pauseAction);
			}
            else
            {
				controlsCount++;
				builder.AddAction(playAction);
			}

			/* Next */
			controlsCount++;
			builder.AddAction(nextAction);

            /* Close */
			controlsCount++;
            builder.AddAction(destroyAction);

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
			}
			this.notificationBuilder = builder;
		}
	}
}

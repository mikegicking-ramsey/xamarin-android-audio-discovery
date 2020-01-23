using Android.OS;
using Android.Support.V4.Media.Session;

namespace android_homebrew_audio
{
    public class MediaSessionCallback : MediaSessionCompat.Callback
    {
		public override void OnPlay()
		{
			base.OnPlay();
		}

		public override void OnSkipToQueueItem(long id)
		{
			base.OnSkipToQueueItem(id);
		}

		public override void OnSeekTo(long pos)
		{
			base.OnSeekTo(pos);
		}

		public override void OnPlayFromMediaId(string mediaId, Bundle extras)
		{
			base.OnPlayFromMediaId(mediaId, extras);
		}

		public override void OnPause()
		{
			base.OnPause();
		}

		public override void OnStop()
		{
			base.OnStop();
		}

		public override void OnSkipToNext()
		{
			base.OnSkipToNext();
		}

		public override void OnSkipToPrevious()
		{
			base.OnSkipToPrevious();
		}

        public override void OnFastForward()
        {
            base.OnFastForward();
        }

        public override void OnRewind()
        {
            base.OnRewind();
        }
    }
}


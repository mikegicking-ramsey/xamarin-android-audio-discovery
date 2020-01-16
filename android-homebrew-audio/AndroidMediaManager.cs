using System;
using Android.Media;
using Android.Content;
using Android.App;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Util;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Extractor;

namespace android_homebrew_audio
{
    public class AndroidMediaManager : IDisposable
    {
        SimpleExoPlayer player;

        public AndroidMediaManager()
        {
            player = ExoPlayerFactory.NewSimpleInstance(Application.Context);
        }

        public void Start(String mediaUrl)
        {
            player.Prepare(GetMediaSourceFromUri(mediaUrl));
            player.PlayWhenReady = true;
        }

        public void PlayPause()
        {
            
        }

        public void Dispose()
        {
        }

        internal IMediaSource GetMediaSourceFromUri(string mediaUri)
        {
            var userAgent = Util.GetUserAgent(Application.Context, "ApplicationName");
            var dataSourceFactory = new DefaultHttpDataSourceFactory(userAgent, new DefaultBandwidthMeter());
            var extractorFactory = new DefaultExtractorsFactory();
            return new ExtractorMediaSource(Android.Net.Uri.Parse(mediaUri), dataSourceFactory, extractorFactory, null, null);
        }
    }
}

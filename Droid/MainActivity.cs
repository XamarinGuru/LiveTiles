using Android.App;
using Android.Widget;
using Android.OS;

namespace LiveTiles.Droid
{
	[Activity(Label = "LiveTiles", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			//var webView = mView.FindViewById<WebView>(Resource.Id.webView);

			//webView.Settings.JavaScriptEnabled = true;
			//webView.Settings.AllowContentAccess = true;
			//webView.Settings.EnableSmoothTransition();
			//webView.Settings.LoadsImagesAutomatically = true;
			//webView.Settings.SetGeolocationEnabled(true);
			//webView.SetWebViewClient(new WebViewClient());
			//webView.SetBackgroundColor(Android.Graphics.Color.Transparent);

			//webView.ClearCache(true);
			//webView.ClearHistory();

			//var url = string.Format(Constants.URL_GAUGE, AppSettings.UserID);
			//webView.LoadUrl(url);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = $"{count++} clicks!"; };
		}
	}
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidHUD;

namespace LiveTiles.Droid
{
	[Activity(Label = "LiveTilesHomeAC")]
	public class LiveTilesHomeAC : Activity
	{
		WebView LTWebView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.LiveTilesHomeLayout);

			InitUISettings();
		}

		void InitUISettings()
		{
			var navBar = FindViewById<LinearLayout>(Resource.Id.navBar);
			navBar.Visibility = ViewStates.Gone;

			FindViewById<LinearLayout>(Resource.Id.ActionBack).Click += ActionBack;
			FindViewById<LinearLayout>(Resource.Id.ActionRefresh).Click += ActionRefresh;
			FindViewById<LinearLayout>(Resource.Id.ActionSettings).Click += ActionSettings;

			LTWebView = FindViewById<WebView>(Resource.Id.LTWebView);

			LTWebView.Settings.JavaScriptEnabled = true;
			LTWebView.Settings.AllowContentAccess = true;
			LTWebView.Settings.EnableSmoothTransition();
			LTWebView.Settings.LoadsImagesAutomatically = true;
			LTWebView.Settings.SetGeolocationEnabled(true);

			LTWebView.SetBackgroundColor(Android.Graphics.Color.Transparent);
			
			LTWebView.ClearCache(true);
			LTWebView.ClearHistory();

			var webviewClient = new WebViewClient();
			LTWebView.SetWebViewClient(new MMWebViewClient(this, navBar));

			LTWebView.LoadUrl(Constants.URL_LIVETILES);
		}

		void HandleLoadFinished(WebView arg1, string arg2, Bitmap arg3)
		{
			//throw new NotImplementedException();
		}

		void ActionBack(object sender, EventArgs e)
		{
			if (LTWebView.CanGoBack())
				LTWebView.GoBack();
		}

		void ActionRefresh(object sender, EventArgs e)
		{
			LTWebView.Reload();
		}

		void ActionSettings(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		public void ShowLoadingView()
		{
			AndHUD.Shared.Show(this, null, -1, MaskType.Black);
		}

		public void HideLoadingView()
		{
			AndHUD.Shared.Dismiss(this);
		}

		private class MMWebViewClient : WebViewClient
		{
			LiveTilesHomeAC _act;
			LinearLayout _navBar;

			public MMWebViewClient(LiveTilesHomeAC act, LinearLayout navBar)
			{
				_act = act;
				_navBar = navBar;
			}

			public override void OnPageStarted(WebView view, String url, Bitmap favicon)
			{
				base.OnPageStarted(view, url, favicon);

				_act.ShowLoadingView();
			}

			public override void OnPageFinished(WebView view, String url)
			{
				base.OnPageFinished(view, url);

				if (url.Contains(Constants.SYMBOL_LOGIN))
					_navBar.Visibility = ViewStates.Gone;
				else
					_navBar.Visibility = ViewStates.Visible;

				string cssString = Constants.INJECT_CSS;
				string jsString = Constants.INJECT_JS;
				string jsWithCSS = string.Format(jsString, cssString);
				view.EvaluateJavascript(jsWithCSS, null);

				_act.HideLoadingView();
			}
		}
	}
}

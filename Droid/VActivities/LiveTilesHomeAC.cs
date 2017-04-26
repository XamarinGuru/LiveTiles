
using System;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace LiveTiles.Droid
{
	[Activity(Label = "LiveTilesHomeAC", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
	public class LiveTilesHomeAC : Activity
	{
		LinearLayout settingsMenu;
		WebView LTWebView;
		bool isLoggedOut = false;
		LinearLayout _navBar;
		LinearLayout linearProgress;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.LiveTilesHomeLayout);

			InitUISettings();

			string email = Intent.GetStringExtra("EMAIL");

			LTWebView.SetWebViewClient(new MMWebViewClient(linearProgress));
			LTWebView.SetWebChromeClient(new MMWebChromeClient(this, _navBar, linearProgress, email));

			//var homepageURL = AppStatus.MxData.homepageURL;
			var homepageURL = AppStatus.LatestURL;
			if (AppStatus.MxData != null && !string.IsNullOrEmpty(AppStatus.MxData.homepageURL))
			{
				homepageURL = AppStatus.MxData.homepageURL;
				AppStatus.LatestURL = AppStatus.MxData.homepageURL;
			}

			LTWebView.LoadUrl(homepageURL);
		}

		void InitUISettings()
		{
			_navBar = FindViewById<LinearLayout>(Resource.Id.navBar);
			_navBar.SetBackgroundColor(
				Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_NAVIGATION_BAR_BACKGROUND)
				)
			);
			_navBar.Visibility = ViewStates.Gone;

			settingsMenu = FindViewById<LinearLayout>(Resource.Id.settingsMenu);
			settingsMenu.SetBackgroundColor(
				Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_MENU_BACKGROUND)
				)
			);
			settingsMenu.Visibility = ViewStates.Gone;

			FindViewById<TextView>(Resource.Id.txtStartPage).SetTextColor(
				Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_MENU_TEXT_BACKGROUND)
				)
			);
			FindViewById<TextView>(Resource.Id.txtLogOut).SetTextColor(
				Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_MENU_TEXT_BACKGROUND)
				)
			);
			FindViewById<TextView>(Resource.Id.txtBuiltWith).SetTextColor(
				Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_MENU_TEXT_BACKGROUND)
				)
			);

			FindViewById<ImageView>(Resource.Id.imgLogo).SetImageResource(Resource.Drawable.icon_logo);

			FindViewById<LinearLayout>(Resource.Id.ActionBack).Click += ActionBack;
			FindViewById<LinearLayout>(Resource.Id.ActionRefresh).Click += ActionRefresh;
			FindViewById<LinearLayout>(Resource.Id.ActionSettings).Click += ActionSettings;

			FindViewById<LinearLayout>(Resource.Id.ActionStartPage).Click += ActionStartPage;
			FindViewById<LinearLayout>(Resource.Id.ActionLogOut).Click += ActionLogOut;

			linearProgress = FindViewById<LinearLayout>(Resource.Id.linearProgress);

			LTWebView = FindViewById<WebView>(Resource.Id.LTWebView);

			LTWebView.Settings.JavaScriptEnabled = true;
			LTWebView.Settings.AllowContentAccess = true;
			LTWebView.Settings.EnableSmoothTransition();
			LTWebView.Settings.LoadsImagesAutomatically = true;
			LTWebView.Settings.SetGeolocationEnabled(true);
			LTWebView.SetBackgroundColor(Color.Transparent);
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
			SettingsMenuAnimation();
		}

		void ActionStartPage(object sender, EventArgs e)
		{
			LTWebView.LoadUrl(AppStatus.LatestURL);
			SettingsMenuAnimation();
		}

		void ActionLogOut(object sender, EventArgs e)
		{
			LTWebView.LoadUrl(AppStatus.LatestURL);
			SettingsMenuAnimation();

			CookieSyncManager.CreateInstance(this);
			CookieManager cookieManager = CookieManager.Instance;
			cookieManager.RemoveAllCookie();
			cookieManager.RemoveAllCookies(null);
			cookieManager.RemoveSessionCookies(null);

			LTWebView.ClearCache(true);
			LTWebView.ClearHistory();

			this.DeleteDatabase("webview.db");
			this.DeleteDatabase("webviewCache.db");

			isLoggedOut = true;
			_navBar.Visibility = ViewStates.Gone;
			AppStatus.IsLoggedIn = false;
			AppStatus.MxData = null;
			Finish();
		}


		void SettingsMenuAnimation()
		{
			if (settingsMenu.Visibility.Equals(ViewStates.Gone))
			{
				settingsMenu.Visibility = ViewStates.Visible;

				int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
				int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
				settingsMenu.Measure(widthSpec, heightSpec);

				ValueAnimator mAnimator = slideAnimator(0, settingsMenu.MeasuredHeight, settingsMenu);
				mAnimator.Start();
			}
			else
			{
				int finalHeight = settingsMenu.Height;

				ValueAnimator mAnimator = slideAnimator(finalHeight, 0, settingsMenu);
				mAnimator.Start();
				mAnimator.AnimationEnd += (object IntentSender, EventArgs arg) =>
				{
					settingsMenu.Visibility = ViewStates.Gone;
				};
			}
		}
		ValueAnimator slideAnimator(int start, int end, LinearLayout content)
		{
			ValueAnimator animator = ValueAnimator.OfInt(start, end);
			animator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) =>
			{
				var value = (int)animator.AnimatedValue;
				ViewGroup.LayoutParams layoutParams = content.LayoutParameters;
				layoutParams.Height = value;
				content.LayoutParameters = layoutParams;
			};
			return animator;
		}

		private class MMWebViewClient : WebViewClient
		{
			LinearLayout _ProgressBar;

			public MMWebViewClient(LinearLayout linearProgress)
			{
				_ProgressBar = linearProgress;
			}

			public override void OnPageStarted(WebView view, String url, Bitmap favicon)
			{
				if (_ProgressBar != null)
				{
					_ProgressBar.Visibility = ViewStates.Visible;
				}

				base.OnPageStarted(view, url, favicon);
			}

			public override void OnReceivedError(WebView view, IWebResourceRequest request, WebResourceError error)
			{
				if (_ProgressBar != null)
				{
					_ProgressBar.Visibility = ViewStates.Gone;
				}

				base.OnReceivedError(view, request, error);
			}
		}

		private class MMWebChromeClient : WebChromeClient
		{
			LiveTilesHomeAC _act;
			LinearLayout _navBar;
			LinearLayout _ProgressBar;
			string _email;

			public MMWebChromeClient(LiveTilesHomeAC act, LinearLayout navBar, LinearLayout linearProgress, string email)
			{
				_act = act;
				_navBar = navBar;
				_ProgressBar = linearProgress;
				_email = email;
			}

			public override void OnProgressChanged(WebView view, int newProgress)
			{
				base.OnProgressChanged(view, newProgress);

				if (newProgress > 50)
				{
					if (_ProgressBar != null)
					{
						_ProgressBar.Visibility = ViewStates.Gone;
					}

					if (_act.isLoggedOut) return;

					CookieSyncManager.Instance.Sync();

					var strURL = view.Url;
					if (strURL.Contains(AppSettings.SYMBOL_LOGIN))
					{
						_navBar.Visibility = ViewStates.Gone;
						AppStatus.IsLoggedIn = false;

						view.LoadUrl(string.Format(AppSettings.INJECT_JS_FILL_EMAIL, _email));
					}
					else
					{
						_navBar.Visibility = ViewStates.Visible;
						AppStatus.IsLoggedIn = true;
					}

					string cssString = AppSettings.INJECT_CSS_HIDE_TOP_BAR;
					string jsString = AppSettings.INJECT_JS_HIDE_BOTTOM_BAR;
					string jsWithCSS = string.Format(jsString, cssString);
					view.EvaluateJavascript(jsWithCSS, null);
				}
			}
		}
	}
}

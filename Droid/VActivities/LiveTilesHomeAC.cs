
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
using AndroidHUD;

namespace LiveTiles.Droid
{
	[Activity(Label = "LiveTilesHomeAC", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
	public class LiveTilesHomeAC : Activity
	{
		LinearLayout settingsMenu;
		WebView LTWebView;
		bool isLoggedOut = false;
		LinearLayout _navBar;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.LiveTilesHomeLayout);

			InitUISettings();
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

			LTWebView = FindViewById<WebView>(Resource.Id.LTWebView);

			LTWebView.Settings.JavaScriptEnabled = true;
			LTWebView.Settings.AllowContentAccess = true;
			LTWebView.Settings.EnableSmoothTransition();
			LTWebView.Settings.LoadsImagesAutomatically = true;
			LTWebView.Settings.SetGeolocationEnabled(true);

			LTWebView.SetBackgroundColor(Color.Transparent);

			var linearProgress = FindViewById<LinearLayout>(Resource.Id.linearProgress);
			LTWebView.SetWebViewClient(new MMWebViewClient(this, _navBar, isLoggedOut, linearProgress));

			LTWebView.LoadUrl(AppSettings.URL_BASE);
		}



		//void HandleLoadFinished(WebView arg1, string arg2, Bitmap arg3)
		//{
		//	//throw new NotImplementedException();
		//}

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
			LTWebView.LoadUrl(AppSettings.URL_BASE);
			SettingsMenuAnimation();
		}

		void ActionLogOut(object sender, EventArgs e)
		{
			LTWebView.LoadUrl(AppSettings.URL_LOGOUT);
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
		private ValueAnimator slideAnimator(int start, int end, LinearLayout content)
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
			LinearLayout _ProgressBar;
			bool _isLoggedOut = false;

			public MMWebViewClient(LiveTilesHomeAC act, LinearLayout navBar, bool isLoggedOut, LinearLayout linearProgress)
			{
				_act = act;
				_navBar = navBar;
				_ProgressBar = linearProgress;
				_isLoggedOut = isLoggedOut;
			}

			public override void OnPageStarted(WebView view, String url, Bitmap favicon)
			{
				if (_ProgressBar != null)
				{
					_ProgressBar.Visibility = ViewStates.Visible;
				}

				base.OnPageStarted(view, url, favicon);

				//_act.ShowLoadingView();
			}

			public override void OnPageFinished(WebView view, String url)
			{
				base.OnPageFinished(view, url);

				if (_ProgressBar != null)
				{
					_ProgressBar.Visibility = ViewStates.Gone;
				}

				if (_act.isLoggedOut) return;
				//HideLoadingView()

				CookieSyncManager.Instance.Sync();

				if (url.Contains(Constants.SYMBOL_LOGIN))
				{
					_navBar.Visibility = ViewStates.Gone;
					AppStatus.IsLoggedIn = false;
				}
				else
				{
					_navBar.Visibility = ViewStates.Visible;
					AppStatus.IsLoggedIn = true;
				}

				string cssString = Constants.INJECT_CSS;
				string jsString = Constants.INJECT_JS;
				string jsWithCSS = string.Format(jsString, cssString);
				view.EvaluateJavascript(jsWithCSS, null);

				//_act.HideLoadingView();
			}
		}
	}
}

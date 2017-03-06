
using System;
using Android.Animation;
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
		LinearLayout settingsMenu;
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
			navBar.SetBackgroundColor(
				Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_NAVIGATION_BAR_BACKGROUND)
				)
			);
			navBar.Visibility = ViewStates.Gone;

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
			
			LTWebView.ClearCache(true);
			LTWebView.ClearHistory();

			LTWebView.SetWebViewClient(new MMWebViewClient(this, navBar));

			LTWebView.LoadUrl(AppSettings.URL_BASE);
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

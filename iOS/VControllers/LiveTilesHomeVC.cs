using Foundation;
using System;
using UIKit;
using CoreGraphics;
using WebKit;

namespace LiveTiles.iOS
{
	public partial class LiveTilesHomeVC : BaseViewController
	{
		public string _email;

		bool hasMenu = false;
		bool isLoggedOut = false;

		public LiveTilesHomeVC() : base()
		{
		}
		public LiveTilesHomeVC(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitUISettings();

			LTWebView.LoadStarted += HandleLoadStarted;
			LTWebView.LoadError += HandleLoadError;
			LTWebView.LoadFinished += HandleLoadFinished;

			var url = AppStatus.LatestURL;
			if (!string.IsNullOrEmpty(_email))
			{
				url = _email.Equals(AppSettings.DEMO_EMAIL) ? AppSettings.URL_DEMO : AppSettings.URL_BASE;
				AppStatus.LatestURL = url;
			}

			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
		}

		void InitUISettings()
		{
			UINavigationBar.Appearance.BarTintColor = ColorFromValue(AppSettings.COLOR_LOGIN_BACKGROUND);
			NavigationController.NavigationBar.Hidden = true;

			var leftButton = new UIButton(new CGRect(0, 0, 20, 20));
			leftButton.SetImage(UIImage.FromFile("icon_back.png"), UIControlState.Normal);
			leftButton.TouchUpInside += (sender, e) => GoToBack();
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

			//refresh button
			var iconReload = new UIButton(new CGRect(0, 0, 20, 20));
			iconReload.SetImage(UIImage.FromFile("icon_reload.png"), UIControlState.Normal);
			iconReload.TouchUpInside += (sender, e) => Refresh();

			//settings button
			var iconSettings = new UIButton(new CGRect(0, 0, 20, 20));
			iconSettings.SetImage(UIImage.FromFile("icon_settings.png"), UIControlState.Normal);
			iconSettings.TouchUpInside += (sender, e) => GoToSettings();

			UIBarButtonItem[] rightButtons = { new UIBarButtonItem(iconSettings), new UIBarButtonItem(iconReload) };

			NavigationItem.RightBarButtonItems = rightButtons;

			imgLogo.Image = UIImage.FromFile(AppSettings.LOGO_IMG_NAME);

			menuContent.BackgroundColor = ColorFromValue(AppSettings.COLOR_MENU_BACKGROUND);
			menuContent.Alpha = 0;
			heightMenu.Constant = 0;

			lblStartPage.TextColor = ColorFromValue(AppSettings.COLOR_MENU_TEXT_BACKGROUND);
			lblLogOut.TextColor = ColorFromValue(AppSettings.COLOR_MENU_TEXT_BACKGROUND);
			lblBuiltWith.TextColor = ColorFromValue(AppSettings.COLOR_MENU_TEXT_BACKGROUND);
		}

		void GoToSettings()
		{
			this.View.LayoutIfNeeded();
			menuContent.LayoutIfNeeded();

			UIView.BeginAnimations("ds");
			UIView.SetAnimationDuration(0.3f);

			menuContent.Alpha = hasMenu ? 0 : 1;
			heightMenu.Constant = hasMenu ? 0 : 230;

			View.LayoutIfNeeded();
			UIView.CommitAnimations();

			hasMenu = !hasMenu;
		}

		void Refresh()
		{
			LTWebView.Reload();
		}

		void GoToBack()
		{
			if (LTWebView.CanGoBack)
				LTWebView.GoBack();
		}

		partial void ActionStartPage(UIButton sender)
		{
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(AppStatus.LatestURL)));
			CloseMenu();
		}

		partial void ActionLogOut(UIButton sender)
		{
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(AppStatus.LatestURL)));

			NSUrlCache.SharedCache.RemoveAllCachedResponses();
			foreach (NSHttpCookie cookie in NSHttpCookieStorage.SharedStorage.Cookies)
			{
				NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
			}
			isLoggedOut = true;
			NavigationController.NavigationBar.Hidden = true;
			AppStatus.IsLoggedIn = false;
			CloseMenu();

			LoginVC ltNVC = Storyboard.InstantiateViewController("LoginVC") as LoginVC;
			this.PresentViewController(ltNVC, true, null);
		}


		#region webview delegate
		void HandleLoadStarted(object sender, EventArgs e)
		{
			var b = (sender as UIWebView).IsLoading;
			_ProgressBar.Hidden = false;

			//var strURL = request.Url.AbsoluteString;

			//if (strURL.Contains("login"))
			//	NavigationController.NavigationBar.Hidden = true;
			//else
			//	NavigationController.NavigationBar.Hidden = false;
		}

		void HandleLoadError(object sender, UIWebErrorArgs e)
		{
			_ProgressBar.Hidden = true;
		}

		void HandleLoadFinished(object sender, EventArgs e)
		{
			//var b = (sender as UIWebView).IsLoading;
			_ProgressBar.Hidden = true;

			if (isLoggedOut) return;

			bool isLoggedIn;
			var strURL = LTWebView.Request.Url.AbsoluteString;

			if (strURL.Contains(Constants.SYMBOL_LOGIN))
			{
				isLoggedIn = false;

				LTWebView.EvaluateJavascript(string.Format(Constants.INJECT_JS_FILL_EMAIL, _email));
			}
			else
			{
				isLoggedIn = true;
			}

			NavigationController.NavigationBar.Hidden = !isLoggedIn;
			AppStatus.IsLoggedIn = isLoggedIn;

			string cssString = Constants.INJECT_CSS_HIDE_TOP_BAR;
			string jsString = Constants.INJECT_JS_HIDE_BOTTOM_BAR;
			string jsWithCSS = string.Format(jsString, cssString);
			LTWebView.EvaluateJavascript(jsWithCSS);

			CloseMenu();
		}

		void CloseMenu()
		{
			menuContent.Alpha = 0;
			heightMenu.Constant = 0;

			View.LayoutIfNeeded();

			hasMenu = false;
		}
  		#endregion
	}
}
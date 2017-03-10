using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace LiveTiles.iOS
{
	public partial class LiveTilesHomeVC : BaseViewController
	{
		bool hasMenu = false;

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

			LTWebView.ShouldStartLoad += HandleShouldStartLoad;
			LTWebView.LoadFinished += HandleLoadFinished;

			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(AppSettings.URL_BASE)));
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
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(AppSettings.URL_BASE)));
			CloseMenu();
		}

		partial void ActionLogOut(UIButton sender)
		{
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(AppSettings.URL_LOGOUT)));
			CloseMenu();
		}


		#region webview delegate
		bool HandleShouldStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			//ShowLoadingView("Loading...");

			//var strURL = request.Url.AbsoluteString;

			//if (strURL.Contains("login"))
			//	NavigationController.NavigationBar.Hidden = true;
			//else
			//	NavigationController.NavigationBar.Hidden = false;
			return true;
		}

		void HandleLoadFinished(object sender, EventArgs e)
		{
			//HideLoadingView();

			var strURL = LTWebView.Request.Url.AbsoluteString;

			if (strURL.Contains(Constants.SYMBOL_LOGIN))
				NavigationController.NavigationBar.Hidden = true;
			else
				NavigationController.NavigationBar.Hidden = false;

			string cssString = Constants.INJECT_CSS;
			string jsString = Constants.INJECT_JS;
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
using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace LiveTiles.iOS
{
	public partial class LiveTilesHomeVC : BaseViewController
	{
		public LiveTilesHomeVC() : base()
		{
		}
		public LiveTilesHomeVC(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationController.NavigationBar.Hidden = true;

			NavigationController.NavigationBar.TintColor = UIColor.Red;

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

			LTWebView.ShouldStartLoad += HandleShouldStartLoad;
			LTWebView.LoadFinished += HandleLoadFinished;

			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(Constants.URL_LIVETILES)));
		}

		void GoToSettings()
		{
			//throw new NotImplementedException();
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

			if (strURL.Contains("login"))
				NavigationController.NavigationBar.Hidden = true;
			else
				NavigationController.NavigationBar.Hidden = false;

			string cssString = Constants.INJECT_CSS;
			string jsString = Constants.INJECT_JS;
			string jsWithCSS = string.Format(jsString, cssString);
			LTWebView.EvaluateJavascript(jsWithCSS);
		}
  		#endregion
	}
}
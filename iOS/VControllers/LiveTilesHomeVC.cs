using Foundation;
using System;
using UIKit;
using CoreGraphics;
using WebKit;

namespace LiveTiles.iOS
{
	public partial class LiveTilesHomeVC : BaseViewController
	{
        const int _buttonDimensions = 40;

		public string _email;
        bool _hasMenu = false;
        bool _isLoggedOut = false;
        nfloat _originalCenterY;
        nfloat _offscreenCenterY;
        AppleAppSettings _appSettings = AppleAppSettings.Instance;
        string _url;
        string _prevUrl;

		public LiveTilesHomeVC() : base()
		{
		}

		public LiveTilesHomeVC(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            
			LTWebView.LoadStarted += HandleLoadStarted;
			LTWebView.LoadError += HandleLoadError;
			LTWebView.LoadFinished += HandleLoadFinished;

            _appSettings.Load();

            InitUISettings();

            var homepageURL = _appSettings.LatestUrl;
			if (AppSettingsBase.OverrideUrl != null || _appSettings.MxData != null && !string.IsNullOrEmpty(_appSettings.MxData.homepageURL))
			{
				homepageURL = AppSettingsBase.OverrideUrl ?? _appSettings.MxData.homepageURL;
				_appSettings.LatestUrl = homepageURL;
			}
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(homepageURL)));
		}
        
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            _originalCenterY = menuContent.Center.Y;
            _offscreenCenterY = menuContent.Center.Y - (menuContent.Frame.Height + NavigationController.NavigationBar.Frame.Height + 20);

            UIColor color = ColorFromValue(_appSettings.FeatureColor);
            NavigationController.NavigationBar.BarTintColor = color;

            ShowMenu(false);
        }

        string ReloadIconName()
        {
            // Menu icons are colored opposite of the theme
            if (_appSettings.Theme == ThemeStyles.Light)
                return "refresh-icon-white.png";
            else
                return "refresh-icon.png";
        }

        string BackIconName()
        {
			// Menu icons are colored opposite of the theme
			if (_appSettings.Theme == ThemeStyles.Light)
				return "back-icon-white.png";
			else
				return "back-icon.png";
        }

		string MoreIconName()
		{
			// Menu icons are colored opposite of the theme
			if (_appSettings.Theme == ThemeStyles.Light)
				return "more-icon-white.png";
			else
				return "more-icon.png";
		}

		void InitUISettings()
        {
            // Back button
            var leftButton = new UIButton(new CGRect(0, 0, _buttonDimensions, _buttonDimensions));
            leftButton.SetImage(UIImage.FromFile(BackIconName()), UIControlState.Normal);
            leftButton.TouchUpInside += (sender, e) => GoToBack();
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

            // Refresh button
            var iconReload = new UIButton(new CGRect(0, 0, _buttonDimensions, _buttonDimensions));
            iconReload.SetImage(UIImage.FromFile(ReloadIconName()), UIControlState.Normal);
            iconReload.TouchUpInside += (sender, e) => Refresh();

            // Settings button
            var iconSettings = new UIButton(new CGRect(0, 0, _buttonDimensions, _buttonDimensions));
            iconSettings.SetImage(UIImage.FromFile(MoreIconName()), UIControlState.Normal);
            iconSettings.TouchUpInside += (sender, e) => AnimateMenuToggle();

            UIBarButtonItem[] rightButtons = { new UIBarButtonItem(iconSettings), new UIBarButtonItem(iconReload) };
            NavigationItem.RightBarButtonItems = rightButtons;

            // TODO Delete this OR set it to last visited icon
            // imgLogo.Image = UIImage.FromFile(AppSettings.LogoName);

            // Menu items            
            //lblStartPage.TextColor = ColorFromValue("000000");
            //lblStartPage.Font = UIFont.FromName("Lato", 17f);

            //lblLogOut.TextColor = ColorFromValue("000000");
            //lblLogOut.Font = UIFont.FromName("Lato", 17f);
        }

        void ShowMenu(bool show)
        {            
            nfloat centerPosition = show ? _originalCenterY : _offscreenCenterY;
            menuContent.Center = new CGPoint(menuContent.Center.X, centerPosition);
            _hasMenu = show;
        }

        void AnimateMenuToggle()
		{
			this.View.LayoutIfNeeded();
			menuContent.LayoutIfNeeded();

			UIView.BeginAnimations("ds");
			UIView.SetAnimationDuration(0.3f);

            ShowMenu(!_hasMenu);

			View.LayoutIfNeeded();
			UIView.CommitAnimations();			
		}

		void Refresh()
		{
			LTWebView.Reload();
		}

        void GoToBack()
        {
            if (LTWebView.CanGoBack && !_prevUrl.Contains(Constants.LoginUrl))
                LTWebView.GoBack();
        }

		partial void ActionStartPage(UIButton sender)
		{
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(_appSettings.LatestUrl)));
			AnimateMenuToggle();
		}

		partial void ActionLogOut(UIButton sender)
		{
			LTWebView.LoadRequest(new NSUrlRequest(new NSUrl(_appSettings.LatestUrl)));

			NSUrlCache.SharedCache.RemoveAllCachedResponses();
			foreach (NSHttpCookie cookie in NSHttpCookieStorage.SharedStorage.Cookies)
			{
				NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
			}
			_isLoggedOut = true;
			NavigationController.NavigationBar.Hidden = true;
			_appSettings.IsLoggedIn = false;
			_appSettings.MxData = null;
            _appSettings.Save();
			CloseMenu();

			LoginVC ltNVC = Storyboard.InstantiateViewController("LoginVC") as LoginVC;
			this.PresentViewController(ltNVC, true, null);
		}


		#region webview delegate
		void HandleLoadStarted(object sender, EventArgs e)
		{
            //var b = (sender as UIWebView).IsLoading;
            //_ProgressBar.Hidden = false;

            //var strURL = request.Url.AbsoluteString;

            //if (strURL.Contains("login"))
            //	NavigationController.NavigationBar.Hidden = true;
            //else
            //	NavigationController.NavigationBar.Hidden = false;
        }

		void HandleLoadError(object sender, UIWebErrorArgs e)
		{
            //_ProgressBar.Hidden = true;
            Console.WriteLine(e.Error);
		}

		void HandleLoadFinished(object sender, EventArgs e)
		{
			//var b = (sender as UIWebView).IsLoading;
			// _ProgressBar.Hidden = true;

			if (_isLoggedOut) return;

			var strURL = LTWebView.Request.Url.AbsoluteString;

			if (strURL.Contains(Constants.LoginUrl))
			{
				_appSettings.IsLoggedIn = false;

				LTWebView.EvaluateJavascript(string.Format(Constants.JsFillEmail, _email));
			}
			else
			{
				_appSettings.IsLoggedIn = true;
			}
            
            string cssString = Constants.CssHideTopBar;
			string jsString = Constants.JsHideBottomBar;
			string jsWithCSS = string.Format(jsString, cssString);
			LTWebView.EvaluateJavascript(jsWithCSS);

            if (_url != LTWebView.Request.Url.AbsoluteString)
            {
                _prevUrl = _url;
                _url = LTWebView.Request.Url.AbsoluteString;
            }

            Console.WriteLine(_url);
        }

		void CloseMenu()
		{
            ShowMenu(false);

			View.LayoutIfNeeded();
		}
  		#endregion
	}
}
using Foundation;
using System;
using UIKit;

namespace LiveTiles.iOS
{
    public partial class LoginVC : BaseViewController
    {
        public LoginVC() : base()
		{
		}
		public LoginVC(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetUIStyle();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (AppStatus.IsLoggedIn == true && AppStatus.MxData != null)
			{
				GoToMainVC(string.Empty);
			}
		}

		void SetUIStyle()
		{
			btnLogin.Layer.CornerRadius = 5;
			btnLogin.ClipsToBounds = true;

			View.BackgroundColor = ColorFromValue(AppSettings.COLOR_LOGIN_BACKGROUND);
			btnLogin.SetTitleColor(ColorFromValue(AppSettings.COLOR_LOGIN_BUTTON_BACKGROUND), UIControlState.Normal);
			btnLogin.BackgroundColor = ColorFromValue(AppSettings.COLOR_LOGIN_BUTTON_TEXT_BACKGROUND);

			imgLogo.Image = UIImage.FromFile(AppSettings.LOGO_IMG_NAME);
		}

		async partial void ActionLogin(UIButton sender)
		{
			if (String.IsNullOrEmpty(txtEmail.Text))
			{
				ShowMessageBox(null, AppSettings.MSG_EMPTY_EMAIL);
				return;
			}

			ShowLoadingView(AppSettings.MSG_LOADING);

			var mxData = await GlobalFunctions.GetMXData(txtEmail.Text);

			HideLoadingView();

			if (mxData == null)
			{
				ShowMessageBox(null, AppSettings.MSG_INVALID_EMAIL);
				return;
			}

			AppStatus.MxData = mxData;

			GoToMainVC(txtEmail.Text);
		}

		void GoToMainVC(string email)
		{
			UINavigationController ltNVC = Storyboard.InstantiateViewController("LiveTilesNVC") as UINavigationController;
			(ltNVC.TopViewController as LiveTilesHomeVC)._email = email;
			this.PresentViewController(ltNVC, true, null);
		}
	}
}
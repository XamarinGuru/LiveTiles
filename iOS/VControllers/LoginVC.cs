using Foundation;
using System;
using UIKit;

namespace LiveTiles.iOS
{
    public partial class LoginVC : BaseViewController
    {
        AppleAppSettings _appSettings = AppleAppSettings.Instance;

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

            _appSettings.Load();

			if (_appSettings.IsLoggedIn == true && _appSettings.MxData != null)
			{
				GoToMainVC(string.Empty);
			}
		}

		void SetUIStyle()
		{
			btnLogin.Layer.CornerRadius = 5;
			btnLogin.ClipsToBounds = true;

			View.BackgroundColor = ColorFromValue(_appSettings.FeatureColor);
			btnLogin.SetTitleColor(ColorFromValue(_appSettings.FeatureColor), UIControlState.Normal);
			btnLogin.BackgroundColor = ColorFromValue(_appSettings.BackgroundColorForTheme);

            txtEmail.Font = UIFont.FromName("Lato", 18f);
    		btnLogin.Font = UIFont.FromName("Lato", 18f);

			// TODO Delete this OR set it to last load
			// imgLogo.Image = UIImage.FromFile(AppSettings.LogoName);
		}

		partial void ActionLogin(UIButton sender)
		{
			if (String.IsNullOrEmpty(txtEmail.Text))
			{
				ShowMessageBox(null, Constants.EmptyEmailTxt);
				return;
			}

			ShowLoadingView(Constants.LoadingTxt);

            var txt = txtEmail.Text;

            new System.Threading.Thread(new System.Threading.ThreadStart(async () => 
			{
				var mxData = await MxData.LoadAsync(txt);

				InvokeOnMainThread(() =>
				{
					HideLoadingView();

					if (mxData == null)
					{
						ShowMessageBox(null, Constants.InvalidEmailTxt);
						return;
					}

					_appSettings.MxData = mxData;
                    _appSettings.Save();

					GoToMainVC(txtEmail.Text);
				});
			})).Start();
		}

		void GoToMainVC(string email)
		{
			UINavigationController ltNVC = Storyboard.InstantiateViewController("LiveTilesNVC") as UINavigationController;
			(ltNVC.TopViewController as LiveTilesHomeVC)._email = email;
			this.PresentViewController(ltNVC, true, null);
		}
	}
}
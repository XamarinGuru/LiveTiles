using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using Android.Content.PM;
using System;

namespace LiveTiles.Droid
{
	[Activity(Label = "MainActivity", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
	public class LoginAC : BaseVC
	{
        AndroidAppSettings _appSettings = AndroidAppSettings.Instance;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			if (_appSettings.IsLoggedIn == true)
			{
				LoginWithEmail(string.Empty);
			}
			// Get our button from the layout resource,
			// and attach an event to it
			FindViewById(Resource.Id.btnLogin).Click += ActionLogin;

			SetUIStyle();
		}

		void SetUIStyle()
		{
            FindViewById<LinearLayout>(Resource.Id.background).SetBackgroundColor(
            	Android.Graphics.Color.ParseColor(
            		Utils.AndroidColorFormat(_appSettings.FeatureColor)
            	)
            );
            var btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

			btnLogin.SetTextColor(
				Android.Graphics.Color.ParseColor(
					Utils.AndroidColorFormat(_appSettings.FeatureColor)
				)
			);
			btnLogin.SetBackgroundColor(
				Android.Graphics.Color.ParseColor(
					Utils.AndroidColorFormat(_appSettings.BackgroundColorForTheme)
                ));
			// FindViewById<ImageView>(Resource.Id.imgLogo).SetImageResource(Resource.Drawable.icon_logo);
		}

		async void ActionLogin(object sender, EventArgs e)
		{
			var txtEmail = FindViewById<EditText>(Resource.Id.txtEmail).Text;

			if (String.IsNullOrEmpty(txtEmail))
			{
				ShowMessageBox(null, Constants.InvalidEmailTxt);
				return;
			}

            ShowLoadingView(Constants.LoadingTxt);

            var mxData = await MxData.LoadAsync(txtEmail);

			HideLoadingView();

			if (mxData == null)
			{
				ShowMessageBox(null, Constants.InvalidEmailTxt);
				return;
			}

			_appSettings.MxData = mxData;
            _appSettings.Save();

			LoginWithEmail(txtEmail);
		}

		void LoginWithEmail(string email)
		{
			var activity = new Intent(this, typeof(LiveTilesHomeAC));
			activity.PutExtra("EMAIL", email);
			StartActivityForResult(activity, 1);
		}
	}
}


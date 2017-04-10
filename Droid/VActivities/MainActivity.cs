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
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			if (AppStatus.IsLoggedIn == true)
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
			//FindViewById<LinearLayout>(Resource.Id.background).SetBackgroundColor(
			//	Android.Graphics.Color.ParseColor(
			//		GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_LOGIN_BACKGROUND)
			//	)
			//);
			FindViewById<Button>(Resource.Id.btnLogin).SetTextColor(
				Android.Graphics.Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_LOGIN_BUTTON_BACKGROUND)
				)
			);
			FindViewById<Button>(Resource.Id.btnLogin).SetBackgroundColor(
				Android.Graphics.Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_LOGIN_BUTTON_TEXT_BACKGROUND)
				)
			);
			FindViewById<ImageView>(Resource.Id.imgLogo).SetImageResource(Resource.Drawable.icon_logo);
		}

		void ActionLogin(object sender, EventArgs e)
		{
			var txtEmail = FindViewById<EditText>(Resource.Id.txtEmail).Text;

			if (String.IsNullOrEmpty(txtEmail))
			{
				var alert = new AlertDialog.Builder(this);
				alert.SetTitle("");
				alert.SetMessage(AppSettings.MSG_INVALID_EMAIL);
				alert.SetCancelable(false);
				alert.SetPositiveButton("OK", delegate
				{
				});
				RunOnUiThread(() =>
				{
					alert.Show();
				});

				return;
			}

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


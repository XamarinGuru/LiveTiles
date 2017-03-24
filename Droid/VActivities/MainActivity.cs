using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

namespace LiveTiles.Droid
{
	[Activity(Label = "LiveTiles", MainLauncher = true, Icon = "@drawable/AppIcon")]
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
				var activity = new Intent(this, typeof(LiveTilesHomeAC));
				StartActivityForResult(activity, 1);
			}
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.btnLogin);

			button.Click += delegate { 
				var activity = new Intent(this, typeof(LiveTilesHomeAC));
				StartActivityForResult(activity, 1);
			};

			SetUIStyle();
		}

		void SetUIStyle()
		{
			FindViewById<LinearLayout>(Resource.Id.background).SetBackgroundColor(
				Android.Graphics.Color.ParseColor(
					GlobalFunctions.AndroidColorFormat(AppSettings.COLOR_LOGIN_BACKGROUND)
				)
			);
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
	}
}


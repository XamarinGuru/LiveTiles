using Android.App;
using Android.OS;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Content;

namespace LiveTiles.Droid
{
	[Activity(Label = "Showcase", MainLauncher = true, Icon = "@drawable/appicon120", Theme = "@style/MyTheme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : Activity
	{
		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);
		}

		protected override void OnResume()
		{
			base.OnResume();

			Task startupWork = new Task(() =>
			{
				StartActivity(new Intent(this, typeof(LoginAC)));
				Finish();
			});

			startupWork.Start();
		}
	}
}


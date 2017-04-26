
using Android.App;
using Android.OS;
using Android.Views;
using AndroidHUD;

namespace LiveTiles.Droid
{
	[Activity(Label = "BaseVC")]
	public class BaseVC : Activity
	{
		AlertDialog.Builder alert;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);
		}

		protected void ShowMessageBox(string title, string msg)
		{
			alert = new AlertDialog.Builder(this);
			alert.SetTitle(title);
			alert.SetMessage(msg);
			alert.SetNegativeButton("OK", (senderAlert, args) => { });
			RunOnUiThread(() => { alert.Show(); });
		}

		protected void ShowLoadingView(string title)
		{
			RunOnUiThread(() =>
			{
				AndHUD.Shared.Show(this, title, -1, MaskType.Black);
			});
		}

		protected void HideLoadingView()
		{
			RunOnUiThread(() =>
			{
				AndHUD.Shared.Dismiss(this);
			});
		}
	}
}

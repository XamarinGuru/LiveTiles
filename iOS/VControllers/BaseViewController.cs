using System;
using BigTed;
using UIKit;

namespace LiveTiles.iOS
{
	public partial class BaseViewController : UIViewController
	{
		public BaseViewController() : base()
		{
		}
		public BaseViewController(IntPtr handle) : base(handle)
		{
		}

		protected void ShowLoadingView(string title)
		{
			InvokeOnMainThread(() => { BTProgressHUD.Show(title, -1, ProgressHUD.MaskType.Black); });
		}

		protected void HideLoadingView()
		{
			InvokeOnMainThread(() => { BTProgressHUD.Dismiss(); });
		}

		public UIColor ColorFromValue(string hexColor)
		{
			var red = Convert.ToInt32(hexColor.Substring(0, 2), 16) / 255f;
			var green = Convert.ToInt32(hexColor.Substring(2, 2), 16) / 255f;
			var blue = Convert.ToInt32(hexColor.Substring(4, 2), 16) / 255f;
			return UIColor.FromRGB(red, green, blue);
		}
	}
}


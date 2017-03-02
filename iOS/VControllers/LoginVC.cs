using Foundation;
using System;
using UIKit;

namespace LiveTiles.iOS
{
    public partial class LoginVC : UIViewController
    {
        public LoginVC (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			btnLogin.Layer.CornerRadius = 5;
			btnLogin.ClipsToBounds = true;
		}

		partial void ActionLogin(UIButton sender)
		{
			UINavigationController ltNVC = Storyboard.InstantiateViewController("LiveTilesNVC") as UINavigationController;
			this.PresentViewController(ltNVC, true, null);
		}
	}
}
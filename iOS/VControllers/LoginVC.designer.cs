// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace LiveTiles.iOS
{
    [Register ("LoginVC")]
    partial class LoginVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLogin { get; set; }

        [Action ("ActionLogin:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ActionLogin (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnLogin != null) {
                btnLogin.Dispose ();
                btnLogin = null;
            }
        }
    }
}
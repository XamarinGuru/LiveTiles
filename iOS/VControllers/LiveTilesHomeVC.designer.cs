// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace LiveTiles.iOS
{
    [Register ("LiveTilesHomeVC")]
    partial class LiveTilesHomeVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView LTWebView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView menuContent { get; set; }

        [Action ("ActionLogOut:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ActionLogOut (UIKit.UIButton sender);

        [Action ("ActionStartPage:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ActionStartPage (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (imgLogo != null) {
                imgLogo.Dispose ();
                imgLogo = null;
            }

            if (LTWebView != null) {
                LTWebView.Dispose ();
                LTWebView = null;
            }

            if (menuContent != null) {
                menuContent.Dispose ();
                menuContent = null;
            }
        }
    }
}
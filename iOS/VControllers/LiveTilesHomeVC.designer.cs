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
    [Register ("LiveTilesHomeVC")]
    partial class LiveTilesHomeVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView _ProgressBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint heightMenu { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBuiltWith { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblLogOut { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblStartPage { get; set; }

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
            if (_ProgressBar != null) {
                _ProgressBar.Dispose ();
                _ProgressBar = null;
            }

            if (heightMenu != null) {
                heightMenu.Dispose ();
                heightMenu = null;
            }

            if (imgLogo != null) {
                imgLogo.Dispose ();
                imgLogo = null;
            }

            if (lblBuiltWith != null) {
                lblBuiltWith.Dispose ();
                lblBuiltWith = null;
            }

            if (lblLogOut != null) {
                lblLogOut.Dispose ();
                lblLogOut = null;
            }

            if (lblStartPage != null) {
                lblStartPage.Dispose ();
                lblStartPage = null;
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
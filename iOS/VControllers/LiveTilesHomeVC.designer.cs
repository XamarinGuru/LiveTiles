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
        UIKit.UIWebView LTWebView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LTWebView != null) {
                LTWebView.Dispose ();
                LTWebView = null;
            }
        }
    }
}
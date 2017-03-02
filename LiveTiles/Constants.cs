using System;
namespace LiveTiles
{
	public class Constants
	{
		public static string URL_LIVETILES = "https://trylivetiles.sharepoint.com/sites/workplace1000/SitePages/Trial_Mobile_View.aspx";
		public static string INJECT_CSS = "@media only screen and (max-width: 748px) {#suiteBarDelta, #s4-ribbonrow{display:none;}}";
		public static string INJECT_JS = "var style = document.createElement('style'); style.innerHTML = '{0}'; document.head.appendChild(style)";
	}
}

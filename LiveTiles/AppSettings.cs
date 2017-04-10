using System;
namespace LiveTiles
{
	public class AppSettings
	{
		#region Android
		///App Icon
		/// <summary>
		/// Change image AppIcon.png in path root/Droid/Resources/drawable with same name & format
		/// </summary>

		/// ///App Name & Package Name
		/// <summary>
		/// Change Application Name & Package Name in project/AndroidManifest.xml
		/// And need to change Activity label in line #9 of MainActivity
		/// </summary>

		/// Logo Icon & background & loading spinner(web view)
		/// <summary>
		/// Change images in path root/Droid/Resources/drawable with same name & format
		/// - [icon_logo.png] for logo 
		/// - [background.png] for background 
		/// - [icon_progress.png] for loading spinner(web view)
		/// </summary>
		#endregion

		#region iOS

		/// ///App Name & Package Name
		/// <summary>
		/// Change Application Name & Package Name in project/info.plist
		/// </summary>

		/// Logo Icon & background & loading spinner(web view)
		/// <summary>
		/// Change images in path root/iOS/Resources/ with same name & format
		/// - [icon_logo.png] for logo 
		/// - [background.png] for background 
		/// - [icon_progress.png] for loading spinner(web view)
		/// </summary>
		#endregion

		//public static string URL_BASE = "https://trylivetiles.sharepoint.com/sites/workplace1000/SitePages/Trial_Mobile_View.aspx";
		//public static string URL_BASE = "https://trylivetiles.sharepoint.com/sites/workplace1000";
		//public static string URL_BASE = "https://getlivetiles.sharepoint.com";

		#region the url for demo account of apple review
		public static string URL_DEMO = "https://trylivetiles.sharepoint.com/sites/workplace1000/sitepages/Andy.aspx";
		#endregion

		#region the url for normal user
		public static string URL_BASE = "https://trylivetiles.sharepoint.com/sites/workplace1000/sitepages/Andy.aspx";
		#endregion
		//public static string URL_LOGOUT = "https://getlivetiles.sharepoint.com";

		public static string COLOR_LOGIN_BACKGROUND = "1CB4E9";
		public static string COLOR_LOGIN_BUTTON_BACKGROUND = "1CB4E9";
		public static string COLOR_LOGIN_BUTTON_TEXT_BACKGROUND = "FFFFFF";
		public static string COLOR_NAVIGATION_BAR_BACKGROUND = "1CB4E9";
		public static string COLOR_MENU_BACKGROUND = "E8E8E8";
		public static string COLOR_MENU_TEXT_BACKGROUND = "000000";

		public static string LOGO_IMG_NAME = "icon_logo.png";

		public static string DEMO_EMAIL = "jun.lee@trylivetiles.onmicrosoft.com";
		public static string MSG_INVALID_EMAIL = "Please input your email.";
	}
}

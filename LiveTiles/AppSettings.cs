using System;
using System.Collections.Generic;

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

		public static string URL_MXDATA = "https://livetiles-staging.s3.amazonaws.com/mx-data/{0}/mxdata.json";
		//public static string URL_LOGOUT = "https://getlivetiles.sharepoint.com";

		public static string COLOR_LOGIN_BACKGROUND = "1CB4E9";
		public static string COLOR_LOGIN_BUTTON_BACKGROUND = "1CB4E9";
		public static string COLOR_LOGIN_BUTTON_TEXT_BACKGROUND = "FFFFFF";
		public static string COLOR_NAVIGATION_BAR_BACKGROUND = "1CB4E9";
		public static string COLOR_MENU_BACKGROUND = "E8E8E8";
		public static string COLOR_MENU_TEXT_BACKGROUND = "000000";

		public static string LOGO_IMG_NAME = "icon_logo.png";

		public static string DEMO_EMAIL = "jun.lee@trylivetiles.onmicrosoft.com";

		public static string MSG_LOADING = "Loading...";
		public static string MSG_EMPTY_EMAIL = "Please input your email.";
		public static string MSG_INVALID_EMAIL = "Your email is not vaild.";


		public static string INJECT_JS_HIDE_BOTTOM_BAR = "var style = document.createElement('style'); style.innerHTML = '{0}'; document.head.appendChild(style);$('#suiteBarToggle').append('<i style=\"color:#000; float: right; font-size: 24px;\" class=\"show-ribbon fa fa-caret-down\" aria-hidden=\"true\"></i><i style=\"color:#000; float: right; font-size: 24px; display: none;\" class=\"hide-ribbon fa fa-caret-up\" aria-hidden=\"true\"></i>');\n$('#suiteBarDelta').hide();\n$('#s4-ribbonrow').hide();\n$('#s4-workspace').height($('#s4-workspace').height()+90);";
		public static string INJECT_CSS_HIDE_TOP_BAR = "@media only screen and (max-width: 748px) {#suiteBarDelta, #s4-ribbonrow{display:none;}}";
		public static string INJECT_JS_FILL_EMAIL = "javascript:document.getElementById('cred_userid_inputtext').value ='{0}';";
		public static string SYMBOL_LOGIN = "login.microsoftonline.com";

		public static Dictionary<String, String> MXEMAIL_VS_DOMAIN = new Dictionary<String, String>
		{
			{"trylivetiles.onmicrosoft.com", "trylivetiles.sharepoint.com" },
			{"livetiles.nyc", "livetiles.nyc"}
		};
	}
}

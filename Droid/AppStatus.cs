using System;
using Android.App;
using Android.Content;

namespace LiveTiles.Droid
{
	public static class AppStatus
	{
		private static ISharedPreferences _appSettings = Application.Context.GetSharedPreferences("App_settings", FileCreationMode.Private);


		private const string isLoggedin = "userID";
		public static bool IsLoggedIn
		{
			get
			{
				return _appSettings.GetBoolean(isLoggedin, false);
			}
			set
			{
				ISharedPreferencesEditor editor = _appSettings.Edit();
				editor.PutBoolean(isLoggedin, value);
				editor.Apply();
			}
		}
	}
}

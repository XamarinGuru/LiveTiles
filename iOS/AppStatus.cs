using System;
using Foundation;

namespace LiveTiles.iOS
{
	public static class AppStatus
	{
		private const string isLoggedIn = "isLoggedIn";
		public static bool IsLoggedIn
		{
			get { return NSUserDefaults.StandardUserDefaults.BoolForKey(isLoggedIn); }
			set
			{
				NSUserDefaults.StandardUserDefaults.SetBool(value, isLoggedIn);
			}
		}
	}
}

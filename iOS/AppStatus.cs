using System;
using Foundation;
using Newtonsoft.Json;

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

		private const string latestURL = "latestURL";
		public static string LatestURL
		{
			get { return NSUserDefaults.StandardUserDefaults.StringForKey(latestURL); }
			set
			{
				NSUserDefaults.StandardUserDefaults.SetString(value, latestURL);
			}
		}

		private const string mxData = "mxData";
		public static MxData MxData
		{
			get 
			{
				try
				{
					var strMxData = NSUserDefaults.StandardUserDefaults.StringForKey(mxData);
					return JsonConvert.DeserializeObject<MxData>(strMxData);
				}
				catch (Exception ex)
				{
					return null;
				}
			}
			set
			{
				var strMxData = JsonConvert.SerializeObject(value);
				NSUserDefaults.StandardUserDefaults.SetString(strMxData, mxData);
			}
		}
	}
}

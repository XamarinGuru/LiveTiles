using System;
using Foundation;
using Newtonsoft.Json;

namespace LiveTiles.iOS
{
    public class AppleAppSettings : AppSettingsBase
    {
        static AppleAppSettings _AppSettings = new AppleAppSettings();

        public static AppleAppSettings Instance
        {
            get
            {
                return _AppSettings;
            }
        }

        private AppleAppSettings() { }

        private const string IsLoggedInId = "AppleAppSettings.IsLoggedIn";
		public override bool IsLoggedIn
		{
			get { return NSUserDefaults.StandardUserDefaults.BoolForKey(IsLoggedInId); }
			set
			{
				NSUserDefaults.StandardUserDefaults.SetBool(value, IsLoggedInId);
			}
		}

        private const string LatestUrlId = "AppleAppSettings.LatestURL";
		public override string LatestUrl
        {
            get { return NSUserDefaults.StandardUserDefaults.StringForKey(LatestUrlId); }
            set
            {
                NSUserDefaults.StandardUserDefaults.SetString(value, LatestUrlId);
            }
        }

        private const string MxDataId = "AppAppSettings.MxData";
        public override void Save()
        {
            if (MxData == null)
            {
                NSUserDefaults.StandardUserDefaults.SetString("", MxDataId);
                return;
            }
            
			var strMxData = JsonConvert.SerializeObject(MxData);
            NSUserDefaults.StandardUserDefaults.SetString(strMxData, MxDataId);
        }

        public override void Load()
        {
			var strMxData = NSUserDefaults.StandardUserDefaults.StringForKey(MxDataId);
            if (!string.IsNullOrEmpty(strMxData))
			    MxData = JsonConvert.DeserializeObject<MxData>(strMxData);
        }
	}
}

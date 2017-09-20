using System;
using Android.App;
using Android.Content;
using Newtonsoft.Json;

namespace LiveTiles.Droid
{
    /// <summary>
    /// The Android version of the centralized appSettings
    /// </summary>
    public class AndroidAppSettings : AppSettingsBase
	{
		static AndroidAppSettings _AppSettings = new AndroidAppSettings();

		public static AndroidAppSettings Instance
		{
			get
			{
				return _AppSettings;
			}
		}

        private static ISharedPreferences _sharedPreferences = Application.Context.GetSharedPreferences("App_settings", FileCreationMode.Private);

        private AndroidAppSettings() {}


        private const string IsLoggedInId = "AndroidAppSettings.IsLoggedIn";
		public override bool IsLoggedIn
		{
			get
			{
				return _sharedPreferences.GetBoolean(IsLoggedInId, false);
			}
			set
			{
				ISharedPreferencesEditor editor = _sharedPreferences.Edit();
				editor.PutBoolean(IsLoggedInId, value);
				editor.Apply();
			}
		}

        private const string LatestUrlId = "latestURL";
		public override string LatestUrl
        {
            get
            {
                return _sharedPreferences.GetString(LatestUrlId, "");
            }
            set
            {
                ISharedPreferencesEditor editor = _sharedPreferences.Edit();
                editor.PutString(LatestUrlId, value);
                editor.Apply();
            }
        }

        private const string MxDataId = "AndroidAppSettings.MxData";

        public override void Save()
        {
			var strMxData = JsonConvert.SerializeObject(base.MxData);
			ISharedPreferencesEditor editor = _sharedPreferences.Edit();
			editor.PutString(MxDataId, strMxData);
			editor.Apply();
        }

        public override void Load()
        {
			var strMxData = _sharedPreferences.GetString(MxDataId, "");
            if (string.IsNullOrEmpty(strMxData))
                return;
            
            base.MxData = JsonConvert.DeserializeObject<MxData>(strMxData);
        }
	}
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiveTiles
{

    public class Utils
	{
		public static string AndroidColorFormat(string hexColor)
		{
			return "#" + hexColor;
		}

		public static string GetDomainFromEmail(string email)
		{
			try
            {
				string[] words = email.Split('@');
                if (words.Length > 1)
                {
                    string mxEmailDomain = words[1].ToLower();
                    return mxEmailDomain;
                }
                else if (words.Length == 1)
                    return words[0].ToLower();
                else
                    return null;
            }
            catch
            {
                return null;
            }
		}

        public static ThemeStyles ThemeStyleForString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return ThemeStyles.Light;

            val = val.ToLower();

            if (val == "dark")
                return ThemeStyles.Dark;
            else
                return ThemeStyles.Light;
        }

		public static string GetColorForContentMode(ThemeStyles theme)
		{
			if (theme == ThemeStyles.Dark)
				return "FFFFFF";
			else
				return "000000";
		}

		public static string GetBackgroundColorForContentMode(ThemeStyles theme)
		{
			if (theme == ThemeStyles.Dark)
				return "000000";
			else
				return "FFFFFF";
		}
	}
}

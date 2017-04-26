using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiveTiles
{
	public class GlobalFunctions
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
				string mxEmailDomain = words[1];
				return AppSettings.MXEMAIL_VS_DOMAIN[mxEmailDomain];
            }
            catch (Exception exception)
            {
                return null;
            }
		}

		public static async Task<MxData> GetMXData(string email)
		{
			try
            {
				var domain = GetDomainFromEmail(email);
				var url = string.Format(AppSettings.URL_MXDATA, domain);

                var client = new HttpClient();
				var json = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<MxData>(json.ToString());
            }
            catch (Exception exception)
            {
                return null;
            }
		}
	}
}

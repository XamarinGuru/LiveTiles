using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiveTiles
{
	public class BrandingData
	{
		public string contentMode { get; set; }
		public string featureColor { get; set; }
	}

	public class MxData
	{
		// Production Azure storage account
		// TODO: Custom Domain
		public static string JsonStoreUrlTemplate = "https://livetilesmx.blob.core.windows.net/enterprise/{0}/mxdata.json";

		public static async Task<MxData> LoadAsync(string email)
		{
			try
			{
				if (AppSettingsBase.UseLocalMxData)
					return AppSettingsBase.LocalMxData;
				else
				{
					var domain = Utils.GetDomainFromEmail(email);
					var url = string.Format(JsonStoreUrlTemplate, domain);

					var client = new HttpClient();
					var json = await client.GetStringAsync(url);
					return JsonConvert.DeserializeObject<MxData>(json.ToString());
				}
			}
			catch
			{
				return null;
			}
		}

		public string homepageURL { get; set; }
		public string orgName { get; set; }
		public bool isActive { get; set; }
		public BrandingData brandingData { get; set; }
	}
}

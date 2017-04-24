using System;
namespace LiveTiles
{
	public class BrandingData
	{
		public string contentMode { get; set; }
		public string featureColor { get; set; }
	}

	public class MxData
	{
		public string homepageURL { get; set; }
		public string orgName { get; set; }
		public bool isActive { get; set; }
		public string iconImage { get; set; }
		public BrandingData brandingData { get; set; }
	}
}

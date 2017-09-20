﻿using System;
using System.Collections.Generic;

namespace LiveTiles
{
	public enum ThemeStyles
	{
		Light,
		Dark
	}

    /// <summary>
    /// Base class for app settings, providing the structure for centralizing styling and behavior
    /// With a set of static helper functions as well
    /// </summary>
    public abstract class AppSettingsBase
    {
        // If true use MxData defined below, else load from blob storage specified in JsonStoreUrlTemplate
        public static bool UseLocalMxData = true;

        /// <summary>
        /// The local mx data
        /// This is used to override all behavior when UseLocalMxData is set to true
        /// </summary>
        public static MxData LocalMxData = new MxData()
        {
            homepageURL = "https://trylivetiles.sharepoint.com/sites/showcase/sitepages/IndustryShowcase.aspx",
            orgName = "Showcase",
            isActive = true,

            brandingData = new BrandingData()
            {
                contentMode = "light",
                featureColor = "3D3A3B",
            }
        };

        /// <summary>
        /// Overrides the URL in the JSON store
        /// This sets the start page for custom builds of the app
        /// </summary>
        public static string OverrideUrl = null;

        /// <summary>
        /// The default color of the feature.
        /// </summary>
        public const string DefaultFeatureColor = "00be9c";

        public static ThemeStyles DefaultTheme = ThemeStyles.Light;

        public string HomeUrl
        {
            get
            {
                if (UseLocalMxData)
                    return LocalMxData.homepageURL;

                return MxData?.homepageURL ?? "";
            }
        }

        /// <summary>
        /// Gets the feature
        /// </summary>
        /// <value>The color of the feature.</value>
        public string FeatureColor
        {
            get
            {
                if (UseLocalMxData)
                    return LocalMxData.brandingData.featureColor;

                return MxData?.brandingData?.featureColor ?? DefaultFeatureColor;
            }
        }

        /// <summary>
        /// Gets the theme defined in MxData
        /// Or falls back to default
        /// </summary>
        /// <value>The theme.</value>
        public ThemeStyles Theme
        {
            get
            {
                if (UseLocalMxData)
                    return Utils.ThemeStyleForString(LocalMxData.brandingData.contentMode);

                string themeString = MxData?.brandingData?.contentMode ?? "light";
                return Utils.ThemeStyleForString(themeString);
            }
        }

        /// <summary>
        /// Gets the foreground color (EG, for text) for the current theme
        /// </summary>
        /// <value>The color for theme.</value>
        public string ColorForTheme
        {
            get
            {
                if (Theme == ThemeStyles.Light)
                    return "000000";
                else
                    return "FFFFFF";
            }
        }

        /// <summary>
        /// Gets the background color for the current theme
        /// </summary>
        /// <value>The background color for theme.</value>
        public string BackgroundColorForTheme
        {
			get
			{
				if (Theme == ThemeStyles.Light)
					return "FFFFFF";
				else
					return "000000";
			}
        }

        /// <summary>
        /// Gets or sets the underlying MxData obo
        /// </summary>
        /// <value>The mx data.</value>
        public MxData MxData { get; set; }

        /// <summary>
        /// Implemented in child classes as platform-specific method of persisting
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Restores this from disk in a platform specific way
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:LiveTiles.AppSettingsBase"/> is logged in.
        /// </summary>
        /// <value><c>true</c> if is logged in; otherwise, <c>false</c>.</value>
        public abstract bool IsLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the latest URL.
        /// </summary>
        /// <value>The latest URL.</value>
        public abstract string LatestUrl { get; set; }
    }
}

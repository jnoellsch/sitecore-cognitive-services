﻿namespace Sitecore.SharedSource.CognitiveMedia
{
    using System;

    public class SitecoreSettingsManager : IApiKeySettings
    {
        private const string DefaultValue = "{YOUR_API_KEY}";

        public SitecoreSettingsManager()
        {
            this.ComputerVision = this.TryGetSetting("CognitiveMedia.ApiKey.ComputerVision");
        }

        public string ComputerVision { get; }

        private string TryGetSetting(string key)
        {
            string val = Configuration.Settings.GetSetting(key);
            bool isDefaultOrMissing = val.Equals(DefaultValue, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(val);

            if (isDefaultOrMissing)
            {
                throw new Exception($"The value for the {key} setting is still set to the default value or is missing. Do you need to patch the setting?");
            }

            return val;
        }
    }
}
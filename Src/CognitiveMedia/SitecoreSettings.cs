namespace Sitecore.SharedSource.CognitiveMedia
{
    using System;

    /// <summary>
    /// Reads specific settings values from Sitecore &lt;settings&gt; configuration element.
    /// </summary>
    public class SitecoreSettings : IApiKeySettings
    {
        private const string DefaultValue = "{YOUR_API_KEY}";

        public SitecoreSettings()
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
namespace Sitecore.SharedSource.CognitiveMedia
{
    public class SitecoreSettingsManager : IApiKeySettings
    {
        public string ComputerVision => Configuration.Settings.GetSetting("CognitiveMedia.ApiKey.ComputerVision");
    }
}
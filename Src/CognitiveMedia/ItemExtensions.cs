namespace Sitecore.SharedSource.CognitiveMedia
{
    using Sitecore.Data.Items;

    /// <summary>
    /// Adds utility methods for <see cref="Item"/>.
    /// </summary>
    public static class ItemExtensions
    {
        public static bool IsNotMasterDb(this Item item) => item?.Database.Name != "master";

        public static bool IsNotMedia(this Item item) => (bool)!item?.Paths.IsMediaItem;
    }
}
namespace Sitecore.SharedSource.CognitiveMedia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ProjectOxford.Vision;
    using Sitecore.Data.Items;
    using Sitecore.Events;
    using Sitecore.Resources.Media;

    public class ImageTagger
    {
        public ImageTagger()
        {
            this.ApiKeys = new SitecoreSettings();
        }

        public IApiKeySettings ApiKeys { get; }

        protected ImageTaggerLogger Log { get; } = new ImageTaggerLogger();

        public virtual void Tag(object sender, EventArgs args)
        {
            if (args == null) return;
            var item = Event.ExtractParameter(args, 0) as Item;

            // validate
            if (item == null) return;
            if (this.IsNotMasterDb(item)) return;
            if (this.IsNotMedia(item)) return;

            // do work
            Task.Factory.StartNew(() => this.TagAsync(item, args));
        }

        private async void TagAsync(Item item, EventArgs args)
        {
            this.Log.TagStarted();

            try
            {
                // convert
                var mediaItem = MediaManager.GetMedia(item);

                // send image data to cognitive services
                var visionClient = new VisionServiceClient(this.ApiKeys.ComputerVision);
                var features = new List<VisualFeature>() { VisualFeature.Tags, VisualFeature.Description };
                var analysis = await visionClient.AnalyzeImageAsync(mediaItem.GetStream().Stream, features);

                // update Sitecore image 
                item.Editing.BeginEdit();
                item["Keywords"] = string.Join(", ", analysis.Description.Tags);
                item["Description"] = string.Join(", ", analysis.Description.Captions.Select(x => x.Text));
                item.Editing.EndEdit();
            }
            catch (Exception ex)
            {
                this.Log.TagException(ex);
            }
            finally
            {
                this.Log.TagCompleted();
            }
        }

        private bool IsNotMasterDb(Item savedItem) => savedItem.Database.Name != "master";

        private bool IsNotMedia(Item savedItem) => !savedItem.Paths.IsMediaItem;
    }
}
namespace Sitecore.SharedSource.CognitiveMedia
{
    using System;
    using System.Diagnostics;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Provides logging and telemetry for <see cref="ImageTagger"/>.
    /// </summary>
    public class ImageTaggerLogger
    {
        protected Stopwatch Stopwatch { get; } = new Stopwatch();

        public void TagStarted()
        {
            this.Stopwatch.Reset();
            this.Stopwatch.Start();
            Log.Info("ImageTagger tagging started.", this);
        }

        public void TagCompleted()
        {
            this.Stopwatch.Stop();
            Log.Info($"ImageTagger tagging complete. Ellapsed time = {this.Stopwatch.ElapsedMilliseconds}ms", this);
        }

        public void TagException(Exception ex)
        {
            Log.Error($"ImageTagger exception. {ex.ToString()}", this);
        }
    }
}
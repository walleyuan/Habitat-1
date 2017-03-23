
namespace Sitecore.Foundation.SitecoreExtensions.Pipelines
{
    using System.Drawing;
    using System.IO;
    using ImageResizer;
    using Resources.Media;

    public class ImageResizerProcessor
    {
        public void Process(GetMediaStreamPipelineArgs args)
        {
            bool imageResizing;

            if (bool.TryParse(args.Options.CustomOptions["ImageResizing"], out imageResizing) && imageResizing)
            {
                var originalImage = Image.FromStream(args.OutputStream.Stream);

                var resizedImage = new MemoryStream();
                var resizeSettings = new ResizeSettings(args.Options.CustomOptions.ToNameValueCollection());
                ImageBuilder.Current.Build(originalImage, resizedImage, resizeSettings);

                var extension = args.OutputStream.Extension;
                args.OutputStream = new MediaStream(resizedImage, extension, args.MediaData.MediaItem);
            }
        }
    }
}
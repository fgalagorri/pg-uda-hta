using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UDA_HTA.Helpers
{
    public static class ImageHelper
    {

        public static void CreateImageA4(FrameworkElement element, string path)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)element.ActualWidth,
                (int)element.ActualHeight,
                96d,
                96d,
                PixelFormats.Pbgra32);

            renderBitmap.Render(element);
            ImageSource imgSrc = (ImageSource) renderBitmap.Clone();

            var newWidth = 604; // Number of pixels A4 page width
            var newHeight = element.ActualHeight/(element.ActualWidth/newWidth);

            var bp = CreateResizedImage(imgSrc, newWidth, (int)newHeight, 1);

            using (FileStream outStream = new FileStream(path, FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(bp);
                // save the data to the stream
                encoder.Save(outStream);
            }
        }


        private static BitmapFrame CreateResizedImage(ImageSource source, int width, int height, int margin)
        {
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);

            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);

            return BitmapFrame.Create(resizedImage);
        } 
    }
}

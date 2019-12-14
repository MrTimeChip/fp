﻿using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudResult.CloudLayouters;
using TagsCloudResult.CloudVisualizers;
using TagsCloudResult.CloudVisualizers.ImageSaving;
using TagsCloudResult.TextParsing;
using TagsCloudResult.TextParsing.CloudParsing;

namespace TagsCloudResult.ApplicationRunning
{
    public class SettingsManager
    {
        private CloudLayouterSettings layouterSettings;
        private CloudVisualizerSettings visualizerSettings;
        private ImageSaverSettings imageSaverSettings;
        private CloudWordsParserSettings cloudWordsParserSettings;

        public SettingsManager()
        {
            layouterSettings = new CloudLayouterSettings();
            visualizerSettings = new CloudVisualizerSettings();
            imageSaverSettings = new ImageSaverSettings();
            cloudWordsParserSettings = new CloudWordsParserSettings();
        }
        
        public void ConfigureLayouterSettings(
            ICloudLayoutingAlgorithm algorithm,
            int rectangleSquareMultiplier,
            double rectangleStep,
            int rectangleBroadness)
        {
            var newSettings = new CloudLayouterSettings
            {
                Algorithm = algorithm,
                RectangleSquareMultiplier = rectangleSquareMultiplier,
                RectangleStep = rectangleStep,
                RectangleBroadness = rectangleBroadness
            };
            layouterSettings = newSettings;
        }
        
        public void ConfigureVisualizerSettings(
            Palette palette,
            IBitmapMaker bitmapMaker,
            int width,
            int height,
            Font font)
        {
            var newSettings = new CloudVisualizerSettings
            {
                Palette = palette, BitmapMaker = bitmapMaker, Width = width, Height = height, Font = font
            };
            visualizerSettings = newSettings;
        }
        
        public void ConfigureImageSaverSettings(ImageFormat format, string savePath)
        {
            var newSettings = new ImageSaverSettings {Format = format, Path = savePath};
            imageSaverSettings = newSettings;
        }
        
        public void ConfigureWordsParserSettings(IFileWordsParser wordsParser, string path, ICloudWordParsingRule rule)
        {
            var newSettings = new CloudWordsParserSettings {FileWordsParser = wordsParser, Path = path, Rule = rule};
            cloudWordsParserSettings = newSettings;
        }
        
        public CloudLayouterSettings GetLayouterSettings()
        {
            return layouterSettings;
        }

        public CloudVisualizerSettings GetVisualizerSettings()
        {
            return visualizerSettings;
        }

        public ImageSaverSettings GetImageSaverSettings()
        {
            return imageSaverSettings;
        }
        
        public CloudWordsParserSettings GetWordsParserSettings()
        {
            return cloudWordsParserSettings;
        }
    }
}
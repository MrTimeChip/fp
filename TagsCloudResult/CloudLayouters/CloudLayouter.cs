﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudResult.CloudVisualizers;
using TagsCloudResult.TextParsing.CloudParsing;

namespace TagsCloudResult.CloudLayouters
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly Func<CloudLayouterSettings> settingsFactory;

        public CloudLayouter(Func<CloudLayouterSettings> settingsFactory)
        {
            this.settingsFactory = settingsFactory;
        }

        public IEnumerable<CloudVisualizationWord> GetWords(IEnumerable<CloudWord> cloudWords)
        {
            var settings = settingsFactory();
            var ordered = cloudWords.OrderByDescending(w => w.Count);
            var sizeMultiplier = GetSizeMultiplier(ordered, settings);
            foreach (var word in ordered)
            {
                var size = GetSize(word, sizeMultiplier);
                var rect = settings.Algorithm.PutNextRectangle(size);
                yield return new CloudVisualizationWord(rect, word.Word);
            }
        }

        private static Size GetSize(CloudWord word, double sizeMultiplier)
        {
            var size = TextRenderer.MeasureText(word.Word, new Font("Arial", 16));
            var width = size.Width * sizeMultiplier * word.Count;
            var height = size.Height * sizeMultiplier * word.Count;
            return new Size((int) width, (int) height);
        }

        private double GetSizeMultiplier(IOrderedEnumerable<CloudWord> ordered, CloudLayouterSettings settings)
        {
            var maxCount = (double) ordered.First().Count;
            var minCount = (double) ordered.Last().Count;
            var countDifference = maxCount - minCount == 0 ? 1 : maxCount - minCount;
            return settings.RectangleSquareMultiplier / countDifference;
        }
    }
}
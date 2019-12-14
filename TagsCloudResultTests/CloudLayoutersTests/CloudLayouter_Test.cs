﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudResult.CloudLayouters;
using TagsCloudResult.CloudLayouters.CircularCloudLayouter;
using TagsCloudResult.TextParsing.CloudParsing;

namespace TagsCloudResultTests.CloudLayoutersTests
{
    [TestFixture]
    public class CloudLayouter_Test
    {
        private CloudLayouterSettings settings;
        private CloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            settings = new CloudLayouterSettings();
            settings.Algorithm = new CircularCloudLayouter(
                new Point(0, 0),
                settings.RectangleStep,
                settings.RectangleBroadness);
            settings.RectangleSquareMultiplier = 100;
            layouter = new CloudLayouter(() => settings);
        }

        [Test]
        public void GetWords_Should_ReturnDescendingOrderedCollection()
        {
            var firstWord = new CloudWord("apple");
            firstWord.AddCount();
            firstWord.AddCount();
            var secondWord = new CloudWord("cider");
            secondWord.AddCount();
            var words = new List<CloudWord> {secondWord, firstWord};
            var visualizationWords = layouter.GetWords(words);
            visualizationWords.First().Word.Should().Be("apple");
        }

        [Test]
        public void GetWords_Should_ReturnWordsWithDescendingSizes()
        {
            var firstWord = new CloudWord("apple");
            firstWord.AddCount();
            firstWord.AddCount();
            var secondWord = new CloudWord("cider");
            secondWord.AddCount();
            var words = new List<CloudWord> {secondWord, firstWord};
            var visualizationWords = layouter.GetWords(words);
            var firstSize = visualizationWords.First().Rectangle.Size;
            var secondSize = visualizationWords.Last().Rectangle.Size;
            var firstSquare = firstSize.Height * firstSize.Width;
            var secondSquare = secondSize.Height * secondSize.Width;
            firstSquare.Should().BeGreaterThan(secondSquare);
        }
    }
}
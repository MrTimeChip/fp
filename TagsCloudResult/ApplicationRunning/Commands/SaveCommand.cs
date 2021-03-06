﻿using System;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudResult.ApplicationRunning.ConsoleApp.ConsoleCommands;
using TagsCloudResult.CloudVisualizers.ImageSaving;

namespace TagsCloudResult.ApplicationRunning.Commands
{
    public class SaveCommand : IConsoleCommand
    {
        private readonly TagsCloud cloud;

        private ImageFormat format;
        private string fullPath;
        private readonly SettingsManager manager;

        public SaveCommand(TagsCloud cloud, SettingsManager manager)
        {
            this.cloud = cloud;
            this.manager = manager;
        }

        public Result<string[]> ParseArguments(string[] args)
        {
            return Check.ArgumentsCountIs(args, 3)
                .Then(CheckPath)
                .Then(CheckImageFormat);
        }

        public Result<None> Act()
        {
            var result = Save(format, fullPath);
            if(result.IsSuccess)
                Console.WriteLine($"Successfully saved image at {fullPath}");
            return result;
        }

        public string Name => "save";
        public string Description => "saves visualized cloud to file";
        public string Arguments => "path name format";

        private Result<string[]> CheckPath(string[] args)
        {
            var errorMessage = $"Incorrect directory '{args[0]}'";
            var checkRes = Check.Argument(args[0], errorMessage, Directory.Exists(args[0]));
            return checkRes.IsSuccess ? Result.Ok(args) : Result.Fail<string[]>(checkRes.Error);
        }

        private Result<string[]> CheckImageFormat(string[] args)
        {
            var filename = args[1] + "." + args[2];
            fullPath = Path.Combine(args[0], filename);

            format = SupportedImageFormats.TryGetSupportedImageFormats(args[2]);
            var errorMessage = $"Incorrect image format '{args[2]}'";
            var checkRes = Check.Argument(args[2], errorMessage, format != null);
            return checkRes.IsSuccess ? Result.Ok(args) : Result.Fail<string[]>(checkRes.Error);
        }

        private Result<None> Save(ImageFormat format, string path)
        {
            manager.ConfigureImageSaverSettings(format, path);
            return cloud.SaveVisualized();
        }
    }
}
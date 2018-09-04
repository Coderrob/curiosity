﻿using System;
using NASA.Api;
using NASA.PhotoImporter.Importers;
using NASA.PhotoImporter.Services;
using NDesk.Options;
using NLog;

namespace NASA.PhotoImporter
{
    internal class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var showHelp = false;
            string outputPath = null;
            string inputFile = null;
            string apiKey = null;

            var p = new OptionSet
            {
                { "i=", "Input date file", v => inputFile = v },
                { "o=", "Output directory", v => outputPath = v },
                { "k|key=", "NASA API key", k => apiKey = k },
                { "h|help", "show this message and exit", v => showHelp = v != null }
            };

            try
            {
                p.Parse(args);

                if (showHelp)
                {
                    ShowHelp(p);
                    return;
                }

                if (string.IsNullOrEmpty(inputFile)) throw new OptionException("Missing required option -i", "Input date file");
                if (string.IsNullOrEmpty(outputPath)) throw new OptionException("Missing required option -o", "Output directory.");
                if (string.IsNullOrEmpty(apiKey)) throw new OptionException("Missing required option -k=key", "NASA API Key");

                var service = new PhotoSyncService(
                    new DateFileImporter(inputFile),
                    new RoverPhotoImporter(
                        new RoverClient(apiKey)));

                service.Export(outputPath).Wait();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to process the request. {ex.Message}");
                Console.Write("Import failure: ");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Try `--help' for more information.");
                return;
            }

            Console.WriteLine("Options:");
            Console.WriteLine("\tInput File: {0}", inputFile);
            Console.WriteLine("\tOutput Folder: {0}", outputPath);
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: importer [OPTIONS]+");
            Console.WriteLine("Program to import MARs rover images to a local folder.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
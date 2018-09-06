using System;
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
                { "h|help", "show this message and exit", v => showHelp = v != null },
                { "i=", "Input date file", v => inputFile = v },
                { "o=", "Output directory", v => outputPath = v },
                { "k|key=", "NASA API key", k => apiKey = k },                
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

                var service = new PhotoExportService(
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

            Console.WriteLine("Import complete:");
            Console.WriteLine("\tInput File: {0}", inputFile);
            Console.WriteLine("\tOutput Folder: {0}", outputPath);
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: NASA.PhotoImporter.dll [OPTIONS]+");
            Console.WriteLine("Import MARs rover photos to a local folder.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
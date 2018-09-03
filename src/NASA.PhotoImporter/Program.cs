using System;
using NASA.Api;
using NDesk.Options;

namespace NASA.PhotoImporter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var showHelp = false;
            string output = null;
            string input = null;
            string apiKey = null;

            var p = new OptionSet
            {
                { "i=", "Input date file", v => input = v },
                { "o=", "Output image directory", v => output = v },
                { "k|key=", "NASA Api key", k => apiKey = k },
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

                if (string.IsNullOrEmpty(input)) throw new OptionException("Missing required option -i", "Input date file");
                if (string.IsNullOrEmpty(output)) throw new OptionException("Missing required option -o", "Output image directory.");
                if (string.IsNullOrEmpty(apiKey)) throw new OptionException("Missing required option -k=key", "NASA Api Key");

                var service = new PhotoSyncService(
                    new DateImporter(input),
                    new RoverPhotoImporter(
                        new RoverClient(apiKey)));

                service.Export(output).Wait();
            }
            catch (OptionException e)
            {
                Console.Write("Import failure: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help' for more information.");
                return;
            }

            Console.WriteLine("Options:");
            Console.WriteLine("\tInput File: {0}", input);
            Console.WriteLine("\tOutput Folder: {0}", output);
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
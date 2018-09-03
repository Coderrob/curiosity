using System.Linq;

namespace NASA.PhotoImporter
{
    public static class FileNameExtensions
    {
        private static readonly string[] SupportedImages = { ".jpg" };

        public static bool IsSupportedImage(this string fileName)
        {
            return !string.IsNullOrEmpty(fileName) &&
                   SupportedImages.Any(fileName.ToLower().EndsWith);
        }
    }
}
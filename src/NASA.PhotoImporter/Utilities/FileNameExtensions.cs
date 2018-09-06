using System.Linq;

namespace NASA.PhotoImporter.Utilities
{
    public static class FileNameExtensions
    {
        private static readonly string[] SupportedImages = { ".jpg" };

        /// <summary>
        /// Gets whether a file is supported by examining its file extension.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Returns true if the file type is supported otherwise false.</returns>
        public static bool IsSupportedImage(this string fileName)
        {
            return !string.IsNullOrEmpty(fileName) &&
                   SupportedImages.Any(fileName.ToLower().EndsWith);
        }
    }
}
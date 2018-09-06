using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NASA.PhotoImporter.Tests")]

namespace NASA.PhotoImporter.Services
{
    internal interface IExportService
    {
        Task Export(string directoryPath);
    }
}
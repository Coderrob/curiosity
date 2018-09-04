using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA.PhotoImporter.Importers
{
    public interface IPhotoImporter
    {
        Task<IEnumerable<string>> GetPhotos(DateTime date);
    }
}
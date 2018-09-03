using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA.PhotoImporter
{
    public interface IPhotoImporter
    {
        Task<IEnumerable<string>> GetPhotos(DateTime date);
    }
}
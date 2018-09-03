using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NASA.Api.Cameras;

namespace NASA.Api.Rovers
{
    public interface IRover
    {
        string Name { get; }
        ICamera GetCamera(string abbreviation);
        IEnumerable<ICamera> GetCameras();
        Task<IEnumerable<Photo>> GetPhotosAsync(DateTime? date = null);
    }
}
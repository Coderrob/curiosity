using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA.Api.Cameras
{
    public interface ICamera
    {
        string Name { get; }
        string Abbreviation { get; }
        Task<IEnumerable<Photo>> GetPhotosAsync(DateTime? date = null);
    }
}
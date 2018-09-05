using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NASA.Api;

namespace NASA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        private readonly IRoverClient _roverClient;

        public RoverController(IRoverClient roverClient)
        {
            _roverClient = roverClient;
        }

        [HttpGet("{rover}")]
        [HttpGet("{rover}/{date}")]
        public async Task<IEnumerable<PhotoViewModel>> GetPhotos(string rover, string date)
        {
            DateTime? earthDate = null;
            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out var result))
                earthDate = result;

            var photos = await _roverClient.GetRover(rover).GetPhotosAsync(earthDate);

            return photos.Select(ToViewModel);
        }

        public static PhotoViewModel ToViewModel(Photo photo)
        {
            return new PhotoViewModel
            {
                Id = photo.Id,
                Source = photo.Source
            };
        }
    }

    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Source { get; set; }
    }
}
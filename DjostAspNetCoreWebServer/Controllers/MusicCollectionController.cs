using Asp.Versioning;
using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;
using DjostAspNetCoreWebServer.Authentication.Models.MusicCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/MusicCollection")]
    [ApiVersion(1)]
    [Authorize]
    public class MusicCollectionController : ControllerBase
    {
        private readonly IMusicCollectionService _musicCollectionService;

        public MusicCollectionController(IMusicCollectionService musicCollectionService) 
        {
            _musicCollectionService = musicCollectionService;
        }

        [Route("GetBandsByBandName")]
        [HttpPost]
        public async Task<IActionResult> GetBandsByBandName([FromBody] GetBandsByBandNameRequestDto request)
        {
            var response = await _musicCollectionService.GetBandsByBandNameAsync(request);
            return Ok(response);
        }

        [Route("GetAlbumsByBandId")]
        [HttpPost]
        public async Task<IActionResult> GetAlbumsByBandId([FromBody] GetAlbumsByBandIdRequestDto request)
        {
            var response = await _musicCollectionService.GetAlbumsByBandIdAsync(request);
            return Ok(response);
        }


        [Route("GetArtistsByBandId")]
        [HttpPost]
        public async Task<IActionResult> GetArtistsByBandId([FromBody] GetArtistsByBandIdRequestDto request)
        {
            var response = await _musicCollectionService.GetArtistsByBandIdAsync(request);
            return Ok(response);
        }


        [Route("GetSongsByAlbumId")]
        [HttpPost]
        public async Task<IActionResult> GetSongsByAlbumId([FromBody] GetSongsByAlbumIdRequestDto request)
        {
            var response = await _musicCollectionService.GetSongsByAlbumIdAsync(request);
            return Ok(response);
        }
    }
}

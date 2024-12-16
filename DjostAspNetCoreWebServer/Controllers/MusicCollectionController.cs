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

        [Route("GetBandByBandName")]
        [HttpPost]
        public async Task<IActionResult> GetBandByBandName([FromBody] GetBandByBandNameRequestDto request)
        {
            var response = await _musicCollectionService.GetBandByBandNameAsync(request);
            return Ok(response);
        }

        [Route("GetAlbumsByBandId")]
        [HttpPost]
        public async Task<IActionResult> GetAlbumsByBandId([FromBody] GetAlbumsByBandIdRequestDto request)
        {
            var response = await _musicCollectionService.GetAlbumsByBandIdAsync(request);
            return Ok(response);
        }


        [Route("GetBandMembershipByBandId")]
        [HttpPost]
        public async Task<IActionResult> GetBandMembershipByBandId([FromBody] GetBandMembershipByBandIdRequestDto request)
        {
            var response = await _musicCollectionService.GetBandMembershipByBandIdAsync(request);
            return Ok(response);
        }


        [Route("GetSongListByAlbumId")]
        [HttpPost]
        public async Task<IActionResult> GetSongListByAlbumId([FromBody] GetSongListByAlbumIdRequestDto request)
        {
            var response = await _musicCollectionService.GetSongListByAlbumIdAsync(request);
            return Ok(response);
        }
    }
}

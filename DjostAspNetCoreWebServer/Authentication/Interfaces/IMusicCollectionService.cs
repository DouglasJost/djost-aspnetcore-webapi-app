using AppDomainEntities;
using AppServiceCore;
using AppServiceCore.Models.MusicCollection;
using DjostAspNetCoreWebServer.Authentication.Models.MusicCollection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface IMusicCollectionService
    {
        Task<CommandResult<IEnumerable<MusicCollectionBandDto>>> GetBandsByBandNameAsync(GetBandsByBandNameRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>> GetAlbumsByBandIdAsync(GetAlbumsByBandIdRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>> GetArtistsByBandIdAsync(GetArtistsByBandIdRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionAlbumSongsDto>>> GetSongsByAlbumIdAsync(GetSongsByAlbumIdRequestDto requestDto);
    }
}

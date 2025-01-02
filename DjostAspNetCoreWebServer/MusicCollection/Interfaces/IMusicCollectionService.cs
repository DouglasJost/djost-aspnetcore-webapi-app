using AppDomainEntities;
using AppServiceCore;
using AppServiceCore.Models.MusicCollection;
using DjostAspNetCoreWebServer.MusicCollection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.MusicCollection.Interfaces
{
    public interface IMusicCollectionService
    {
        Task<CommandResult<IEnumerable<MusicCollectionBandDto>>> GetBandsByBandNameAsync(GetBandsByBandNameRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>> GetAlbumsByBandIdAsync(GetAlbumsByBandIdRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>> GetArtistsByBandIdAsync(GetArtistsByBandIdRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionAlbumSongsDto>>> GetSongsByAlbumIdAsync(GetSongsByAlbumIdRequestDto requestDto);

        Task<CommandResult<MusicCollectionBandDto?>> AddBandAsync(AddBandRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>> AddBandArtistsAsync(AddBandArtistsRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>> AddBandAlbumAsync(AddBandAlbumRequestDto requestDto);
    }
}

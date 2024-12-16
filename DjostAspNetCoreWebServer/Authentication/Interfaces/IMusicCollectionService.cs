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
        Task<CommandResult<IEnumerable<MusicCollectionBandDto>>> GetBandByBandNameAsync(GetBandByBandNameRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionAlbumDto>>> GetAlbumsByBandIdAsync(GetAlbumsByBandIdRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionBandMembershipDto>>> GetBandMembershipByBandIdAsync(GetBandMembershipByBandIdRequestDto requestDto);

        Task<CommandResult<IEnumerable<MusicCollectionSongListByAlbumDto>>> GetSongListByAlbumIdAsync(GetSongListByAlbumIdRequestDto requestDto);
    }
}

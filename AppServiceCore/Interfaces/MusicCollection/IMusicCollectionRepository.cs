using AppDomainEntities;
using AppServiceCore.Models.MusicCollection;
using AppServiceCore.Repositories.MusicCollection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.MusicCollection
{
    public interface IMusicCollectionRepository
    {
        Task<IEnumerable<MusicCollectionBandDto>> GetBandsByBandNameAsync(MusicCollectionDbContext dbContext, string bandName);

        Task<IEnumerable<MusicCollectionBandAlbumsDto>> GetAlbumsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionBandArtistsDto>> GetArtistsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionAlbumSongsDto>> GetSongsByAlbumIdAsync(MusicCollectionDbContext dbContext, Guid albumId);
    }
}

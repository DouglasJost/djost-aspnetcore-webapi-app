using AppDomainEntities;
using AppServiceCore.Models.MusicCollection;
using AppServiceCore.Services.DbTransactionService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.MusicCollection
{
    public interface IMusicCollectionRepository
    {
        Task<IEnumerable<MusicCollectionBandDto>> GetBandsByBandNameAsync(MusicCollectionDbContext dbContext, string bandName);

        Task<MusicCollectionBandDto?> GetBandByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionBandAlbumsDto>> GetAlbumsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionBandArtistsDto>> GetArtistsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionAlbumSongsDto>> GetSongsByAlbumIdAsync(MusicCollectionDbContext dbContext, Guid albumId);

        Task<DbOperationResult<Guid>> AddBandAsync(MusicCollectionDbContext dbContext, MusicCollectionBandDto requestDto);

        Task<DbOperationResult<Guid>> AddBandArtistsAsync(MusicCollectionDbContext dbContext, MusicCollectionBandArtistsDto bandArtistsDto);

        Task<List<Guid>> AddBandAlbumAsync(MusicCollectionDbContext dbContext, MusicCollectionBandAlbumsDto bandAlbumsDto);
    }
}

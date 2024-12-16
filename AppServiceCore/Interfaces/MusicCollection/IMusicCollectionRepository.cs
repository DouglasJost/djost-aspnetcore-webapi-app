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
        Task<IEnumerable<MusicCollectionBandDto>> GetBandByBandNameAsync(MusicCollectionDbContext dbContext, string bandName);

        Task<IEnumerable<MusicCollectionAlbumDto>> GetAlbumsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionBandMembershipDto>> GetBandMembershipByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId);

        Task<IEnumerable<MusicCollectionSongListByAlbumDto>> GetSongListByAlbumIdAsync(MusicCollectionDbContext dbContext, Guid albumId);
    }
}

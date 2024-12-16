using AppDomainEntities;
using AppDomainEntities.Entities;
using AppServiceCore.Interfaces.MusicCollection;
using AppServiceCore.Models.MusicCollection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Repositories.MusicCollection
{
    public class MusicCollectionRepository : IMusicCollectionRepository
    {
        public async Task<IEnumerable<MusicCollectionBandDto>> GetBandByBandNameAsync(MusicCollectionDbContext dbContext, string bandName)
        {
            if (string.IsNullOrWhiteSpace(bandName))
            {
                return new List<MusicCollectionBandDto>();
            }

            var responseDto = await dbContext.Database.SqlQuery<MusicCollectionBandDto>($"GetBandByBandName {bandName}").ToListAsync();

            return responseDto;
        }

        public async Task<IEnumerable<MusicCollectionAlbumDto>> GetAlbumsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return new List<MusicCollectionAlbumDto>();
            }

            var responseDto = await dbContext.Database.SqlQuery<MusicCollectionAlbumDto>($"GetAlbumsByBandId {bandId}").ToListAsync();
            return responseDto;
        }

        public async Task<IEnumerable<MusicCollectionBandMembershipDto>> GetBandMembershipByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return new List<MusicCollectionBandMembershipDto>();
            }

            var responseDto = await dbContext.Database.SqlQuery<MusicCollectionBandMembershipDto>($"GetBandMembershipByBandId {bandId}").ToListAsync();
            return responseDto;
        }

        public async Task<IEnumerable<MusicCollectionSongListByAlbumDto>> GetSongListByAlbumIdAsync(MusicCollectionDbContext dbContext, Guid albumId)
        {
            if (albumId == Guid.Empty)
            {
                return new List<MusicCollectionSongListByAlbumDto>();
            }

            var responseDto = await dbContext.Database.SqlQuery<MusicCollectionSongListByAlbumDto>($"GetSongListByAlbumId {albumId}").ToListAsync();
            return responseDto;
        }
    }
}

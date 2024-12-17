using AppDomainEntities;
using AppDomainEntities.Entities;
using AppServiceCore.AutoMapper;
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
        private readonly IAutoTypeMapper<MusicCollectionBandResult, MusicCollectionBandDto> _bandResultToBandDtoMapper;

        public MusicCollectionRepository(IAutoTypeMapper<MusicCollectionBandResult, MusicCollectionBandDto> bandResultToBandDtoMapper) 
        {
            _bandResultToBandDtoMapper = bandResultToBandDtoMapper;
        }

        public async Task<IEnumerable<MusicCollectionBandDto>> GetBandsByBandNameAsync(MusicCollectionDbContext dbContext, string bandName)
        {
            var responseDto = new List<MusicCollectionBandDto>();

            if (string.IsNullOrWhiteSpace(bandName))
            {
                return responseDto;
            }

            List<MusicCollectionBandResult> bandsByBandNameResult = await dbContext.Database.SqlQuery<MusicCollectionBandResult>($"GetBandsByBandName {bandName}").ToListAsync();
            if (bandsByBandNameResult != null) 
            {
                foreach (var band in bandsByBandNameResult)
                {
                    responseDto.Add(_bandResultToBandDtoMapper.Map(band));
                }
            }

            return responseDto;
        }

        public async Task<IEnumerable<MusicCollectionBandAlbumsDto>> GetAlbumsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return new List<MusicCollectionBandAlbumsDto>();
            }

            var albumsResult = await dbContext.Database.SqlQuery<MusicCollectionAlbumResult>($"GetAlbumsByBandId {bandId}").ToListAsync();
            
            List<MusicCollectionBandAlbumsDto> bandAlbumsDto = albumsResult
                .GroupBy(ar => new { ar.BandId, ar.BandName })
                .Select(group => new MusicCollectionBandAlbumsDto
                {
                    BandId = group.Key.BandId,
                    BandName = group.Key.BandName,
                    Albums = group.Select(album => new MusicCollectionAlbumDto
                    {
                        AlbumId = album.AlbumId,
                        AlbumTitle = album.AlbumTitle,
                        GenreName = album.GenreName,
                        RecordingLabel = album.RecordingLabel,
                        ReleaseDate = album.ReleaseDate,
                        PlaybackFormat = album.PlaybackFormat,
                        Country = album.Country
                    }).ToList()
                }).ToList();
            
            return bandAlbumsDto;
        }

        public async Task<IEnumerable<MusicCollectionBandArtistsDto>> GetArtistsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return new List<MusicCollectionBandArtistsDto>();
            }

            var bandArtistsResult = await dbContext.Database.SqlQuery<MusicCollectionBandArtistResult>($"GetArtistsByBandId {bandId}").ToListAsync();

            List<MusicCollectionBandArtistsDto> bandArtistsDto = bandArtistsResult
                .GroupBy(bar => new { bar.BandId, bar.BandName })
                .Select(group => new MusicCollectionBandArtistsDto
                {
                    BandId = group.Key.BandId,
                    BandName = group.Key.BandName,
                    Artists = group.Select(artist => new MusicCollectionArtistDto
                    {
                        ArtistId = artist.ArtistId,
                        ArtistFirstName = artist.ArtistFirstName,
                        ArtistLastName = artist.ArtistLastName,
                        Birthdate = artist.Birthdate,
                        Deathdate = artist.Deathdate,
                        Instrument = artist.Instrument,
                        City = artist.City,
                        Country = artist.Country
                    }).ToList()
                }).ToList();

            return bandArtistsDto;
        }

        public async Task<IEnumerable<MusicCollectionAlbumSongsDto>> GetSongsByAlbumIdAsync(MusicCollectionDbContext dbContext, Guid albumId)
        {
            if (albumId == Guid.Empty)
            {
                return new List<MusicCollectionAlbumSongsDto>();
            }

            var albumSongsResult = await dbContext.Database.SqlQuery<MusicCollectionSongsByAlbumResult>($"GetSongsByAlbumId {albumId}").ToListAsync();
            
            List<MusicCollectionAlbumSongsDto> albumSongsDto = albumSongsResult
                .GroupBy(asr => new { asr.AlbumId, asr.AlbumTitle })
                .Select(group => new MusicCollectionAlbumSongsDto
                {
                    AlbumId = group.Key.AlbumId,
                    AlbumTitle = group.Key.AlbumTitle,
                    Songs = group.Select(song => new MusicCollectionSongDto
                    {
                        SongId = song.SongId,
                        SongTitle = song.SongTitle,
                        TrackNumber = song.TrackNumber,
                        Duration = song.Duration,
                        Credits = song.Credits
                    }).ToList()
                }).ToList();

            return albumSongsDto;
        }
    }
}

using AppDomainEntities;
using AppDomainEntities.Entities;
using AppServiceCore.AutoMapper;
using AppServiceCore.Interfaces.MusicCollection;
using AppServiceCore.Logging;
using AppServiceCore.Models.MusicCollection;
using AppServiceCore.Services.DbTransactionService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace AppServiceCore.Repositories.MusicCollection
{
    public class MusicCollectionRepository : IMusicCollectionRepository
    {
        // Class-level fields used when remote debugging Azure Web App.
        // ========================================================================================================
        //   https://www.linkedin.com/pulse/asynclocalt-net-solution-shared-state-issues-code-madhan-kumar-66qqc/
        // ========================================================================================================
        #if DEBUG
        private static AsyncLocal<List<MusicCollectionBandResult>> _bandsByBandNameDbResult = new AsyncLocal<List<MusicCollectionBandResult>>();
        private static AsyncLocal<List<MusicCollectionBandDto>> _bandsByBandNameResponseDto = new AsyncLocal<List<MusicCollectionBandDto>>();
        
        private static AsyncLocal<Band?> _bandDbResult = new AsyncLocal<Band?>();
        private static AsyncLocal<MusicCollectionBandDto> _bandResponseDto = new AsyncLocal<MusicCollectionBandDto>();

        private static AsyncLocal<List<MusicCollectionAlbumResult>> _albumsByBandIdDbResult = new AsyncLocal<List<MusicCollectionAlbumResult>>();
        private static AsyncLocal<List<MusicCollectionBandAlbumsDto>> _albumsByBandIdResponseDto = new AsyncLocal<List<MusicCollectionBandAlbumsDto>>();

        private static AsyncLocal<List<MusicCollectionBandArtistResult>> _artistsByBandIdDbResult = new AsyncLocal<List<MusicCollectionBandArtistResult>>();
        private static AsyncLocal<List<MusicCollectionBandArtistsDto>> _artistsByBandIdResponseDto = new AsyncLocal<List<MusicCollectionBandArtistsDto>>();

        private static AsyncLocal<List<MusicCollectionSongsByAlbumResult>> _songsByAlbumIdDbResult = new AsyncLocal<List<MusicCollectionSongsByAlbumResult>>();
        private static AsyncLocal<List<MusicCollectionAlbumSongsDto>> _songsByAlbumIdResponseDto = new AsyncLocal<List<MusicCollectionAlbumSongsDto>>();
        #endif

        private readonly IDbTransactionService _dbTransactionService;
        //private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.AppLogger);

        private readonly IAutoTypeMapper<MusicCollectionBandResult, MusicCollectionBandDto> _bandResultToBandDtoMapper;
        private readonly IAutoTypeMapper<MusicCollectionBandDto, AppDomainEntities.Entities.Band> _bandDtoToBandEntityMapper;
        private readonly IAutoTypeMapper<AppDomainEntities.Entities.Band, MusicCollectionBandDto> _bandEntityToBandDtoMapper;
        private readonly IAutoTypeMapper<MusicCollectionAlbumResult, MusicCollectionAlbumDto> _albumResultToAlbumDtoMapper;
        private readonly IAutoTypeMapper<MusicCollectionBandArtistResult, MusicCollectionArtistDto> _artistResultToArtistDtoMapper;
        private readonly IAutoTypeMapper<MusicCollectionSongsByAlbumResult, MusicCollectionSongDto> _songsByAlbumResultToSongDtoMapper;
        private readonly IAutoTypeMapper<MusicCollectionArtistDto, AppDomainEntities.Entities.Artist> _artistDtoToArtistEntityMapper;
        private readonly IAutoTypeMapper<MusicCollectionAlbumDto, AppDomainEntities.Entities.Album> _albumDtoToAlbumEntityMapper;

        public MusicCollectionRepository(
            IDbTransactionService dbTransactionService,
            IAutoTypeMapper<MusicCollectionBandResult, MusicCollectionBandDto> bandResultToBandDtoMapper,
            IAutoTypeMapper<MusicCollectionBandDto, AppDomainEntities.Entities.Band> bandDtoToBandEntityMapper,
            IAutoTypeMapper<AppDomainEntities.Entities.Band, MusicCollectionBandDto> bandEntityToBandDtoMapper,
            IAutoTypeMapper<MusicCollectionAlbumResult, MusicCollectionAlbumDto> albumResultToAlbumDtoMapper,
            IAutoTypeMapper<MusicCollectionBandArtistResult, MusicCollectionArtistDto> artistResultToArtistDtoMapper,
            IAutoTypeMapper<MusicCollectionSongsByAlbumResult, MusicCollectionSongDto> songsByAlbumResultToSongDtoMapper,
            IAutoTypeMapper<MusicCollectionArtistDto, AppDomainEntities.Entities.Artist> artistDtoToArtistEntityMapper,
            IAutoTypeMapper<MusicCollectionAlbumDto, AppDomainEntities.Entities.Album> albumDtoToAlbumEntityMapper) 
        {
            _dbTransactionService = dbTransactionService;   
            _bandResultToBandDtoMapper = bandResultToBandDtoMapper;
            _bandDtoToBandEntityMapper = bandDtoToBandEntityMapper;
            _bandEntityToBandDtoMapper = bandEntityToBandDtoMapper;
            _albumResultToAlbumDtoMapper = albumResultToAlbumDtoMapper;
            _artistResultToArtistDtoMapper = artistResultToArtistDtoMapper;
            _songsByAlbumResultToSongDtoMapper = songsByAlbumResultToSongDtoMapper;
            _artistDtoToArtistEntityMapper = artistDtoToArtistEntityMapper;
            _albumDtoToAlbumEntityMapper = albumDtoToAlbumEntityMapper;
        }

        public async Task<IEnumerable<MusicCollectionBandDto>> GetBandsByBandNameAsync(MusicCollectionDbContext dbContext, string bandName)
        {
            var bandsByBandNameResponseDto = new List<MusicCollectionBandDto>();
            if (string.IsNullOrWhiteSpace(bandName))
            {
                return bandsByBandNameResponseDto;
            }

            var bandsByBandNameDbResult = await dbContext.Database.SqlQuery<MusicCollectionBandResult>($"GetBandsByBandName {bandName}").ToListAsync();

            #if DEBUG
                _bandsByBandNameDbResult.Value = bandsByBandNameDbResult;
                Debug.WriteLine($"_bandsByBandNameDbResult : {JsonConvert.SerializeObject(_bandsByBandNameDbResult.Value)}");
                //_logger.LogInformation($"_bandsByBandNameDbResult : {JsonConvert.SerializeObject(_bandsByBandNameDbResult)}");
            #endif

            if (bandsByBandNameDbResult != null) 
            {
                foreach (var band in bandsByBandNameDbResult)
                {
                    bandsByBandNameResponseDto.Add(_bandResultToBandDtoMapper.Map(band));
                }
            }

            #if DEBUG 
                _bandsByBandNameResponseDto.Value = bandsByBandNameResponseDto;
            #endif

            return bandsByBandNameResponseDto;
        }

        public async Task<MusicCollectionBandDto?> GetBandByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return null;
            }

            var bandEntity = await dbContext.Bands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BandId == bandId);
            #if DEBUG
                _bandDbResult.Value = bandEntity;
            #endif

            if (bandEntity == null)
            {
                return null;
            }

            var responseDto = _bandEntityToBandDtoMapper.Map(bandEntity);
            #if DEBUG
                _bandResponseDto.Value = responseDto;
            #endif

            return responseDto;
        }

        public async Task<IEnumerable<MusicCollectionBandAlbumsDto>> GetAlbumsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return new List<MusicCollectionBandAlbumsDto>();
            }

            var albumsResult = await dbContext.Database.SqlQuery<MusicCollectionAlbumResult>($"GetAlbumsByBandId {bandId}").ToListAsync();
            if (albumsResult == null || albumsResult.Count <= 0)
            {
                return new List<MusicCollectionBandAlbumsDto>();
            }

            #if DEBUG
                _albumsByBandIdDbResult.Value = albumsResult;
            #endif

            List<MusicCollectionBandAlbumsDto> bandAlbumsDto = albumsResult
                .GroupBy(ar => new { ar.BandId, ar.BandName })
                .Select(group => new MusicCollectionBandAlbumsDto
                {
                    BandId = group.Key.BandId.GetValueOrDefault(),
                    BandName = group.Key.BandName,
                    Albums = group.Select(album => _albumResultToAlbumDtoMapper.Map(album)).ToList()
                }).ToList();

            #if DEBUG
                _albumsByBandIdResponseDto.Value = bandAlbumsDto;
            #endif

            return bandAlbumsDto;
        }

        public async Task<IEnumerable<MusicCollectionBandArtistsDto>> GetArtistsByBandIdAsync(MusicCollectionDbContext dbContext, Guid bandId)
        {
            if (bandId == Guid.Empty)
            {
                return new List<MusicCollectionBandArtistsDto>();
            }

            var bandArtistsResult = await dbContext.Database.SqlQuery<MusicCollectionBandArtistResult>($"GetArtistsByBandId {bandId}").ToListAsync();
            if (bandArtistsResult == null || bandArtistsResult.Count <= 0)
            {
                return new List<MusicCollectionBandArtistsDto>();
            }

            #if DEBUG
                _artistsByBandIdDbResult.Value = bandArtistsResult;
            #endif

            List<MusicCollectionBandArtistsDto> bandArtistsDto = bandArtistsResult
                .GroupBy(bar => new { bar.BandId, bar.BandName })
                .Select(group => new MusicCollectionBandArtistsDto
                {
                    BandId = group.Key.BandId,
                    BandName = group.Key.BandName,
                    Artists = group.Select(artist => _artistResultToArtistDtoMapper.Map(artist)).ToList()
                }).ToList();

            #if DEBUG
                _artistsByBandIdResponseDto.Value = bandArtistsDto;
            #endif

            return bandArtistsDto;
        }

        public async Task<IEnumerable<MusicCollectionAlbumSongsDto>> GetSongsByAlbumIdAsync(MusicCollectionDbContext dbContext, Guid albumId)
        {
            if (albumId == Guid.Empty)
            {
                return new List<MusicCollectionAlbumSongsDto>();
            }

            var albumSongsResult = await dbContext.Database.SqlQuery<MusicCollectionSongsByAlbumResult>($"GetSongsByAlbumId {albumId}").ToListAsync();
            if (albumSongsResult == null || albumSongsResult.Count <= 0)
            {
                return new List<MusicCollectionAlbumSongsDto>();
            }

            #if DEBUG
                _songsByAlbumIdDbResult.Value = albumSongsResult;
            #endif

            List<MusicCollectionAlbumSongsDto> albumSongsDto = albumSongsResult
                .GroupBy(asr => new { asr.AlbumId, asr.AlbumTitle })
                .Select(group => new MusicCollectionAlbumSongsDto
                {
                    AlbumId = group.Key.AlbumId,
                    AlbumTitle = group.Key.AlbumTitle,
                    Songs = group.Select(song => _songsByAlbumResultToSongDtoMapper.Map(song)).ToList()
                }).ToList();

            #if DEBUG
                _songsByAlbumIdResponseDto.Value = albumSongsDto;
            #endif

            return albumSongsDto;
        }

        public async Task<DbOperationResult<Guid>> AddBandAsync(MusicCollectionDbContext dbContext, MusicCollectionBandDto requestDto)
        {
            if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.BandName))
            {
                return new DbOperationResult<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    ErrorMessage = $"Band name is a required input."
                };
            }

            var dbTransResult = await _dbTransactionService.ExecuteWithTransactionAsync(dbContext, async () =>
            {
                var bandEntity = _bandDtoToBandEntityMapper.Map(requestDto);
                dbContext.Bands.Add(bandEntity);
                await dbContext.SaveChangesAsync();

                return bandEntity.BandId;
            });

            return dbTransResult;
        }

        public async Task<DbOperationResult<Guid>> AddBandArtistsAsync(MusicCollectionDbContext dbContext, MusicCollectionBandArtistsDto bandArtistsDto)
        {
            if (bandArtistsDto == null ||
                bandArtistsDto.BandId == Guid.Empty ||
                bandArtistsDto.Artists == null ||
                bandArtistsDto.Artists.Count <= 0)
            {
                return new DbOperationResult<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    ErrorMessage = $"Band Id and Artists are required inputs."
                };
            }

            if (await this.GetBandByBandIdAsync(dbContext, bandArtistsDto.BandId) == null)
            {
                return new DbOperationResult<Guid>
                {
                    Success = false,
                    Data = Guid.Empty,
                    ErrorMessage = $"Band Id does not exits."
                };
            }

            var dbTransResult = await _dbTransactionService.ExecuteWithTransactionAsync(dbContext, async () =>
            {
                // Add artists to db
                var artists = new List<Artist>();
                foreach (var artistDto in bandArtistsDto.Artists)
                {
                    artists.Add(_artistDtoToArtistEntityMapper.Map(artistDto));
                }

                dbContext.Artists.AddRange(artists);
                await dbContext.SaveChangesAsync();

                // Create BandMemberships to associated the Artists with the Band
                var bandMemberships = artists.Select(artist => new BandMembership
                {
                    BandId = bandArtistsDto.BandId,
                    ArtistId = artist.ArtistId,  // Auto populated after SaveChangesAsync()
                }).ToList();

                dbContext.BandMemberships.AddRange(bandMemberships);
                await dbContext.SaveChangesAsync();

                return bandArtistsDto.BandId;
            });

            return dbTransResult;
        }

        public async Task<List<Guid>> AddBandAlbumAsync(MusicCollectionDbContext dbContext, MusicCollectionBandAlbumsDto bandAlbumsDto)
        {
            if (bandAlbumsDto == null || 
                bandAlbumsDto.BandId == Guid.Empty || 
                bandAlbumsDto.Albums == null || 
                bandAlbumsDto.Albums.Count <= 0)
            {
                return new List<Guid>();
            }

            if (await this.GetBandByBandIdAsync(dbContext, bandAlbumsDto.BandId) == null)
            {
                return new List<Guid>();
            }

            // Add albums to db
            var albums = new List<Album>();
            foreach (var albumDto in bandAlbumsDto.Albums)
            {
                albums.Add(_albumDtoToAlbumEntityMapper.Map(albumDto));
            }

            dbContext.Albums.AddRange(albums);
            await dbContext.SaveChangesAsync();

            var albumIds = albums.Select(album => album.AlbumId).ToList();

            return albumIds;
        }
    }
}

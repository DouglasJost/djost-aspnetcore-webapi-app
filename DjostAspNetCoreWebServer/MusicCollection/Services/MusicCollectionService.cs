using AppDomainEntities;
using AppServiceCore;
using AppServiceCore.Interfaces.MusicCollection;
using AppServiceCore.Models.MusicCollection;
using AppServiceCore.Services.DbTransactionService;
using DjostAspNetCoreWebServer.MusicCollection.Interfaces;
using DjostAspNetCoreWebServer.MusicCollection.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.MusicCollection.Services
{
    public class MusicCollectionService : IMusicCollectionService
    {
        private readonly IMusicCollectionRepository _musicCollectionRepository;
        private readonly IDbContextFactory<MusicCollectionDbContext> _dbContextFactory;

        public MusicCollectionService(
            IDbContextFactory<MusicCollectionDbContext> dbContextFactory,
            IMusicCollectionRepository musicCollectionRepository)
        {
            _dbContextFactory = dbContextFactory;
            _musicCollectionRepository = musicCollectionRepository;
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandDto>>> GetBandsByBandNameAsync(GetBandsByBandNameRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.BandName))
                {
                    return CommandResult<IEnumerable<MusicCollectionBandDto>>.Failure("Band name cannot be null.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    var response = await _musicCollectionRepository.GetBandsByBandNameAsync(dbContext, requestDto.BandName);
                    return CommandResult<IEnumerable<MusicCollectionBandDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetBandsByBandNameAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>> GetAlbumsByBandIdAsync(GetAlbumsByBandIdRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.BandId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Failure("BandId cannot be an empty GUID.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    var response = await _musicCollectionRepository.GetAlbumsByBandIdAsync(dbContext, requestDto.BandId);
                    return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetAlbumsByBandIdAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>> GetArtistsByBandIdAsync(GetArtistsByBandIdRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.BandId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Failure("BandId cannot be an empty GUID.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    var response = await _musicCollectionRepository.GetArtistsByBandIdAsync(dbContext, requestDto.BandId);
                    return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetArtistsByBandIdAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionAlbumSongsDto>>> GetSongsByAlbumIdAsync(GetSongsByAlbumIdRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.AlbumId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionAlbumSongsDto>>.Failure("AlbumId cannot be an empty GUID.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    var response = await _musicCollectionRepository.GetSongsByAlbumIdAsync(dbContext, requestDto.AlbumId);
                    return CommandResult<IEnumerable<MusicCollectionAlbumSongsDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetSongsByAlbumIdAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionAlbumSongsDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<MusicCollectionBandDto?>> AddBandAsync(AddBandRequestDto requestDto)
        {
            try 
            {
                if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.Name))
                {
                    return CommandResult<MusicCollectionBandDto?>.Failure("Band name is a required field.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    MusicCollectionBandDto bandDto = new MusicCollectionBandDto
                    {
                        BandId = Guid.Empty,
                        BandName = requestDto.Name,
                        FormationDate = requestDto.FormationDate,
                        DisbandDate = requestDto.DisbandDate,
                        City = requestDto.City,
                        Country = requestDto.Country
                    };

                    DbOperationResult<Guid> dbOperationResult = await _musicCollectionRepository.AddBandAsync(dbContext, bandDto);
                    if (!dbOperationResult.Success)
                    {
                        return CommandResult<MusicCollectionBandDto?>.Failure(
                            !string.IsNullOrWhiteSpace(dbOperationResult.ErrorMessage)
                                ? dbOperationResult.ErrorMessage
                                : "Error calling AddBandAsync().  New Band object not inserted into Db.");
                    }

                    var responseDto = await _musicCollectionRepository.GetBandByBandIdAsync(dbContext, dbOperationResult.Data);

                    return CommandResult<MusicCollectionBandDto?>.Success(responseDto);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling AddBandAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<MusicCollectionBandDto?>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>> AddBandArtistsAsync(AddBandArtistsRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.BandId == Guid.Empty || string.IsNullOrWhiteSpace(requestDto.BandName))
                {
                    return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Failure("Band Id and Name are required values.");
                }
                if (requestDto.Artists == null || requestDto.Artists.Count <= 0)
                {
                    return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Failure("Artists list cannot be empty.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    MusicCollectionBandArtistsDto bandArtistsDto = new MusicCollectionBandArtistsDto
                    {
                        BandId = requestDto.BandId,
                        BandName = requestDto.BandName
                    };
                    foreach (var artistRequestDto in requestDto.Artists)
                    {
                        MusicCollectionArtistDto artistDto = new MusicCollectionArtistDto
                        {
                            ArtistFirstName = artistRequestDto.FirstName,
                            ArtistLastName = artistRequestDto.LastName,
                            Birthdate = artistRequestDto.Birthdate,
                            Deathdate = artistRequestDto.Deathdate,
                            City = artistRequestDto.City,
                            Country = artistRequestDto.Country,
                            Instrument = artistRequestDto.Instrument
                        };
                        bandArtistsDto.Artists.Add(artistDto);
                    }

                    DbOperationResult<Guid> dbOperationResult = await _musicCollectionRepository.AddBandArtistsAsync(dbContext, bandArtistsDto);
                    if (!dbOperationResult.Success)
                    {
                        return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Failure(
                            !string.IsNullOrWhiteSpace(dbOperationResult.ErrorMessage)
                                ? dbOperationResult.ErrorMessage
                                : "Error calling AddBandArtistsAsync().  Band artists not added.");
                    }

                    var responseDto = await _musicCollectionRepository.GetArtistsByBandIdAsync(dbContext, dbOperationResult.Data);

                    return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Success(responseDto);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling AddBandArtistsAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandArtistsDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>> AddBandAlbumAsync(AddBandAlbumRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.BandId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Failure("Band Id must be defined.");
                }

                if (requestDto.Albums == null || requestDto.Albums.Count <= 0)
                {
                    return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Failure("Albums list cannot be empty.");
                }

                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    MusicCollectionBandAlbumsDto bandAlbumsDto = new MusicCollectionBandAlbumsDto
                    {
                        BandId = requestDto.BandId,
                        BandName = requestDto.BandName,
                    };
                    foreach (var albumRequestDto in requestDto.Albums)
                    {
                        MusicCollectionAlbumDto albumDto = new MusicCollectionAlbumDto
                        {
                            BandId = requestDto.BandId,
                            ArtistId = null,  // TODO : Implement an album being mapped to an Artist.
                            AlbumTitle = albumRequestDto.AlbumTitle,
                            RecordingLabel = albumRequestDto.RecordingLabel,
                            PlaybackFormat = albumRequestDto.PlaybackFormat,
                            Country = albumRequestDto.Country,
                            ReleaseDate = albumRequestDto.ReleaseDate,
                            GenreId = albumRequestDto.GenreId,
                        };
                        bandAlbumsDto.Albums.Add(albumDto);
                    }

                    var albumIds = await _musicCollectionRepository.AddBandAlbumAsync(dbContext, bandAlbumsDto);
                    if (albumIds == null || albumIds.Count <= 0)
                    {
                        return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Failure("BandId is not valid.  Related Band does not exist.");
                    }

                    var responseDto = await _musicCollectionRepository.GetAlbumsByBandIdAsync(dbContext, requestDto.BandId);

                    return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Success(responseDto);
                }

            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling AddBandAlbumAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandAlbumsDto>>.Failure(sbError.ToString());
            }
        }

    }
}

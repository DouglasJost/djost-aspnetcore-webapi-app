using AppDomainEntities;
using AppServiceCore;
using AppServiceCore.Interfaces.MusicCollection;
using AppServiceCore.Models.MusicCollection;
using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;
using DjostAspNetCoreWebServer.Authentication.Models.MusicCollection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Services
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

                //using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
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

                //using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
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

                //using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
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
    }
}

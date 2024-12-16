using AppDomainEntities;
using AppServiceCore;
using AppServiceCore.Interfaces.MusicCollection;
using AppServiceCore.Models.MusicCollection;
using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;
using DjostAspNetCoreWebServer.Authentication.Models.MusicCollection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Services
{
    public class MusicCollectionService : IMusicCollectionService
    {
        private readonly IMusicCollectionRepository _musicCollectionRepository;

        public MusicCollectionService(IMusicCollectionRepository musicCollectionRepository)
        {
            _musicCollectionRepository = musicCollectionRepository;
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandDto>>> GetBandByBandNameAsync(GetBandByBandNameRequestDto requestDto)
        {
            try 
            {
                if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.BandName))
                {
                    return CommandResult<IEnumerable<MusicCollectionBandDto>>.Failure("Band name cannot be null.");
                }

                using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
                {
                    var response = await _musicCollectionRepository.GetBandByBandNameAsync(dbContext, requestDto.BandName);
                    return CommandResult<IEnumerable<MusicCollectionBandDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetBandByBandNameAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionAlbumDto>>> GetAlbumsByBandIdAsync(GetAlbumsByBandIdRequestDto requestDto)
        {
            try 
            {
                if (requestDto == null || requestDto.BandId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionAlbumDto>>.Failure("BandId cannot be an empty GUID.");
                }

                using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
                {
                    var response = await _musicCollectionRepository.GetAlbumsByBandIdAsync(dbContext, requestDto.BandId);
                    return CommandResult<IEnumerable<MusicCollectionAlbumDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetAlbumsByBandIdAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionAlbumDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionBandMembershipDto>>> GetBandMembershipByBandIdAsync(GetBandMembershipByBandIdRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.BandId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionBandMembershipDto>>.Failure("BandId cannot be an empty GUID.");
                }

                using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
                {
                    var response = await _musicCollectionRepository.GetBandMembershipByBandIdAsync(dbContext, requestDto.BandId);
                    return CommandResult<IEnumerable<MusicCollectionBandMembershipDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetBandMembershipByBandIdAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionBandMembershipDto>>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<IEnumerable<MusicCollectionSongListByAlbumDto>>> GetSongListByAlbumIdAsync(GetSongListByAlbumIdRequestDto requestDto)
        {
            try
            {
                if (requestDto == null || requestDto.AlbumId == Guid.Empty)
                {
                    return CommandResult<IEnumerable<MusicCollectionSongListByAlbumDto>>.Failure("AlbumId cannot be an empty GUID.");
                }

                using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
                {
                    var response = await _musicCollectionRepository.GetSongListByAlbumIdAsync(dbContext, requestDto.AlbumId);
                    return CommandResult<IEnumerable<MusicCollectionSongListByAlbumDto>>.Success(response);
                }
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error calling GetSongListByAlbumIdAsync().");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<IEnumerable<MusicCollectionSongListByAlbumDto>>.Failure(sbError.ToString());
            }
        }
    }
}

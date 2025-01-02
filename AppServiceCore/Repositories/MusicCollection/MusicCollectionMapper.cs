using AppServiceCore.Models.MusicCollection;
using AutoMapper;
using System;

namespace AppServiceCore.Repositories.MusicCollection
{
    public class MusicCollectionMapper : Profile
    {
        public MusicCollectionMapper()
        {
            CreateMap<MusicCollectionBandResult, MusicCollectionBandDto>()
                .ForMember(d => d.BandId, o => o.MapFrom(s => s.BandId))
                .ForMember(d => d.BandName, o => o.MapFrom(s => s.BandName))
                .ForMember(d => d.FormationDate, o => o.MapFrom(s => s.FormationDate))
                .ForMember(d => d.DisbandDate, o => o.MapFrom(s => s.DisbandDate))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country));

            CreateMap<MusicCollectionBandDto, AppDomainEntities.Entities.Band>()
                .ForMember(d => d.BandId, o => o.MapFrom(s => Guid.Empty))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.BandName))
                .ForMember(d => d.FormationDate, o => o.MapFrom(s => s.FormationDate))
                .ForMember(d => d.DisbandDate, o => o.MapFrom(s => s.DisbandDate))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country));

            CreateMap<AppDomainEntities.Entities.Band, MusicCollectionBandDto>()
                .ForMember(d => d.BandId, o => o.MapFrom(s => s.BandId))
                .ForMember(d => d.BandName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.FormationDate, o => o.MapFrom(s => s.FormationDate))
                .ForMember(d => d.DisbandDate, o => o.MapFrom(s => s.DisbandDate))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country));

            CreateMap<MusicCollectionAlbumResult, MusicCollectionAlbumDto>()
                .ForMember(d => d.AlbumId, o => o.MapFrom(s => s.AlbumId))
                .ForMember(d => d.BandId, o => o.MapFrom(s => s.BandId))
                .ForMember(d => d.ArtistId, o => o.MapFrom(s => s.ArtistId))
                .ForMember(d => d.AlbumTitle, o => o.MapFrom(s => s.AlbumTitle))
                .ForMember(d => d.GenreName, o => o.MapFrom(s => s.GenreName))
                .ForMember(d => d.RecordingLabel, o => o.MapFrom(s => s.RecordingLabel))
                .ForMember(d => d.ReleaseDate, o => o.MapFrom(s => s.ReleaseDate))
                .ForMember(d => d.PlaybackFormat, o => o.MapFrom(s => s.PlaybackFormat))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country));

            CreateMap<MusicCollectionBandArtistResult, MusicCollectionArtistDto>()
                .ForMember(d => d.ArtistId, o => o.MapFrom(s => s.ArtistId))
                .ForMember(d => d.ArtistFirstName, o => o.MapFrom(s => s.ArtistFirstName))
                .ForMember(d => d.ArtistLastName, o => o.MapFrom(s => s.ArtistLastName))
                .ForMember(d => d.Birthdate, o => o.MapFrom(s => s.Birthdate))
                .ForMember(d => d.Deathdate, o => o.MapFrom(s => s.Deathdate))
                .ForMember(d => d.Instrument, o => o.MapFrom(s => s.Instrument))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country));

            CreateMap<MusicCollectionSongsByAlbumResult, MusicCollectionSongDto>()
                .ForMember(d => d.SongId, o => o.MapFrom(s => s.SongId))
                .ForMember(d => d.SongTitle, o => o.MapFrom(s => s.SongTitle))
                .ForMember(d => d.TrackNumber, o => o.MapFrom(s => s.TrackNumber))
                .ForMember(d => d.Duration, o => o.MapFrom(s => s.Duration))
                .ForMember(d => d.Credits, o => o.MapFrom(s => s.Credits));

            CreateMap<MusicCollectionArtistDto, AppDomainEntities.Entities.Artist>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.ArtistFirstName == null ? string.Empty : s.ArtistFirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.ArtistLastName == null ? string.Empty : s.ArtistLastName))
                .ForMember(d => d.Birthdate, o => o.MapFrom(s => s.Birthdate))
                .ForMember(d => d.Deathdate, o => o.MapFrom(s => s.Deathdate))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
                .ForMember(d => d.Instrument, o => o.MapFrom(s => s.Instrument));

            CreateMap<MusicCollectionAlbumDto, AppDomainEntities.Entities.Album>()
                .ForMember(d => d.BandId, o => o.MapFrom(s => s.BandId))
                .ForMember(d => d.ArtistId, o => o.MapFrom(s => s.ArtistId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.AlbumTitle))
                .ForMember(d => d.RecordingLabel, o => o.MapFrom(s => s.RecordingLabel))
                .ForMember(d => d.PlaybackFormat, o => o.MapFrom(s => s.PlaybackFormat))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
                .ForMember(d => d.ReleaseDate, o => o.MapFrom(s => s.ReleaseDate))
                .ForMember(d => d.GenreId, o => o.MapFrom(s => s.GenreId));
        }
    }
}

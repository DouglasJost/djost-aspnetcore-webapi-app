using System;
using System.Collections.Generic;

namespace AppServiceCore.Models.MusicCollection
{
    public class MusicCollectionBandArtistResult
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; } = string.Empty;
        public Guid? ArtistId { get; set; }
        public string? ArtistFirstName { get; set; }
        public string? ArtistLastName { get; set; }
        public DateOnly? Birthdate { get; set; }
        public DateOnly? Deathdate { get; set; }
        public string? Instrument { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    public class MusicCollectionBandArtistsDto
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; } = string.Empty;

        public List<MusicCollectionArtistDto> Artists { get; set; } = new List<MusicCollectionArtistDto>();
    }

    public class MusicCollectionArtistDto
    {
        public Guid? ArtistId { get; set; }
        public string? ArtistFirstName { get; set; }
        public string? ArtistLastName { get; set; }
        public DateOnly? Birthdate { get; set; }
        public DateOnly? Deathdate { get; set; }
        public string? Instrument { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}

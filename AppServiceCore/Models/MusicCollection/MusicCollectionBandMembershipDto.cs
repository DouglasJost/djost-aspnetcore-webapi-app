using System;

namespace AppServiceCore.Models.MusicCollection
{
    public class MusicCollectionBandMembershipDto
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; } = string.Empty;
        public Guid? ArtistId { get; set; }
        public string? ArtistFirstName { get; set; }
        public string? ArtistLastName { get; set; }
        public DateOnly? Birthdate { get; set; }
        public DateOnly? Deathdate { get; set; }
        public string? Instrument {  get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}

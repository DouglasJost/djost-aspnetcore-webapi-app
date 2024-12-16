using System;

namespace AppServiceCore.Models.MusicCollection
{
    public class MusicCollectionBandDto
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; } = string.Empty;
        public DateOnly? FormationDate { get; set; }
        public DateOnly? DisbandDate { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}

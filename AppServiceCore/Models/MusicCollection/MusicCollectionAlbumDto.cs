using System;
using System.Collections.Generic;

namespace AppServiceCore.Models.MusicCollection
{
    public class MusicCollectionAlbumResult
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; } = string.Empty;
        public Guid AlbumId { get; set; }
        public string AlbumTitle { get; set; } = string.Empty;
        public string? GenreName { get; set; }
        public string? RecordingLabel { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? PlaybackFormat { get; set; }
        public string? Country { get; set; }
    }

    public class MusicCollectionBandAlbumsDto
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; } = string.Empty;
        public List<MusicCollectionAlbumDto> Albums { get; set; } = new List<MusicCollectionAlbumDto>();
    }
    public class MusicCollectionAlbumDto
    {
        public Guid AlbumId { get; set; }
        public string AlbumTitle { get; set; } = string.Empty;
        public string? GenreName { get; set; }
        public string? RecordingLabel { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? PlaybackFormat { get; set; }
        public string? Country { get; set; }
    }
}

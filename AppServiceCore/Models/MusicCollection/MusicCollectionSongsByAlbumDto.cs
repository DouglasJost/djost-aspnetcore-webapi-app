using System;
using System.Collections.Generic;

namespace AppServiceCore.Models.MusicCollection
{
    public class MusicCollectionSongsByAlbumResult
    {
        public Guid AlbumId { get; set; }
        public Guid? BandId { get; set; }
        public Guid? ArtistId { get; set; }
        public string AlbumTitle { get; set; } = string.Empty;
        public Guid SongId { get; set; }
        public string SongTitle { get; set; } = string.Empty;
        public string TrackNumber { get; set; } = string.Empty;
        public TimeOnly? Duration { get; set; }
        public string? Credits { get; set; }
    }

    public class MusicCollectionAlbumSongsDto
    {
        public Guid AlbumId { get; set; }
        public string AlbumTitle { get; set; } = string.Empty;
        public List<MusicCollectionSongDto> Songs { get; set; } = new List<MusicCollectionSongDto>();
    }

    public class MusicCollectionSongDto
    {
        public Guid SongId { get; set; }
        public string SongTitle { get; set; } = string.Empty;
        public string TrackNumber { get; set; } = string.Empty;
        public TimeOnly? Duration { get; set; }
        public string? Credits { get; set; }
    }
}

using System;

namespace AppServiceCore.Models.MusicCollection
{
    public class MusicCollectionSongListByAlbumDto
    {
        public Guid AlbumId { get; set; }
        public Guid? BandId { get; set; }
        public Guid? ArtistId { get; set; }
        public string AlbumTitle { get; set; } = string.Empty;
        public Guid SongId { get; set; }
        public string SongTitle { get; set;} = string.Empty;
        public string TrackNumber {  get; set; } = string.Empty;
        public TimeOnly? Duration { get; set; }
        public string? Credits { get; set; }
    }
}

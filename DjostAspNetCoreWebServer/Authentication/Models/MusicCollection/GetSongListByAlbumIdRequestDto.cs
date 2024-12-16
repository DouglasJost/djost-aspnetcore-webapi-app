using System;

namespace DjostAspNetCoreWebServer.Authentication.Models.MusicCollection
{
    public class GetSongListByAlbumIdRequestDto
    {
        public Guid AlbumId { get; set; }
    }
}

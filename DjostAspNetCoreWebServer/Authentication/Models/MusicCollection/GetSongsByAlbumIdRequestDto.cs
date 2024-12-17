using System;

namespace DjostAspNetCoreWebServer.Authentication.Models.MusicCollection
{
    public class GetSongsByAlbumIdRequestDto
    {
        public Guid AlbumId { get; set; }
    }
}

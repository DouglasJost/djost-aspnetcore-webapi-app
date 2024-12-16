using System;

namespace DjostAspNetCoreWebServer.Authentication.Models.MusicCollection
{
    public class GetAlbumsByBandIdRequestDto
    {
        public Guid BandId { get; set; }
    }
}

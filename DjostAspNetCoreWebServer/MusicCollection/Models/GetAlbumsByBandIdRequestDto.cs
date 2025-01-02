using AppServiceCore.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.MusicCollection.Models
{
    public class GetAlbumsByBandIdRequestDto
    {
        [Required(ErrorMessage = "Band Id is a required field.")]
        [NotEqual("00000000-0000-0000-0000-000000000000")] // Custom Validation Attribute : Guid.Empty is not allowed
        public Guid BandId { get; set; }
    }
}

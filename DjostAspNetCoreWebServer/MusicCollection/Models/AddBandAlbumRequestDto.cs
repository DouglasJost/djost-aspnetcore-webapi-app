using AppServiceCore.CustomAttributes;
using Microsoft.OpenApi.MicrosoftExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.MusicCollection.Models
{
    public class AddBandAlbumRequestDto
    {
        [Required(ErrorMessage = "Band Id is a required field.")]
        [NotEqual("00000000-0000-0000-0000-000000000000")] // Custom Validation Attribute : Guid.Empty is not allowed
        public Guid BandId { get; set; }

        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string BandName { get; set; } = string.Empty;

        public List<AddAlbumRequestDto> Albums { get; set; } = new List<AddAlbumRequestDto>();
    }

    public class AddAlbumRequestDto
    {
        [Required(ErrorMessage = "Album title is a required field.")]
        [MaxLength(255, ErrorMessage = "Album title cannot exceed 255 characters.")]
        public string AlbumTitle { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Recording label cannot exceed 255 characters.")]
        public string? RecordingLabel { get; set; }

        [MaxLength(255, ErrorMessage = "Playback format cannot exceed 255 characters.")]
        public string? PlaybackFormat { get; set; }

        [MaxLength(255, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public Guid? GenreId { get; set; }
    }
}

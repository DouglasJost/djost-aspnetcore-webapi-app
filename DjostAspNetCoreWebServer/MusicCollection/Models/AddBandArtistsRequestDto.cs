using AppServiceCore.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.MusicCollection.Models
{
    public class AddBandArtistsRequestDto
    {
        [Required(ErrorMessage = "Band Id is a required field.")]
        [NotEqual("00000000-0000-0000-0000-000000000000")] // Custom Validation Attribute : Guid.Empty is not allowed
        public Guid BandId { get; set; }

        [MaxLength(255, ErrorMessage  = "Band name cannot exceed 255 characters.")]
        public string BandName { get; set; } = string.Empty;

        public List<AddArtistRequestDto> Artists { get; set; } = new List<AddArtistRequestDto>();
    }

    public class AddArtistRequestDto
    {
        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(255, ErrorMessage = "First name cannot exceed 255 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is a required field.")]
        [MaxLength(255, ErrorMessage = "Last name cannot exceed 255 characters.")]
        public string LastName { get; set; } = string.Empty;
        public DateOnly? Birthdate { get; set; }

        public DateOnly? Deathdate { get; set; }

        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? City { get; set; }

        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; }

        [MaxLength(255, ErrorMessage = "Instrument cannot exceed 255 characters.")]
        public string? Instrument { get; set; }
    }
}

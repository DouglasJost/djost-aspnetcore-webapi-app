using System;
using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.MusicCollection.Models
{
    public class AddBandRequestDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        public DateOnly? FormationDate { get; set; }

        public DateOnly? DisbandDate { get; set; }

        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? City { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; } = string.Empty;
    }
}

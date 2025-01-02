using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.MusicCollection.Models
{
    public class GetBandsByBandNameRequestDto
    {
        [Required(ErrorMessage = "BandName is a required field.")]
        [MaxLength(255, ErrorMessage = "BandName cannot exceed 255 characters.")]
        public string BandName { get; set; } = string.Empty;
    }
}

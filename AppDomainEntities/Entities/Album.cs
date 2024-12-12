using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("Album")]
public partial class Album
{
    [Key]
    public Guid AlbumId { get; set; }

    public Guid? BandId { get; set; }

    public Guid? ArtistId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? RecordingLabel { get; set; }

    /// <summary>
    /// e.g., Vinyl, CD, Digital 
    /// </summary>
    [StringLength(255)]
    [Unicode(false)]
    public string? PlaybackFormat { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Country { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public Guid? GenreId { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("Albums")]
    public virtual Artist? Artist { get; set; }

    [ForeignKey("BandId")]
    [InverseProperty("Albums")]
    public virtual Band? Band { get; set; }

    [ForeignKey("GenreId")]
    [InverseProperty("Albums")]
    public virtual Genre? Genre { get; set; }

    [InverseProperty("Album")]
    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}

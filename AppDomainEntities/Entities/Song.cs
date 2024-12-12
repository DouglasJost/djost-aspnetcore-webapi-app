using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("Song")]
public partial class Song
{
    [Key]
    public Guid SongId { get; set; }

    public Guid AlbumId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Order of the song in the album
    /// </summary>
    [StringLength(25)]
    [Unicode(false)]
    public string TrackNumber { get; set; } = null!;

    public TimeOnly? Duration { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Credits { get; set; }

    [ForeignKey("AlbumId")]
    [InverseProperty("Songs")]
    public virtual Album Album { get; set; } = null!;

    [InverseProperty("Song")]
    public virtual ICollection<SongWriter> SongWriters { get; set; } = new List<SongWriter>();
}

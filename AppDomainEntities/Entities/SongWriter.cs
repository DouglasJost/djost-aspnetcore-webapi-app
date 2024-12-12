using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("SongWriter")]
public partial class SongWriter
{
    [Key]
    public Guid SongWriterId { get; set; }

    public Guid SongId { get; set; }

    public Guid ArtistId { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("SongWriters")]
    public virtual Artist Artist { get; set; } = null!;

    [ForeignKey("SongId")]
    [InverseProperty("SongWriters")]
    public virtual Song Song { get; set; } = null!;
}

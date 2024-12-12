using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("Artist")]
public partial class Artist
{
    [Key]
    public Guid ArtistId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public DateOnly? Birthdate { get; set; }

    public DateOnly? Deathdate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [InverseProperty("Artist")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    [InverseProperty("Artist")]
    public virtual ICollection<BandMembership> BandMemberships { get; set; } = new List<BandMembership>();

    [InverseProperty("Artist")]
    public virtual ICollection<SongWriter> SongWriters { get; set; } = new List<SongWriter>();
}

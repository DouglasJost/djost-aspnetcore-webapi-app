using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("Band")]
public partial class Band
{
    [Key]
    public Guid BandId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public DateOnly? FormationDate { get; set; }

    public DateOnly? DisbandDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Country { get; set; }

    [InverseProperty("Band")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    [InverseProperty("Band")]
    public virtual ICollection<BandMembership> BandMemberships { get; set; } = new List<BandMembership>();
}

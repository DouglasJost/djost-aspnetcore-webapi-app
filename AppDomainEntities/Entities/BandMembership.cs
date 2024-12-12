using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("BandMembership")]
public partial class BandMembership
{
    [Key]
    public Guid BandMembershipId { get; set; }

    public Guid BandId { get; set; }

    public Guid ArtistId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("BandMemberships")]
    public virtual Artist Artist { get; set; } = null!;

    [ForeignKey("BandId")]
    [InverseProperty("BandMemberships")]
    public virtual Band Band { get; set; } = null!;
}

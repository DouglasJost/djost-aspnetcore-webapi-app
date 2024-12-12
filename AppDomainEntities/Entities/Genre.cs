using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("Genre")]
[Index("Name", Name = "IX_Name_Unique", IsUnique = true)]
public partial class Genre
{
    [Key]
    public Guid GenreId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Genre")]
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}

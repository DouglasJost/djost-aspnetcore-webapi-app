using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("UserAccount")]
public partial class UserAccount
{
    [Key]
    public Guid UserAccountId { get; set; }

    public bool Inactive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public bool UserDefined { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastModifiedDate { get; set; }

    [InverseProperty("UserAccount")]
    public virtual UserLogin? UserLogin { get; set; }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities.Entities;

[Table("UserLogin")]
[Index("Login", Name = "FK_UserLogin")]
public partial class UserLogin
{
    [Key]
    public Guid UserAccountId { get; set; }

    public bool Inactive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Login { get; set; } = null!;

    [Column(TypeName = "varchar(max)")]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    public bool UserDefined { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastModifiedDate { get; set; }

    [ForeignKey("UserAccountId")]
    [InverseProperty("UserLogin")]
    public virtual UserAccount UserAccount { get; set; } = null!;
}

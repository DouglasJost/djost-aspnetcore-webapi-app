using System;
using System.Collections.Generic;
using AppDomainEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities;

public partial class MusicCollectionDbContext : DbContext
{
    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }


    public MusicCollectionDbContext()
    {
    }

    public MusicCollectionDbContext(DbContextOptions<MusicCollectionDbContext> options)
        : base(options)
    {
    }

    //
    // *** Reference Program.cs for DbContext configuration ***
    //

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(DB_CONNECTION_STRING);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK_User");

            entity.Property(e => e.UserAccountId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.Property(e => e.UserAccountId).ValueGeneratedNever();
            entity.Property(e => e.LastModifiedDate).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.UserAccount).WithOne(p => p.UserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLogin_User");
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<UserAccount>().HasData(
            new UserAccount
            {
                UserAccountId = Guid.Parse("4EC76740-6895-40F4-ABB8-3FBAB440FFF1"),
                Inactive = false,
                FirstName = "JWT",
                LastName = "Issuer",
                UserDefined = true,
                LastModifiedDate = DateTime.UtcNow
            });

        modelBuilder.Entity<UserLogin>().HasData(
            new UserLogin
            {
                UserAccountId = Guid.Parse("4EC76740-6895-40F4-ABB8-3FBAB440FFF1"),
                Inactive = false,
                Login = "JwtIssuer",
                Password = "DHRDlZikUzbgrZDMbnw0L4CCiZJCvbMvIZGZUtBCoGna697qdCPnFZ53qHFxUKEzClrmoClhkuReEweYObes53sSENv4xZRJI9x+aS8xTD0=",
                UserDefined = true,
                LastModifiedDate = DateTime.UtcNow
            });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

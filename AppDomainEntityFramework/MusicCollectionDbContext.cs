using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using AppDomainEntities.Entities;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace AppDomainEntities;

public partial class MusicCollectionDbContext : DbContext
{
    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Band> Bands { get; set; }

    public virtual DbSet<BandMembership> BandMemberships { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongWriter> SongWriters { get; set; }



    public MusicCollectionDbContext()
    {
    }

    public MusicCollectionDbContext(DbContextOptions<MusicCollectionDbContext> options)
        : base(options)
    {
    }

    //
    // A design decision was made that ASP.NET Core Dependence Injection would not be used to manage the database context object.
    // This is why there is no "builder.Services.AddDbContext()" statement in Program.cs in the start up project.
    //
    // Instead, it is the responsibility of the "parent service" to wrap an instance of DbContext in a Using() statement and pass
    // that DbContext to the respective "services" and "repositories" that are part of the respective workflow.  This also means
    // it is the responsibility of the "parent service" to call DbContext.SaveChanges(), if an insert/update/delete is performed
    // by the workflow.
    //
    // Here is an example of how the "parent service" should manage the DbContext for a given workflow.
    // ================================================================================================
    //   using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
    //   {
    //       string jwtSecurityToken = await _tokenService.CreateJwtSecurityTokenAsync(dbContext, request.Login, request.Password);
    //       var response = new SecurityTokenResponseDto
    //       {
    //           JwtSecurityToken = jwtSecurityToken,
    //       };
    //
    //       // For demonstrations purposes, SaveChanges() is being called to show where it typically would be called.
    //       dbContext.SaveChanges();
    //     
    //       return CommandResult<SecurityTokenResponseDto>.Success(response);
    //   }
    //
    // Because it is the responsibility of the "parent service" to manage the DbContext for a workflow, all
    // (MyClassName : IMyClassName) classes are registered as Transient by ServiceCollectionExtensions.AddServicesWithDefaultConventions().
    //

    public static DbContextOptions<MusicCollectionDbContext> GetDbContextOptions()
    {
        var dbConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONNECTION_STRING");
        if (dbConnectionString == null)
        {
            throw new ArgumentNullException(nameof(dbConnectionString));
        }

        var optionsBuilder = new DbContextOptionsBuilder<MusicCollectionDbContext>();
        optionsBuilder.UseSqlServer(dbConnectionString);
        return optionsBuilder.Options;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //
        // DO NOT call "optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONNECTION_STRING"));"
        //
        // See explaination above.
        //


        //
        // Retrieve and validate environment varialbes
        //
        //   To create an environment variable from PowerShell prompt:
        //     PS>set OPENAI_API_TOKEN=  
        //     PS>set OPENAI_API_URL=
        //      
        //   To Display an enivronment varialbe from PowerShell prompt:
        //     PS>Get-ChildItem Env:
        //
        //   Or, System Properties > Advanced Tab > Environment Variables 
        //
        //   Or, use Azure KeyVault
        //

        var dbConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONNECTION_STRING");
        if (dbConnectionString == null)
        {
            throw new ArgumentNullException(nameof(dbConnectionString));
        }
        optionsBuilder.UseSqlServer(dbConnectionString);
    }

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

        modelBuilder.Entity<Album>(entity =>
        {
            entity.Property(e => e.AlbumId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.PlaybackFormat).HasComment("e.g., Vinyl, CD, Digital ");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums).HasConstraintName("FK_Album_Artist");

            entity.HasOne(d => d.Band).WithMany(p => p.Albums).HasConstraintName("FK_Album_Band");

            entity.HasOne(d => d.Genre).WithMany(p => p.Albums).HasConstraintName("FK_Album_Genre");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.Property(e => e.ArtistId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Band>(entity =>
        {
            entity.Property(e => e.BandId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<BandMembership>(entity =>
        {
            entity.Property(e => e.BandMembershipId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Artist).WithMany(p => p.BandMemberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BandMembership_Artist");

            entity.HasOne(d => d.Band).WithMany(p => p.BandMemberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BandMembership_Band");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.GenreId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("Unique_AlbumId_TrackNumber");

            entity.Property(e => e.SongId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TrackNumber).HasComment("Order of the song in the album");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Song_Album");
        });

        modelBuilder.Entity<SongWriter>(entity =>
        {
            entity.Property(e => e.SongWriterId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Artist).WithMany(p => p.SongWriters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongWriter_Artist");

            entity.HasOne(d => d.Song).WithMany(p => p.SongWriters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongWriter_Song");
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
                Password = "mVwmDVr8OwTwnbVwDvi40w==.DWy8ko+AwMzcA/yu2uGVVCiMM2dGdXkWmkn0FGZvkxk=",
                UserDefined = true,
                LastModifiedDate = DateTime.UtcNow
            });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

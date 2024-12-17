using System;
using AppDomainEntities.Entities;
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

    /*
        A design decision was made not to use the ASP.NET Core Dependence Injection DI to manage the DbContext.
    
        Instead, it is the responsibility of the "parent service" to create a DbContext and wrap it in a Using() statement
        and pass that DbContext to the respective services and repositories that are part of the workflow being implemented.
    
        In this way, multiple units of work can be performed within the scope of single HTTP request (scope).  
    
        This also means it is the responsibility of the "parent service" to call _dbContext.SaveChangesAsync(), if any inserts,
        updates and/or deletes are peformed by the workflow.

        Please note, because the "parent service" is managing the DbContext, all (MyClassName: IMyClassName) classes are registered
        as Transient by ServiceCollectionExtensions.AddServicesWithDefaultConventions().
    

        In implementing this work flow, the following logic is included in Program.cs to register a factory for creation
        of DbContext instances.

            var dbConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(dbConnectionString))
            {
                throw new InvalidOperationException("Db Connection String is not defined.");
            }
            builder.Services.AddDbContextFactory<MusicCollectionDbContext>(options => options.UseSqlServer(dbConnectionString));


        And, the following logic is included in the "parent service" class, which is where the DbContext is created and managed.

            public class MusicCollectionService : IMusicCollectionService
            {
                private readonly IMusicCollectionRepository _musicCollectionRepository;
                private readonly IDbContextFactory<MusicCollectionDbContext> _dbContextFactory;

                public MusicCollectionService(
                    IDbContextFactory<MusicCollectionDbContext> dbContextFactory,
                    IMusicCollectionRepository musicCollectionRepository)
                {
                    _dbContextFactory = dbContextFactory;
                    _musicCollectionRepository = musicCollectionRepository;
                }

                public async Task<CommandResult<IEnumerable<MusicCollectionBandDto>>> GetBandsByBandNameAsync(GetBandsByBandNameRequestDto requestDto)
                {
                    try 
                    {
                        if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.BandName))
                        {
                            return CommandResult<IEnumerable<MusicCollectionBandDto>>.Failure("Band name cannot be null.");
                        }

                        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                        {
                            var response = await _musicCollectionRepository.GetBandsByBandNameAsync(dbContext, requestDto.BandName);

                            // For demo purposes.  Parent is responsible for calling SaveChanges().
                            await dbContext.SaveChangesAsync();

                            return CommandResult<IEnumerable<MusicCollectionBandDto>>.Success(response);
                        }
                    }
                    ...
                }
                ...
            }
    */

    //public static DbContextOptions<MusicCollectionDbContext> GetDbContextOptions()
    //{
    //    var dbConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONNECTION_STRING");
    //    if (dbConnectionString == null)
    //    {
    //        throw new ArgumentNullException(nameof(dbConnectionString));
    //    }
    //
    //    var optionsBuilder = new DbContextOptionsBuilder<MusicCollectionDbContext>();
    //    optionsBuilder.UseSqlServer(dbConnectionString);
    //    return optionsBuilder.Options;
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
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

        if (optionsBuilder.IsConfigured)
        {
            // DbContextOptionsBuilder (optionsBuilder) is already configured.
            // Most likely this was done by a "Parent Service" who is responsible for managing the DbContext.
            // See description above for an example on how this can be done.

            // For debug purposes, EnableSensitiveDataLogging() can be turned by uncommenting the next statement.
            //optionsBuilder.EnableSensitiveDataLogging();

            return;
        }

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

        //modelBuilder.Entity<UserAccount>().HasData(
        //    new UserAccount
        //    {
        //        UserAccountId = Guid.Parse("4EC76740-6895-40F4-ABB8-3FBAB440FFF1"),
        //        Inactive = false,
        //        FirstName = "JWT",
        //        LastName = "Issuer",
        //        UserDefined = true,
        //        LastModifiedDate = DateTime.UtcNow
        //    });
        //
        //modelBuilder.Entity<UserLogin>().HasData(
        //    new UserLogin
        //    {
        //        UserAccountId = Guid.Parse("4EC76740-6895-40F4-ABB8-3FBAB440FFF1"),
        //        Inactive = false,
        //        Login = "JwtIssuer",
        //        Password = "mVwmDVr8OwTwnbVwDvi40w==.DWy8ko+AwMzcA/yu2uGVVCiMM2dGdXkWmkn0FGZvkxk=",
        //        UserDefined = true,
        //        LastModifiedDate = DateTime.UtcNow
        //    });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

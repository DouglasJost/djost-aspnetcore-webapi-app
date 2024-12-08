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


        //var dbConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONNECTION_STRING");
        //if (dbConnectionString == null)
        //{
        //    throw new ArgumentNullException(nameof(dbConnectionString));
        //}
        //optionsBuilder.UseSqlServer(dbConnectionString);
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

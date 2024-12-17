The ASP.NET Core WebAPI application that I wrote includes the following:

* Dynamic dependency injection registration.  Dynamic registration of interface and implementation pairs that follow the naming convention (MyClass : IMyClass) with the ASP.NET Core dependency injection container.

  The extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions(this IServiceCollection services) was added to the startup project and is called by the static main function in Program.cs.  This extension method dynamically "scans" the loaded assemblies, excluding System and Microsoft assemblies for the ServiceRegistration.GetServices() method that each application library implements.  GetServices() returns a list of interfaceType/implementationType pairs that match the default naming convention (MyClass : IMyClass) that the class library wants registered with the ASP.NET Core dependency injection (DI) container.  Each pair is then registered as Transient - create new instance every time requested.

  Please reference the extension method ServiceCollectionExtensions.AddServicesWithDefaultConventions(this IServiceCollection services) and comments contain in the method for more details and assumptions that were made in implementing this logic.

* Added support to ensure that enum values are serialized and deserialized as strings instead of their default numeric value when enum are passed to and returned from WebAPI endpoints.  This logic is implemented in the program.cs file.

* Added class library that supports OpenAI Chat Completions controller endpoints.  These endpoints are in the OpenAiChatCompletionsController.cs file.

* Added AppServiceCore library that includes the various interfaces and DTO models that are used by the application.  It also implements the CommandResult object that is returned by the respective endpoints to the caller.

* Added assessment library.

* Adding NUnit tests for the various class libraries that are included in this application.

* Added JWT Bearer Token authentication.  Reference LoginAuthenticationController.cs, AuthenticationService.cs and Program.cs. 

* Added Serilog general and custom loggers.  Reference Program.cs, appsettings.json, and AppSerilogLogger.cs.

* Added input validate by using System.ComponentModel.DataAnnotations.  Future enhancement is to use FluentValidation.

* Added NuGet package Asp.Versioning.Mvc to support versioning of controller end points via the route.

* Added Entity Framework Core and EF Core Power Tools.  

  All application domain entities referenced by Entity Framework Core are contained in the AppDomainEntities project.  Please note, Database First approach was used to initially create the entity classes.
    
  Entity Framework Core logic is contained in the AppDomainEntityFramework project.  This is where the migrations and the DbContext.cs file are located.  A README file is included in the project root, which has the PowerShell (Package Manager) and CLI commands to scaffold the DbContext for db first approach.  Migration commands that have been run are also included in the README file.  

  With respect to Entity Framework Core and ASP.NET Core DI, a design decision was made not to have DI manage the DbContext.  Instead it is the responsibility of the Parent Service to manage the DbContext.  This is why the "MyClassName: IMyClassName" classes are registered as Transient and "builder.Services.AddDbContext()" statement is not included in Program.cs.  Please reference MusicCollectionDbContext.cs where this reasoning is explained and for an example of how the DbContext should be managed.

* Added Password hashing logic.  Rfc2898DeriveBytes.Pbkdf2() is used to hash a given password. 
  
  Reference AppServiceCore.Util.PasswordHasher HashPassword(string password) and VerifyPassword(string password, string hashedPassword).

  Reference AppDomainEntities.MusicCollectionDbContext.OnModelCreating(ModelBuilder modelBuilder) for system Login and hashed (with salt) Password.

  Reference DjostAspNetCoreWebServer.Controllers Authenticate() and HashPassword() for workflow examples.

* Added initial MusicCollection entity classes (Album, Artist, Band, BandMembership, Genre, Song, SongWriter) and created migration.  

* Added SQL database insert scripts for initial MusicCollection system and seed data tables.

  Reference AppDomainEntityFramework\SQL\01_InsertSystemData.sql and 02_InsertSeedData.sql.

* Created the following MusicCollection database stored procedures and respective migrations.

  Db Proc: GetBandsByBandName,   Migration: AddGetBandsByBandNameStoredProcedure

  Db Proc: GetArtistsByBandId,   Migration: AddGetArtistsByBandIdStoredProcedure

  Db Proc: GetAlbumsByBandId,    Migration: AddGetAlbumsByBandIdStoredProcedure
 
  Db Proc: GetSongsByAlbumId,    Migration: AddGetSongsByAlbumIdStoredProcedure

* Added MusicCollection controller, service and repository classes and endpoints to retrieve "Band by Band Name", "Band Membership by BandId", "Albums by BandId", and "Song List by AlbumId".

* Refactored how DbContext is created.  Added builder.Services.AddDbContextFactory<T> to Program.cs.  And, parent service now calls "await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())" to get a DbContext.  See comments in MusicCollectionDbContext.cs for more information on how a parent service should create and manage it's DbContext.

Database First Approach in creating the entity class
====================================================

-- Package Manager Console - PowerShell Command
-- ============================================
Scaffold-DbContext "Server=DOUG-JOST;Database=MusicCollectionDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project AppDomainEntities -ContextDir . -DataAnnotations -Force



-- CLI Command
-- ===========
dotnet ef dbcontext scaffold "Server=DOUG-JOST;Database=MusicCollectionDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --project AppDomainEntities --context-dir . --data-annotations --force


=================================
=================================

Migrations Commands
===================

CLI (command prompt) - initial is the name of the migration
===========================================================
dotnet ef migrations add InitialMigration
dotnet ef migrations add SeedUserAccountAndUserLogin


PMC (Package Manager Console from Visual Studio - executes PowerShell commands)
===============================================================================
PM> add-migration InitialMigration
PM> add-migration SeedUserAccountAndUserLogin
PM> add-migration UpdatePasswordLength
PM> add-migration AddedInitialMusicCollectionEntityClasses

PM> add-migration AddGetBandsByBandNameStoredProcedure
PM> add-migration AddGetAlbumsByBandIdStoredProcedure
PM> add-migration AddGetArtistsByBandIdStoredProcedure
PM> add-migration AddGetSongsByAlbumIdStoredProcedure


=================================
=================================

Applying Migrations
===================

CLI (command prompt)
====================
dotnet ef database update
dotnet ef migrations script


PMC (Package Manager Console from Visual Studio - executes PowerShell commands)
===============================================================================
PM> Update-Database -verbose

PM> Script-Migration -idempotent   NOTE: Creates SQL Script; Scripts all migrations but checks for each object first (table already exists)
                                   NOTE: -From starting migration.  Defaults to '0' (the initial database).
                                               specifies last migration run, so start at the next one.

                                         -To   target migration.  Defaults to the last migration.
                                               final one to apply.


=================================
=================================

Scaffolding Limitations
=======================
Updating Model/Entities when database changes is not currently supported.  
In other words, after updating the db schema, currently there is no way to then update the Entities classes.  "database first".

One has to update the class object model, then add any DbSet parameters to the DbContext.cs file (MusicCollectionDbContext.cs),
then create a migration to update the database schema.

Would rather update the database schema, then run a "migration" that updates the DbContext.cs file and Entities classes (AppDomainEntities).

Have to 
1) update entities classes and or DbSet params in the DbContext.cs file.
2) create a migration :: "PM> add-migration <migration-name>".
3) then, run "PM> Update-Database" to create/update the database schema.


Transition to migrations is not pretty.  Look for helpful link in resources


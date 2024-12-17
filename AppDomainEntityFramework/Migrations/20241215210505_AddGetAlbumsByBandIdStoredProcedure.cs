using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddGetAlbumsByBandIdStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetAlbumsByBandId] @bandId UNIQUEIDENTIFIER
                AS
                    SELECT 
                        b.BandId, b.Name AS BandName,  
                        a.AlbumId, a.Title AS AlbumTitle, g.Name AS GenreName, 
                        a.RecordingLabel, a.ReleaseDate, a.PlaybackFormat, a.Country

                    FROM Band b LEFT JOIN Album a on b.BandId = a.BandId
                    LEFT JOIN Genre g on a.GenreId = g.GenreId

                    WHERE b.BandId = @bandId
                    
                    ORDER BY a.ReleaseDate, a.Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE dbo.GetAlbumsByBandId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddGetArtistsByBandIdStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetArtistsByBandId] @bandId UNIQUEIDENTIFIER
                AS
                  SELECT b.BandId, b.Name AS BandName,
                         a.ArtistId, a.FirstName AS ArtistFirstName, a.LastName AS ArtistLastName,
	                     a.Birthdate, a.Deathdate, a.Instrument, a.City, a.Country

                  FROM Band b LEFT JOIN BandMembership bm on b.BandId = bm.BandId
                              LEFT JOIN Artist a on bm.ArtistId = a.ArtistId

                  WHERE b.BandId = @bandId

                  ORDER BY a.LastName, a.FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE dbo.GetArtistsByBandId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddGetBandByBandNameStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              CREATE PROCEDURE dbo.GetBandByBandName
                @bandName varchar(50)
              AS
                SELECT b.BandId, b.Name AS BandName, b.FormationDate, b.DisbandDate, b.City, b.Country
                FROM Band b
                where b.Name like '%' + @bandName + '%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE dbo.AuthorsPublishedinYearRange");
        }
    }
}

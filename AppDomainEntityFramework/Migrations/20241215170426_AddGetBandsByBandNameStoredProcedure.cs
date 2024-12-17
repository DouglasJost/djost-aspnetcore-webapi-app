using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddGetBandsByBandNameStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              CREATE PROCEDURE dbo.GetBandsByBandName
                @bandName varchar(50)
              AS
                SELECT b.BandId, b.Name AS BandName, b.FormationDate, b.DisbandDate, b.City, b.Country
                FROM Band b
                WHERE b.Name like '%' + @bandName + '%'
                ORDER BY b.Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE dbo.GetBandsByBandName");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddGetSongListByAlbumIdStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE [dbo].[GetSongListByAlbumId] @albumId UNIQUEIDENTIFIER
                AS
                  SELECT a.AlbumId, a.BandId, a.ArtistId, a.title AS AlbumTitle,
                         s.SongId, s.Title AS SongTitle, s.TrackNumber, s.Duration, s.Credits

                  FROM Album a LEFT JOIN Song s ON a.AlbumId = s.AlbumId

                  WHERE a.AlbumId = @albumId

                  ORDER BY s.TrackNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE dbo.GetSongListByAlbumId");
        }
    }
}

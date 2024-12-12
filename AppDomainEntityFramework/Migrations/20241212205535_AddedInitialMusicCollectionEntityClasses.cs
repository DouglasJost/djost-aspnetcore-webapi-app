using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitialMusicCollectionEntityClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FirstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Deathdate = table.Column<DateOnly>(type: "date", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Instrument = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Band",
                columns: table => new
                {
                    BandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FormationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DisbandDate = table.Column<DateOnly>(type: "date", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Band", x => x.BandId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "BandMembership",
                columns: table => new
                {
                    BandMembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    BandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandMembership", x => x.BandMembershipId);
                    table.ForeignKey(
                        name: "FK_BandMembership_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId");
                    table.ForeignKey(
                        name: "FK_BandMembership_Band",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "BandId");
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    BandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RecordingLabel = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PlaybackFormat = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, comment: "e.g., Vinyl, CD, Digital "),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumId);
                    table.ForeignKey(
                        name: "FK_Album_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId");
                    table.ForeignKey(
                        name: "FK_Album_Band",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "BandId");
                    table.ForeignKey(
                        name: "FK_Album_Genre",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId");
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    TrackNumber = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false, comment: "Order of the song in the album"),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: true),
                    Credits = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Unique_AlbumId_TrackNumber", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Song_Album",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "AlbumId");
                });

            migrationBuilder.CreateTable(
                name: "SongWriter",
                columns: table => new
                {
                    SongWriterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongWriter", x => x.SongWriterId);
                    table.ForeignKey(
                        name: "FK_SongWriter_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId");
                    table.ForeignKey(
                        name: "FK_SongWriter_Song",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "SongId");
                });

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 12, 20, 55, 35, 107, DateTimeKind.Utc).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 12, 20, 55, 35, 107, DateTimeKind.Utc).AddTicks(6874));

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_BandId",
                table: "Album",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_GenreId",
                table: "Album",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMembership_ArtistId",
                table: "BandMembership",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMembership_BandId",
                table: "BandMembership",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Name_Unique",
                table: "Genre",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumId",
                table: "Song",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_SongWriter_ArtistId",
                table: "SongWriter",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_SongWriter_SongId",
                table: "SongWriter",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BandMembership");

            migrationBuilder.DropTable(
                name: "SongWriter");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Band");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 10, 0, 10, 44, 921, DateTimeKind.Utc).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 10, 0, 10, 44, 921, DateTimeKind.Utc).AddTicks(7684));
        }
    }
}

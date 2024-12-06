using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Inactive = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserDefined = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserAccountId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false),
                    Login = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UserDefined = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.UserAccountId);
                    table.ForeignKey(
                        name: "FK_UserLogin_User",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId");
                });

            migrationBuilder.CreateIndex(
                name: "FK_UserLogin",
                table: "UserLogin",
                column: "Login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserAccountAndUserLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "UserAccount",
            //    columns: new[] { "UserAccountId", "FirstName", "Inactive", "LastModifiedDate", "LastName", "UserDefined" },
            //    values: new object[] { new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"), "JWT", false, new DateTime(2024, 11, 22, 20, 14, 28, 884, DateTimeKind.Utc).AddTicks(8663), "Issuer", true });

            //migrationBuilder.InsertData(
            //    table: "UserLogin",
            //    columns: new[] { "UserAccountId", "Inactive", "LastModifiedDate", "Login", "Password", "UserDefined" },
            //    values: new object[] { new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"), false, new DateTime(2024, 11, 22, 20, 14, 28, 884, DateTimeKind.Utc).AddTicks(8738), "JwtIssuer", "RidersOnTheStorm", true });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "UserLogin",
            //    keyColumn: "UserAccountId",
            //    keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"));

            //migrationBuilder.DeleteData(
            //    table: "UserAccount",
            //    keyColumn: "UserAccountId",
            //    keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"));
    }
  }
}

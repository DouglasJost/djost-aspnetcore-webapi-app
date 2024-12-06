using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 6, 5, 56, 9, 443, DateTimeKind.Utc).AddTicks(1401));

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                columns: new[] { "LastModifiedDate", "Password" },
                values: new object[] { new DateTime(2024, 12, 6, 5, 56, 9, 443, DateTimeKind.Utc).AddTicks(1475), "DHRDlZikUzbgrZDMbnw0L4CCiZJCvbMvIZGZUtBCoGna697qdCPnFZ53qHFxUKEzClrmoClhkuReEweYObes53sSENv4xZRJI9x+aS8xTD0=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 6, 5, 51, 8, 39, DateTimeKind.Utc).AddTicks(984));

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                columns: new[] { "LastModifiedDate", "Password" },
                values: new object[] { new DateTime(2024, 12, 6, 5, 51, 8, 39, DateTimeKind.Utc).AddTicks(1054), "RidersOnTheStorm" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordToVarcharMax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserLogin",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

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
                column: "LastModifiedDate",
                value: new DateTime(2024, 12, 6, 5, 51, 8, 39, DateTimeKind.Utc).AddTicks(1054));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserLogin",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 11, 22, 20, 14, 28, 884, DateTimeKind.Utc).AddTicks(8663));

            migrationBuilder.UpdateData(
                table: "UserLogin",
                keyColumn: "UserAccountId",
                keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
                column: "LastModifiedDate",
                value: new DateTime(2024, 11, 22, 20, 14, 28, 884, DateTimeKind.Utc).AddTicks(8738));
        }
    }
}

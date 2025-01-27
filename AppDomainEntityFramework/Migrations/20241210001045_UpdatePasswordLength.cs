using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDomainEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.UpdateData(
            //    table: "UserAccount",
            //    keyColumn: "UserAccountId",
            //    keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
            //    column: "LastModifiedDate",
            //    value: new DateTime(2024, 12, 10, 0, 10, 44, 921, DateTimeKind.Utc).AddTicks(7610));

            //migrationBuilder.UpdateData(
            //    table: "UserLogin",
            //    keyColumn: "UserAccountId",
            //    keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
            //    columns: new[] { "LastModifiedDate", "Password" },
            //    values: new object[] { new DateTime(2024, 12, 10, 0, 10, 44, 921, DateTimeKind.Utc).AddTicks(7684), "mVwmDVr8OwTwnbVwDvi40w==.DWy8ko+AwMzcA/yu2uGVVCiMM2dGdXkWmkn0FGZvkxk=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.UpdateData(
            //    table: "UserAccount",
            //    keyColumn: "UserAccountId",
            //    keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
            //    column: "LastModifiedDate",
            //    value: new DateTime(2024, 12, 6, 5, 56, 9, 443, DateTimeKind.Utc).AddTicks(1401));

            //migrationBuilder.UpdateData(
            //    table: "UserLogin",
            //    keyColumn: "UserAccountId",
            //    keyValue: new Guid("4ec76740-6895-40f4-abb8-3fbab440fff1"),
            //    columns: new[] { "LastModifiedDate", "Password" },
            //    values: new object[] { new DateTime(2024, 12, 6, 5, 56, 9, 443, DateTimeKind.Utc).AddTicks(1475), "DHRDlZikUzbgrZDMbnw0L4CCiZJCvbMvIZGZUtBCoGna697qdCPnFZ53qHFxUKEzClrmoClhkuReEweYObes53sSENv4xZRJI9x+aS8xTD0=" });
        }
    }
}

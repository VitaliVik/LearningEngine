using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEngine.Persistence.Migrations
{
    public partial class DB_AddPasswordFieldTypeByte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Users",
                maxLength: 64,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: new byte[] { 34, 22, 251, 167, 215, 101, 240, 213, 55, 98, 27, 159, 248, 119, 191, 223, 139, 53, 131, 5, 241, 224, 207, 83, 179, 206, 76, 178, 191, 94, 134, 102, 156, 172, 222, 210, 74, 107, 22, 106, 14, 27, 177, 136, 78, 197, 208, 136, 215, 90, 245, 106, 145, 219, 50, 9, 106, 255, 251, 66, 111, 244, 162, 161 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
        }
    }
}

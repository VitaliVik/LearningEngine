using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEngine.Persistence.Migrations
{
    public partial class ManyToManyInOneTable_Theme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Theme_ThemeId",
                table: "Theme");

            migrationBuilder.DropIndex(
                name: "IX_Theme_ThemeId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Theme");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Theme",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentThemeId",
                table: "Theme",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Theme_ParentThemeId",
                table: "Theme",
                column: "ParentThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Theme_ParentThemeId",
                table: "Theme",
                column: "ParentThemeId",
                principalTable: "Theme",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Theme_ParentThemeId",
                table: "Theme");

            migrationBuilder.DropIndex(
                name: "IX_Theme_ParentThemeId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "ParentThemeId",
                table: "Theme");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Theme",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Theme",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theme_ThemeId",
                table: "Theme",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Theme_ThemeId",
                table: "Theme",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

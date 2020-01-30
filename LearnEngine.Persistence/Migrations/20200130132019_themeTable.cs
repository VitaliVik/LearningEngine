using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEngine.Persistence.Migrations
{
    public partial class themeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Theme_ThemeId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Theme_ThemeId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Theme_ThemeId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Theme_ParentThemeId",
                table: "Theme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theme",
                table: "Theme");

            migrationBuilder.RenameTable(
                name: "Theme",
                newName: "Themes");

            migrationBuilder.RenameIndex(
                name: "IX_Theme_ParentThemeId",
                table: "Themes",
                newName: "IX_Themes_ParentThemeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Themes",
                table: "Themes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Themes_ThemeId",
                table: "Cards",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Themes_ThemeId",
                table: "Notes",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Themes_ThemeId",
                table: "Permissions",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Themes_ParentThemeId",
                table: "Themes",
                column: "ParentThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Themes_ThemeId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Themes_ThemeId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Themes_ThemeId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Themes_ParentThemeId",
                table: "Themes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Themes",
                table: "Themes");

            migrationBuilder.RenameTable(
                name: "Themes",
                newName: "Theme");

            migrationBuilder.RenameIndex(
                name: "IX_Themes_ParentThemeId",
                table: "Theme",
                newName: "IX_Theme_ParentThemeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theme",
                table: "Theme",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Theme_ThemeId",
                table: "Cards",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Theme_ThemeId",
                table: "Notes",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Theme_ThemeId",
                table: "Permissions",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Theme_ParentThemeId",
                table: "Theme",
                column: "ParentThemeId",
                principalTable: "Theme",
                principalColumn: "Id");
        }
    }
}

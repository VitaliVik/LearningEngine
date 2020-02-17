using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEngine.Persistence.Migrations
{
    public partial class AddThemeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "Description", "IsPublic", "Name", "ParentThemeId" },
                values: new object[] { 2, "all about .NET", true, "dotNet", null });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "Description", "IsPublic", "Name", "ParentThemeId" },
                values: new object[] { 3, "all about linq", true, "linq", 2 });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "ThemeId", "Title" },
                values: new object[] { 1, "deffered execution exist", 3, "deffered execution" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "ThemeId", "Title" },
                values: new object[] { 2, "GC - is garbage collector", 3, "GC " });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

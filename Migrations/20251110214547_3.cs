using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace saglik.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Slogan",
                table: "Slogan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_About",
                table: "About");

            migrationBuilder.RenameTable(
                name: "Slogan",
                newName: "Slogans");

            migrationBuilder.RenameTable(
                name: "About",
                newName: "Abouts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slogans",
                table: "Slogans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abouts",
                table: "Abouts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Slogans",
                table: "Slogans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abouts",
                table: "Abouts");

            migrationBuilder.RenameTable(
                name: "Slogans",
                newName: "Slogan");

            migrationBuilder.RenameTable(
                name: "Abouts",
                newName: "About");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slogan",
                table: "Slogan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_About",
                table: "About",
                column: "Id");
        }
    }
}

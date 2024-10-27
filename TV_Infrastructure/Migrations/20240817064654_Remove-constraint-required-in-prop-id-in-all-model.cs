using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TV_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removeconstraintrequiredinpropidinallmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowLanguages",
                table: "TVShowLanguages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowLanguages",
                table: "TVShowLanguages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShowLanguages",
                table: "TVShowLanguages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShowLanguages",
                table: "TVShowLanguages",
                column: "IsDeleted");
        }
    }
}
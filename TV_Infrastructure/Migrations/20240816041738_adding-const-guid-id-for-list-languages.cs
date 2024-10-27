using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TV_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingconstguididforlistlanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("8d23b7f9-58e5-400b-985c-05f6bde511af"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("d1314fc3-d0e8-47ee-a7bb-2cdccc15018a"));

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("79be201e-37d4-4c65-814b-e726d61ac82c"), false, "Arabic" },
                    { new Guid("a25aeca6-cfc9-43d1-96b9-e4f3530ef7fb"), false, "English" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("79be201e-37d4-4c65-814b-e726d61ac82c"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("a25aeca6-cfc9-43d1-96b9-e4f3530ef7fb"));

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("8d23b7f9-58e5-400b-985c-05f6bde511af"), false, "English" },
                    { new Guid("d1314fc3-d0e8-47ee-a7bb-2cdccc15018a"), false, "Arabic" }
                });
        }
    }
}
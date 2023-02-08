using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skyrim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedLandmarkLocationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LandMarks",
                table: "LandMarks");

            migrationBuilder.RenameTable(
                name: "LandMarks",
                newName: "Landmarks");

            migrationBuilder.RenameIndex(
                name: "IX_LandMarks_PatrollerId",
                table: "Landmarks",
                newName: "IX_Landmarks_PatrollerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Landmarks",
                table: "Landmarks",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Landmarks",
                table: "Landmarks");

            migrationBuilder.RenameTable(
                name: "Landmarks",
                newName: "LandMarks");

            migrationBuilder.RenameIndex(
                name: "IX_Landmarks_PatrollerId",
                table: "LandMarks",
                newName: "IX_LandMarks_PatrollerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LandMarks",
                table: "LandMarks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Patroller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PersonSequence]"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatrollerId = table.Column<int>(type: "int", nullable: true),
                    TypeOfPerson = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patroller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BuildingSequence]"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatrollerId = table.Column<int>(type: "int", nullable: true),
                    TypeOfBuilding = table.Column<int>(type: "int", nullable: false),
                    TypeOfShop = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patroller_LocationId",
                table: "Patroller",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patroller_PatrollerId",
                table: "Patroller",
                column: "PatrollerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_LocationId",
                table: "Shop",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_PatrollerId",
                table: "Shop",
                column: "PatrollerId");
        }
    }
}

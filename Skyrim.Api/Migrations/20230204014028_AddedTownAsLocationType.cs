using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skyrim.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedTownAsLocationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [LocationSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfLocation = table.Column<int>(type: "int", nullable: false),
                    GeographicalDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatrollerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Towns_PatrollerId",
                table: "Towns",
                column: "PatrollerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Towns");

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

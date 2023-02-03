using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skyrim.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "BuildingSequence");

            migrationBuilder.CreateSequence(
                name: "CreatureSequence");

            migrationBuilder.CreateSequence(
                name: "LocationSequence");

            migrationBuilder.CreateSequence(
                name: "PersonSequence");

            migrationBuilder.CreateTable(
                name: "Chickens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [CreatureSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfCreature = table.Column<int>(type: "int", nullable: false),
                    locationId = table.Column<int>(type: "int", nullable: false),
                    PatrollerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chickens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
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
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PersonSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfPerson = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    PatrollerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhyscialFightingShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BuildingSequence]"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfBuilding = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    PatrollerId = table.Column<int>(type: "int", nullable: true),
                    TypeOfShop = table.Column<int>(type: "int", nullable: false),
                    HasBlackSmithStation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhyscialFightingShops", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_locationId",
                table: "Chickens",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_PatrollerId",
                table: "Chickens",
                column: "PatrollerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_PatrollerId",
                table: "Cities",
                column: "PatrollerId");

            migrationBuilder.CreateIndex(
                name: "IX_Guards_LocationId",
                table: "Guards",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Guards_PatrollerId",
                table: "Guards",
                column: "PatrollerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhyscialFightingShops_LocationId",
                table: "PhyscialFightingShops",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PhyscialFightingShops_PatrollerId",
                table: "PhyscialFightingShops",
                column: "PatrollerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chickens");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Guards");

            migrationBuilder.DropTable(
                name: "PhyscialFightingShops");

            migrationBuilder.DropSequence(
                name: "BuildingSequence");

            migrationBuilder.DropSequence(
                name: "CreatureSequence");

            migrationBuilder.DropSequence(
                name: "LocationSequence");

            migrationBuilder.DropSequence(
                name: "PersonSequence");
        }
    }
}

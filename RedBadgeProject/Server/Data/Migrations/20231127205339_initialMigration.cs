using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RedBadgeProject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationFromId = table.Column<int>(type: "int", nullable: false),
                    LocationToId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Locations_LocationFromId",
                        column: x => x.LocationFromId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trips_Locations_LocationToId",
                        column: x => x.LocationToId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CurrentLocationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Staff_Locations_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StaffEntityTripEntity",
                columns: table => new
                {
                    StaffListId = table.Column<int>(type: "int", nullable: false),
                    TripListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffEntityTripEntity", x => new { x.StaffListId, x.TripListId });
                    table.ForeignKey(
                        name: "FK_StaffEntityTripEntity_Staff_StaffListId",
                        column: x => x.StaffListId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StaffEntityTripEntity_Trips_TripListId",
                        column: x => x.TripListId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Indianapolis" },
                    { 2, "Phoenix" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, 2, "BusCo" },
                    { 2, 1, "BoatCo" }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "Capacity", "EndDate", "LocationFromId", "LocationToId", "Name", "StartDate", "VehicleId" },
                values: new object[,]
                {
                    { 1, 26, new DateTimeOffset(new DateTime(2023, 11, 29, 20, 53, 38, 831, DateTimeKind.Unspecified).AddTicks(98), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, "Indy to Phoenix", new DateTimeOffset(new DateTime(2023, 11, 27, 20, 53, 38, 831, DateTimeKind.Unspecified).AddTicks(95), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 4, new DateTimeOffset(new DateTime(2023, 12, 11, 20, 53, 38, 831, DateTimeKind.Unspecified).AddTicks(116), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, "Phoenix to Indy", new DateTimeOffset(new DateTime(2023, 11, 27, 20, 53, 38, 831, DateTimeKind.Unspecified).AddTicks(115), new TimeSpan(0, 0, 0, 0, 0)), 2 }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "CompanyId", "CurrentLocationId", "Name", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 2, "Joe", 0 },
                    { 2, 2, 1, "Jane", 0 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Capacity", "CompanyId", "Name" },
                values: new object[,]
                {
                    { 1, 5, 2, "Boston Whaler" },
                    { 2, 30, 1, "International Schooler" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId",
                table: "Companies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_CompanyId",
                table: "Staff",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_CurrentLocationId",
                table: "Staff",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEntityTripEntity_TripListId",
                table: "StaffEntityTripEntity",
                column: "TripListId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationFromId",
                table: "Trips",
                column: "LocationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationToId",
                table: "Trips",
                column: "LocationToId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompanyId",
                table: "Vehicles",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffEntityTripEntity");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}

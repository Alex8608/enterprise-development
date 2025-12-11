using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirlineCompany.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreateWithSeedData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AircraftFamilies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AircraftFamilies", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Passengers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PassportNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Passengers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AircraftModels",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Range = table.Column<int>(type: "int", nullable: false),
                PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                CargoCapacity = table.Column<double>(type: "float", nullable: false),
                AircraftFamilyId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AircraftModels", x => x.Id);
                table.ForeignKey(
                    name: "FK_AircraftModels_AircraftFamilies_AircraftFamilyId",
                    column: x => x.AircraftFamilyId,
                    principalTable: "AircraftFamilies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Flights",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DepartureCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ArrivalCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                AircraftModelId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Flights", x => x.Id);
                table.ForeignKey(
                    name: "FK_Flights_AircraftModels_AircraftModelId",
                    column: x => x.AircraftModelId,
                    principalTable: "AircraftModels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Tickets",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SeatNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                HasHandLuggage = table.Column<bool>(type: "bit", nullable: false),
                BaggageWeight = table.Column<double>(type: "float", nullable: false),
                FlightId = table.Column<int>(type: "int", nullable: false),
                PassengerId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tickets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tickets_Flights_FlightId",
                    column: x => x.FlightId,
                    principalTable: "Flights",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Tickets_Passengers_PassengerId",
                    column: x => x.PassengerId,
                    principalTable: "Passengers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "AircraftFamilies",
            columns: new[] { "Id", "Manufacturer", "Name" },
            values: new object[,]
            {
                { 1, "Airbus", "A320" },
                { 2, "Boeing", "737" },
                { 3, "Airbus", "A330" },
                { 4, "Boeing", "777" },
                { 5, "Airbus", "A350" },
                { 6, "Boeing", "787" },
                { 7, "Bombardier", "CRJ" },
                { 8, "Embraer", "E-Jet" },
                { 9, "Airbus", "A220" },
                { 10, "Boeing", "747" }
            });

        migrationBuilder.InsertData(
            table: "Passengers",
            columns: new[] { "Id", "DateOfBirth", "FullName", "PassportNumber" },
            values: new object[,]
            {
                { 1, new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivanov Ivan Ivanovich", "3600-123456" },
                { 2, new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petrov Petr Petrovich", "3605-654321" },
                { 3, new DateTime(1985, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sidorova Anna Sergeevna", "3610-987654" },
                { 4, new DateTime(1978, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuznetsov Alexey Vladimirovich", "3615-456789" },
                { 5, new DateTime(1992, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smirnova Elena Dmitrievna", "3620-135790" },
                { 6, new DateTime(1988, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Popov Mikhail Igorevich", "3624-246801" },
                { 7, new DateTime(1983, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Volkova Olga Nikolaevna", "3602-112233" },
                { 8, new DateTime(1995, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Novikov Dmitry Andreevich", "3608-445566" },
                { 9, new DateTime(1987, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fedorova Maria Pavlovna", "3612-778899" },
                { 10, new DateTime(1975, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Orlov Sergey Viktorovich", "3618-990011" }
            });

        migrationBuilder.InsertData(
            table: "AircraftModels",
            columns: new[] { "Id", "AircraftFamilyId", "CargoCapacity", "Name", "PassengerCapacity", "Range" },
            values: new object[,]
            {
                { 1, 1, 4.5, "A320-200", 180, 6100 },
                { 2, 1, 5.2000000000000002, "A321neo", 240, 7400 },
                { 3, 2, 4.7999999999999998, "737-800", 189, 5765 },
                { 4, 2, 5.0999999999999996, "737 MAX 8", 210, 6570 },
                { 5, 3, 12.5, "A330-300", 440, 10800 },
                { 6, 4, 16.5, "777-300ER", 550, 13650 },
                { 7, 5, 14.5, "A350-900", 440, 15000 },
                { 8, 6, 13.5, "787-9", 420, 14140 },
                { 9, 7, 2.5, "CRJ-900", 90, 2870 },
                { 10, 8, 3.7999999999999998, "E195-E2", 146, 4815 }
            });

        migrationBuilder.InsertData(
            table: "Flights",
            columns: new[] { "Id", "AircraftModelId", "ArrivalCity", "ArrivalDate", "Code", "DepartureCity", "DepartureDate", "Duration" },
            values: new object[,]
            {
                { 1, 1, "London", new DateTime(2025, 12, 6, 18, 12, 40, 95, DateTimeKind.Local).AddTicks(779), "SU100", "Moscow", new DateTime(2025, 12, 6, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(768), new TimeSpan(0, 4, 0, 0, 0) },
                { 2, 2, "Paris", new DateTime(2025, 12, 8, 17, 42, 40, 95, DateTimeKind.Local).AddTicks(785), "SU200", "Moscow", new DateTime(2025, 12, 8, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(785), new TimeSpan(0, 3, 30, 0, 0) },
                { 3, 3, "New York", new DateTime(2025, 12, 9, 22, 12, 40, 95, DateTimeKind.Local).AddTicks(788), "SU300", "London", new DateTime(2025, 12, 9, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(787), new TimeSpan(0, 8, 0, 0, 0) },
                { 4, 4, "Tokyo", new DateTime(2025, 12, 11, 2, 12, 40, 95, DateTimeKind.Local).AddTicks(790), "SU400", "Paris", new DateTime(2025, 12, 10, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(789), new TimeSpan(0, 12, 0, 0, 0) },
                { 5, 5, "Dubai", new DateTime(2025, 12, 11, 20, 12, 40, 95, DateTimeKind.Local).AddTicks(792), "SU500", "Berlin", new DateTime(2025, 12, 11, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(792), new TimeSpan(0, 6, 0, 0, 0) },
                { 6, 6, "Singapore", new DateTime(2025, 12, 12, 21, 12, 40, 95, DateTimeKind.Local).AddTicks(794), "SU600", "Dubai", new DateTime(2025, 12, 12, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(794), new TimeSpan(0, 7, 0, 0, 0) },
                { 7, 7, "Sydney", new DateTime(2025, 12, 13, 22, 12, 40, 95, DateTimeKind.Local).AddTicks(796), "SU700", "Singapore", new DateTime(2025, 12, 13, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(796), new TimeSpan(0, 8, 0, 0, 0) },
                { 8, 8, "Los Angeles", new DateTime(2025, 12, 15, 4, 12, 40, 95, DateTimeKind.Local).AddTicks(799), "SU800", "Sydney", new DateTime(2025, 12, 14, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(798), new TimeSpan(0, 14, 0, 0, 0) },
                { 9, 9, "Tokyo", new DateTime(2025, 12, 16, 1, 12, 40, 95, DateTimeKind.Local).AddTicks(801), "SU900", "Los Angeles", new DateTime(2025, 12, 15, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(800), new TimeSpan(0, 11, 0, 0, 0) },
                { 10, 10, "Moscow", new DateTime(2025, 12, 17, 0, 12, 40, 95, DateTimeKind.Local).AddTicks(803), "SU1000", "Tokyo", new DateTime(2025, 12, 16, 14, 12, 40, 95, DateTimeKind.Local).AddTicks(802), new TimeSpan(0, 10, 0, 0, 0) }
            });

        migrationBuilder.InsertData(
            table: "Tickets",
            columns: new[] { "Id", "BaggageWeight", "FlightId", "HasHandLuggage", "PassengerId", "SeatNumber" },
            values: new object[,]
            {
                { 1, 15.5, 1, true, 1, "10A" },
                { 2, 0.0, 1, false, 2, "10B" },
                { 3, 10.0, 1, true, 3, "10C" },
                { 4, 12.0, 2, true, 4, "15A" },
                { 5, 8.5, 2, true, 5, "15B" },
                { 6, 0.0, 2, false, 6, "15C" },
                { 7, 20.0, 3, true, 7, "20A" },
                { 8, 5.5, 3, true, 8, "20B" },
                { 9, 0.0, 4, false, 9, "25A" },
                { 10, 18.0, 4, true, 10, "25B" },
                { 11, 7.5, 4, true, 1, "25C" },
                { 12, 0.0, 4, false, 2, "25D" },
                { 13, 9.0, 5, true, 3, "30A" },
                { 14, 11.0, 6, true, 4, "35A" },
                { 15, 0.0, 6, false, 5, "35B" },
                { 16, 6.5, 6, true, 6, "35C" },
                { 17, 14.0, 7, true, 7, "40A" },
                { 18, 0.0, 7, false, 8, "40B" },
                { 19, 16.5, 8, true, 9, "45A" },
                { 20, 13.0, 9, true, 10, "50A" },
                { 21, 0.0, 9, false, 1, "50B" },
                { 22, 8.0, 10, true, 2, "55A" },
                { 23, 19.5, 10, true, 3, "55B" },
                { 24, 0.0, 10, false, 4, "55C" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_AircraftModels_AircraftFamilyId",
            table: "AircraftModels",
            column: "AircraftFamilyId");

        migrationBuilder.CreateIndex(
            name: "IX_Flights_AircraftModelId",
            table: "Flights",
            column: "AircraftModelId");

        migrationBuilder.CreateIndex(
            name: "IX_Passengers_PassportNumber",
            table: "Passengers",
            column: "PassportNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_FlightId_SeatNumber",
            table: "Tickets",
            columns: new[] { "FlightId", "SeatNumber" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_PassengerId",
            table: "Tickets",
            column: "PassengerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Tickets");

        migrationBuilder.DropTable(
            name: "Flights");

        migrationBuilder.DropTable(
            name: "Passengers");

        migrationBuilder.DropTable(
            name: "AircraftModels");

        migrationBuilder.DropTable(
            name: "AircraftFamilies");
    }
}

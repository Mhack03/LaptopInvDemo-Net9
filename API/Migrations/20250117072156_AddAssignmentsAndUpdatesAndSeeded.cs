using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentsAndUpdatesAndSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaptopSerialNumber = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Laptops_LaptopSerialNumber",
                        column: x => x.LaptopSerialNumber,
                        principalTable: "Laptops",
                        principalColumn: "SerialNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "AssignedDate", "EmployeeId", "LaptopSerialNumber", "ReturnDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, "TZJHkvhCOe", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1002, "AQ5RrvbFR1", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1003, "jdEluAbfxz", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1004, "fI5D0yyqxc", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1005, "Drjg7rPPIb", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_EmployeeId",
                table: "Assignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LaptopSerialNumber",
                table: "Assignments",
                column: "LaptopSerialNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");
        }
    }
}

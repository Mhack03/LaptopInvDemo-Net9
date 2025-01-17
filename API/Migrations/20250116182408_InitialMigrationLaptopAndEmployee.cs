using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationLaptopAndEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    LaptopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Brand = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ram = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Storage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperatingSystem = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HasBag = table.Column<bool>(type: "bit", nullable: false),
                    HasKeyboard = table.Column<bool>(type: "bit", nullable: false),
                    HasMouse = table.Column<bool>(type: "bit", nullable: false),
                    HasCharger = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.LaptopId);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "CreatedDate", "Department", "Email", "Name", "Position", "UpdatedDate" },
                values: new object[,]
                {
                    { 1001, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "john.doe@example.com", "John Doe", "Manager", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1002, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "jane.smith@example.com", "Jane Smith", "Developer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1003, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "alice.johnson@example.com", "Alice Johnson", "Designer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1004, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "bob.williams@example.com", "Bob Williams", "Developer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1005, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "emily.brown@example.com", "Emily Brown", "Designer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Laptops",
                columns: new[] { "LaptopId", "Brand", "Description", "HasBag", "HasCharger", "HasKeyboard", "HasMouse", "Model", "OperatingSystem", "Processor", "Ram", "Status", "Storage" },
                values: new object[,]
                {
                    { 1001, 0, "Lightweight and portable laptop for everyday use.", true, true, false, false, "Swift 3", 1, "Intel Core i5", "8GB", 0, "256GB SSD" },
                    { 1002, 2, "Stylish and powerful laptop for professionals.", true, true, false, false, "Zenbook 14", 1, "AMD Ryzen 7", "16GB", 0, "512GB SSD" },
                    { 1003, 3, "Premium ultrabook with excellent display.", true, true, false, false, "XPS 13", 1, "Intel Core i7", "16GB", 0, "1TB SSD" },
                    { 1004, 4, "Versatile 2-in-1 laptop.", true, true, false, false, "Spectre x360", 1, "Intel Core i7", "16GB", 0, "1TB SSD" },
                    { 1005, 5, "Business-oriented laptop.", true, true, false, false, "ThinkPad X1 Carbon", 1, "Intel Core i7", "32GB", 0, "1TB SSD" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Laptops");
        }
    }
}

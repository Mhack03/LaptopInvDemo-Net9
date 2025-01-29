using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChangePKForLaptop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Laptops_LaptopSerialNumber",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LaptopSerialNumber",
                table: "Assignments");

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "SerialNumber",
                keyValue: "AQ5RrvbFR1");

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "SerialNumber",
                keyValue: "Drjg7rPPIb");

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "SerialNumber",
                keyValue: "fI5D0yyqxc");

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "SerialNumber",
                keyValue: "jdEluAbfxz");

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "SerialNumber",
                keyValue: "TZJHkvhCOe");

            migrationBuilder.AlterColumn<string>(
                name: "LaptopSerialNumber",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)");

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops",
                column: "LaptopId");

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 1,
                column: "LaptopId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 2,
                column: "LaptopId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 3,
                column: "LaptopId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 4,
                column: "LaptopId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 5,
                column: "LaptopId",
                value: null);

            migrationBuilder.InsertData(
                table: "Laptops",
                columns: new[] { "LaptopId", "Brand", "Description", "HasBag", "HasCharger", "HasKeyboard", "HasMouse", "Model", "OperatingSystem", "Processor", "Ram", "SerialNumber", "Status", "Storage" },
                values: new object[,]
                {
                    { 1001, 0, "Lightweight and portable laptop for everyday use.", true, true, false, false, "Swift 3", 1, "Intel Core i5", "8GB", "TZJHkvhCOe", 0, "256GB SSD" },
                    { 1002, 2, "Stylish and powerful laptop for professionals.", true, true, false, false, "Zenbook 14", 1, "AMD Ryzen 7", "16GB", "AQ5RrvbFR1", 0, "512GB SSD" },
                    { 1003, 3, "Premium ultrabook with excellent display.", true, true, false, false, "XPS 13", 1, "Intel Core i7", "16GB", "jdEluAbfxz", 0, "1TB SSD" },
                    { 1004, 4, "Versatile 2-in-1 laptop.", true, true, false, false, "Spectre x360", 1, "Intel Core i7", "16GB", "fI5D0yyqxc", 0, "1TB SSD" },
                    { 1005, 5, "Business-oriented laptop.", true, true, false, false, "ThinkPad X1 Carbon", 1, "Intel Core i7", "32GB", "Drjg7rPPIb", 0, "1TB SSD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LaptopId",
                table: "Assignments",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Laptops_LaptopId",
                table: "Assignments",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "LaptopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Laptops_LaptopId",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LaptopId",
                table: "Assignments");

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1005);

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "Assignments");

            migrationBuilder.AlterColumn<string>(
                name: "LaptopSerialNumber",
                table: "Assignments",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops",
                column: "SerialNumber");

            migrationBuilder.InsertData(
                table: "Laptops",
                columns: new[] { "SerialNumber", "Brand", "Description", "HasBag", "HasCharger", "HasKeyboard", "HasMouse", "LaptopId", "Model", "OperatingSystem", "Processor", "Ram", "Status", "Storage" },
                values: new object[,]
                {
                    { "AQ5RrvbFR1", 2, "Stylish and powerful laptop for professionals.", true, true, false, false, 1002, "Zenbook 14", 1, "AMD Ryzen 7", "16GB", 0, "512GB SSD" },
                    { "Drjg7rPPIb", 5, "Business-oriented laptop.", true, true, false, false, 1005, "ThinkPad X1 Carbon", 1, "Intel Core i7", "32GB", 0, "1TB SSD" },
                    { "fI5D0yyqxc", 4, "Versatile 2-in-1 laptop.", true, true, false, false, 1004, "Spectre x360", 1, "Intel Core i7", "16GB", 0, "1TB SSD" },
                    { "jdEluAbfxz", 3, "Premium ultrabook with excellent display.", true, true, false, false, 1003, "XPS 13", 1, "Intel Core i7", "16GB", 0, "1TB SSD" },
                    { "TZJHkvhCOe", 0, "Lightweight and portable laptop for everyday use.", true, true, false, false, 1001, "Swift 3", 1, "Intel Core i5", "8GB", 0, "256GB SSD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LaptopSerialNumber",
                table: "Assignments",
                column: "LaptopSerialNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Laptops_LaptopSerialNumber",
                table: "Assignments",
                column: "LaptopSerialNumber",
                principalTable: "Laptops",
                principalColumn: "SerialNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

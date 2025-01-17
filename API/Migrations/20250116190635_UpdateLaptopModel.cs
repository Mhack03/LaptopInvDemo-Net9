using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLaptopModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Laptops",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1001,
                column: "SerialNumber",
                value: "TZJHkvhCOe");

            migrationBuilder.UpdateData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1002,
                column: "SerialNumber",
                value: "AQ5RrvbFR1");

            migrationBuilder.UpdateData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1003,
                column: "SerialNumber",
                value: "jdEluAbfxz");

            migrationBuilder.UpdateData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1004,
                column: "SerialNumber",
                value: "fI5D0yyqxc");

            migrationBuilder.UpdateData(
                table: "Laptops",
                keyColumn: "LaptopId",
                keyValue: 1005,
                column: "SerialNumber",
                value: "Drjg7rPPIb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Laptops");
        }
    }
}

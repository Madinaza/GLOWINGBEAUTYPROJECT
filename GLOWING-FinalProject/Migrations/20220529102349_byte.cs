using Microsoft.EntityFrameworkCore.Migrations;

namespace GLOWING_FinalProject.Migrations
{
    public partial class @byte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Sliders",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Order",
                table: "Sliders",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

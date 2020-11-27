using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchEngine.Migrations
{
    public partial class addJsCssCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Css",
                table: "PositonAndDates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Js",
                table: "PositonAndDates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "WordCount",
                table: "PositonAndDates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Css",
                table: "PositonAndDates");

            migrationBuilder.DropColumn(
                name: "Js",
                table: "PositonAndDates");

            migrationBuilder.DropColumn(
                name: "WordCount",
                table: "PositonAndDates");
        }
    }
}

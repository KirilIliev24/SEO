using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchEngine.Migrations
{
    public partial class addedNumberInTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "noOfKeywords",
                table: "KeywordsInText",
                newName: "keywordsInText");

            migrationBuilder.AddColumn<int>(
                name: "keywordsInMetaTags",
                table: "KeywordsInText",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "keywordsInMetaTags",
                table: "KeywordsInText");

            migrationBuilder.RenameColumn(
                name: "keywordsInText",
                table: "KeywordsInText",
                newName: "noOfKeywords");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estate.DataAccessLayer.Migrations
{
    public partial class smallchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeihbourhoodId",
                table: "Adverts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeihbourhoodId",
                table: "Adverts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

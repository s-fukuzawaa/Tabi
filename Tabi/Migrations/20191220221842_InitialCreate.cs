using Microsoft.EntityFrameworkCore.Migrations;

namespace Tabi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(nullable: true),
                    placetype = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    imgUrl = table.Column<string>(nullable: true),
                    map = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    age = table.Column<int>(nullable: false),
                    size = table.Column<int>(nullable: false),
                    budget = table.Column<int>(nullable: false),
                    planeTicket = table.Column<bool>(nullable: false),
                    duration = table.Column<int>(nullable: false),
                    region = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.DropTable(
                name: "Survey");
        }
    }
}

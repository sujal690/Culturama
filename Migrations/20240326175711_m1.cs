using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegionalTempleInfo.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "information",
                columns: table => new
                {
                    sr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nameofgod = table.Column<string>(name: "Name_of_god", type: "nvarchar(max)", nullable: false),
                    nameofmandir = table.Column<string>(name: "name_of_mandir", type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exactlocation = table.Column<string>(name: "exact_location", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_information", x => x.sr);
                });

            migrationBuilder.CreateTable(
                name: "registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneno = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registrations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "information");

            migrationBuilder.DropTable(
                name: "registrations");
        }
    }
}

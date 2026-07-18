using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBox.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_heroSlider_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Events",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HeroSliders",
                columns: table => new
                {
                    HeroSliderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSliders", x => x.HeroSliderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroSliders");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Events");
        }
    }
}

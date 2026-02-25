using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerInitiativesSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToInitiative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxParticipants",
                table: "Initiatives",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxParticipants",
                table: "Initiatives");
        }
    }
}

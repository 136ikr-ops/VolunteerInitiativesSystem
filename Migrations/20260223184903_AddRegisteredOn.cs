using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerInitiativesSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisteredOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "InitiativeParticipants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "InitiativeParticipants");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addedvotingendedboolpropinevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Activities_ActivityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Events_EventId",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "VotingEnded",
                table: "Events",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Activities_ActivityId",
                table: "Users",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_EventId",
                table: "Users",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Activities_ActivityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Events_EventId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VotingEnded",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Activities_ActivityId",
                table: "Users",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_EventId",
                table: "Users",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

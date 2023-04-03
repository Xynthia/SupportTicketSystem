using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class ResposibleForTicketAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsibleForID",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ResponsibleForID",
                table: "Ticket",
                column: "ResponsibleForID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_ResponsibleForID",
                table: "Ticket",
                column: "ResponsibleForID",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_ResponsibleForID",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_ResponsibleForID",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "ResponsibleForID",
                table: "Ticket");
        }
    }
}

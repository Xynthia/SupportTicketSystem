using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class ConversationToUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToUserId",
                table: "Conversation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ToUserId",
                table: "Conversation",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_User_ToUserId",
                table: "Conversation",
                column: "ToUserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_User_ToUserId",
                table: "Conversation");

            migrationBuilder.DropIndex(
                name: "IX_Conversation_ToUserId",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Conversation");
        }
    }
}

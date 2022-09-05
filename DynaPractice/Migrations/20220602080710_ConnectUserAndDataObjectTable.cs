using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynaPractice.Migrations
{
    public partial class ConnectUserAndDataObjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DataObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DataObjects_UserId",
                table: "DataObjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataObjects_Users_UserId",
                table: "DataObjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataObjects_Users_UserId",
                table: "DataObjects");

            migrationBuilder.DropIndex(
                name: "IX_DataObjects_UserId",
                table: "DataObjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DataObjects");
        }
    }
}

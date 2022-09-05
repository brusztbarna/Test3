using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynaPractice.Migrations
{
    public partial class ConnectUserAndBlueprintTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Blueprints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blueprints_UserId",
                table: "Blueprints",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blueprints_Users_UserId",
                table: "Blueprints",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blueprints_Users_UserId",
                table: "Blueprints");

            migrationBuilder.DropIndex(
                name: "IX_Blueprints_UserId",
                table: "Blueprints");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Blueprints");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynaPractice.Migrations
{
    public partial class CreateOneToOneInDataOjectAndStructre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataObjectId",
                table: "DataStructures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DataStructures_DataObjectId",
                table: "DataStructures",
                column: "DataObjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStructures_DataObjects_DataObjectId",
                table: "DataStructures",
                column: "DataObjectId",
                principalTable: "DataObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataStructures_DataObjects_DataObjectId",
                table: "DataStructures");

            migrationBuilder.DropIndex(
                name: "IX_DataStructures_DataObjectId",
                table: "DataStructures");

            migrationBuilder.DropColumn(
                name: "DataObjectId",
                table: "DataStructures");
        }
    }
}

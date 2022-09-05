using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynaPractice.Migrations
{
    public partial class AddDataObjectAndDataStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false),
                    DataTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataSample = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CallMethod = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataStructures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    DataValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataMask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUnitPosition = table.Column<int>(type: "int", nullable: false),
                    DataLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStructures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataObjects");

            migrationBuilder.DropTable(
                name: "DataStructures");
        }
    }
}

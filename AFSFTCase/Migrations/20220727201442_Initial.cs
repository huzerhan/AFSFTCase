using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFSFTCase.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataModelEntities",
                columns: table => new
                {
                    TextId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslatedString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Translator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataModelEntities", x => x.TextId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataModelEntities");
        }
    }
}

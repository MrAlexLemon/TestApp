using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebService.Migrations
{
    public partial class InitMigraion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputerInfos",
                columns: table => new
                {
                    ComputerName = table.Column<string>(nullable: false),
                    TimeZone = table.Column<string>(nullable: false),
                    OsName = table.Column<string>(nullable: false),
                    DotNetVersion = table.Column<string>(nullable: false),
                    ConnectedTime = table.Column<DateTime>(nullable: false),
                    DisconnectedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerInfos", x => x.ComputerName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerInfos");
        }
    }
}

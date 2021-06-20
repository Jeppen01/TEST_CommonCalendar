using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TEST_CommonCalendar.Data.Migrations
{
    public partial class @event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mEVENT",
                columns: table => new
                {
                    k_EVENT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    s_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    s_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    d_DateTimeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    d_DateTimeTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mEVENT", x => x.k_EVENT_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mEVENT");
        }
    }
}

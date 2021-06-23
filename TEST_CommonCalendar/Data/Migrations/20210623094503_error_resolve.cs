using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TEST_CommonCalendar.Data.Migrations
{
    public partial class error_resolve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "d_ResolutionDateTime",
                table: "mErrorHandling",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "d_ResolutionDateTime",
                table: "mErrorHandling");
        }
    }
}

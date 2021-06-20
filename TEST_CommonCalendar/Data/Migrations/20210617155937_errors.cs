using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TEST_CommonCalendar.Data.Migrations
{
    public partial class errors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mErrorHandling",
                columns: table => new
                {
                    k_ErrorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    s_ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    s_FunctionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    s_ExceptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    d_OccurTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mErrorHandling", x => x.k_ErrorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mErrorHandling");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProxyPeeker.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "request",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    send_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    request_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    request_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    response_status_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    response_body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_request", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "request");
        }
    }
}

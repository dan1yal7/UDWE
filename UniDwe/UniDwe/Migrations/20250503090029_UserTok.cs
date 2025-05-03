using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniDwe.Migrations
{
    /// <inheritdoc />
    public partial class UserTok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usertokens",
                columns: table => new
                {
                    UserTokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usertokens", x => x.UserTokenId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usertokens");
        }
    }
}

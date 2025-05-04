using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniDwe.Migrations
{
    /// <inheritdoc />
    public partial class profileimageprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "profiles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "profiles");
        }
    }
}

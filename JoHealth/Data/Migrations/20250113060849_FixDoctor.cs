using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doctor_Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctor_Password",
                table: "AspNetUsers");
        }
    }
}

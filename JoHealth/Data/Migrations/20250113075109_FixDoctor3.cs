using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDoctor3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doctor_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Doctor_LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctor_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Doctor_LastName",
                table: "AspNetUsers");
        }
    }
}

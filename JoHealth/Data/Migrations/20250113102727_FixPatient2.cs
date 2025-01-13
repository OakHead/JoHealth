using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPatient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AspNetUsers",
                newName: "Patient_LastName");

            migrationBuilder.AddColumn<string>(
                name: "Patient_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Patient_FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Patient_LastName",
                table: "AspNetUsers",
                newName: "Password");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Migrations
{
    /// <inheritdoc />
    public partial class AddPharmacistRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pharmacist_LastName",
                table: "AspNetUsers",
                newName: "Patient_LastName");

            migrationBuilder.RenameColumn(
                name: "Pharmacist_ImageUrl",
                table: "AspNetUsers",
                newName: "Patient_ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Pharmacist_FirstName",
                table: "AspNetUsers",
                newName: "Patient_FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patient_LastName",
                table: "AspNetUsers",
                newName: "Pharmacist_LastName");

            migrationBuilder.RenameColumn(
                name: "Patient_ImageUrl",
                table: "AspNetUsers",
                newName: "Pharmacist_ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Patient_FirstName",
                table: "AspNetUsers",
                newName: "Pharmacist_FirstName");
        }
    }
}

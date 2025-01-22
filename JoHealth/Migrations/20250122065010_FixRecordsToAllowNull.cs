using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Migrations
{
    /// <inheritdoc />
    public partial class FixRecordsToAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdministeredByPharmacist",
                table: "NewRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Prescriptions",
                table: "NewRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prescriptions",
                table: "NewRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdministeredByPharmacist",
                table: "NewRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

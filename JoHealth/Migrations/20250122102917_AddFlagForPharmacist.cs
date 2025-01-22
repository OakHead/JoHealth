using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Migrations
{
    /// <inheritdoc />
    public partial class AddFlagForPharmacist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdministeredByPharmacist",
                table: "NewRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdministeredByPharmacist",
                table: "NewRecords");
        }
    }
}

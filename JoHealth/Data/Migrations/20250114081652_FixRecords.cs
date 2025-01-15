using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoHealth.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Records_RecordId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_PatientId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_RecordId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "SubmissionDate",
                table: "Records",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Records",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Records",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Records",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_PatientId",
                table: "Records",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_PatientId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Records");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Records",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Records",
                newName: "SubmissionDate");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Records",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_RecordId",
                table: "Medicines",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Records_RecordId",
                table: "Medicines",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_PatientId",
                table: "Records",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_Care_System.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditPropertiesToBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Doctors");
        }
    }
}

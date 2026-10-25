using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_Care_System.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentAuditProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Appointments");
        }
    }
}

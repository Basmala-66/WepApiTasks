using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_Care_System.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     migrationBuilder.AddColumn<bool>(
     name: "IsDeleted",
     table: "Patients",
     type: "bit",
     nullable: false,
     defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
       migrationBuilder.DropColumn(
       name: "IsDeleted",
       table: "Patients");
        }
    }
}

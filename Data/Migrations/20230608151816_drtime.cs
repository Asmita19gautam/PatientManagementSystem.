using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMgmtfinal.Data.Migrations
{
    public partial class drtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApppointmentTime",
                table: "PatientDoctorAppointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApppointmentTime",
                table: "PatientDoctorAppointment");
        }
    }
}

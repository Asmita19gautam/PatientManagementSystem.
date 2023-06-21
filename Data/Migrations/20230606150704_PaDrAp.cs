using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMgmtfinal.Data.Migrations
{
    public partial class PaDrAp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepID",
                table: "PatientDoctorAppointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientDoctorAppointment_DepID",
                table: "PatientDoctorAppointment",
                column: "DepID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDoctorAppointment_Department_DepID",
                table: "PatientDoctorAppointment",
                column: "DepID",
                principalTable: "Department",
                principalColumn: "DepID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDoctorAppointment_Department_DepID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropIndex(
                name: "IX_PatientDoctorAppointment_DepID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropColumn(
                name: "DepID",
                table: "PatientDoctorAppointment");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMgmtfinal.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DoctorTimeDrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.RenameColumn(
                name: "DoctorTimeDrTimeID",
                table: "PatientDoctorAppointment",
                newName: "DoctorTimingDrTimeID");

            migrationBuilder.RenameIndex(
                name: "IX_PatientDoctorAppointment_DoctorTimeDrTimeID",
                table: "PatientDoctorAppointment",
                newName: "IX_PatientDoctorAppointment_DoctorTimingDrTimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment",
                column: "DoctorTimingDrTimeID",
                principalTable: "DoctorTiming",
                principalColumn: "DrTimeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.RenameColumn(
                name: "DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment",
                newName: "DoctorTimeDrTimeID");

            migrationBuilder.RenameIndex(
                name: "IX_PatientDoctorAppointment_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment",
                newName: "IX_PatientDoctorAppointment_DoctorTimeDrTimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DoctorTimeDrTimeID",
                table: "PatientDoctorAppointment",
                column: "DoctorTimeDrTimeID",
                principalTable: "DoctorTiming",
                principalColumn: "DrTimeID");
        }
    }
}

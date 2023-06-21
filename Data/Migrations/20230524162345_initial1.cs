using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMgmtfinal.Data.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Department_DepartmentDepID",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropIndex(
                name: "IX_PatientDoctorAppointment_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_DepartmentDepID",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropColumn(
                name: "DepartmentDepID",
                table: "Doctor");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDoctorAppointment_DrTimeID",
                table: "PatientDoctorAppointment",
                column: "DrTimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DepID",
                table: "Doctor",
                column: "DepID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Department_DepID",
                table: "Doctor",
                column: "DepID",
                principalTable: "Department",
                principalColumn: "DepID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DrTimeID",
                table: "PatientDoctorAppointment",
                column: "DrTimeID",
                principalTable: "DoctorTiming",
                principalColumn: "DrTimeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Department_DepID",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropIndex(
                name: "IX_PatientDoctorAppointment_DrTimeID",
                table: "PatientDoctorAppointment");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_DepID",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentDepID",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientDoctorAppointment_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment",
                column: "DoctorTimingDrTimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DepartmentDepID",
                table: "Doctor",
                column: "DepartmentDepID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Department_DepartmentDepID",
                table: "Doctor",
                column: "DepartmentDepID",
                principalTable: "Department",
                principalColumn: "DepID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDoctorAppointment_DoctorTiming_DoctorTimingDrTimeID",
                table: "PatientDoctorAppointment",
                column: "DoctorTimingDrTimeID",
                principalTable: "DoctorTiming",
                principalColumn: "DrTimeID");
        }
    }
}

namespace PatientMgmtfinal.Models
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        Moderator,
        Basic
    }
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }
        public static class DoctorPatientManagement
        {
            public const string View = "Permissions.DoctorPatientManagement.View";
            public const string Create = "Permissions.DoctorPatientManagement.Create";
            public const string Edit = "Permissions.DoctorPatientManagement.Edit";
            public const string Delete = "Permissions.DoctorPatientManagement.Delete";
        }
    }


}

using System.Security.Principal;

namespace EmployeeManagement.Utils.Constant
{
    public class Constant
    {
        // public const int MaxLengthOfEntity = 100;
        // public const int MaxLengthOfEthnicGroup = 20;
        // public const int MaxLengthOfOccupation = 20;
        public const int MaxLengthOfName = 40;
        public const int LengthOfCitizenIdentityCardNumber = 12;
        public const int LengthOfPhoneNumber = 10;
        public const int MinBirthdateYear = 1970;
        public const int MaxDuration = 20;
        public const int MaxNumberOfDiplomasOfEachEmployee = 3;
        public const string ProvinceFileName = "C:/Users/phong/Desktop/Workspace/Oceantech/EmployeeManagement/EmployeeManagement.Models/ExcelFile/Province.xls";
        public const string DistrictFileName = "C:/Users/phong/Desktop/Workspace/Oceantech/EmployeeManagement/EmployeeManagement.Models/ExcelFile/District.xls";
        public const string CommuneFileName = "C:/Users/phong/Desktop/Workspace/Oceantech/EmployeeManagement/EmployeeManagement.Models/ExcelFile/Commune.xls";
        public const string SaveFileName = "D:/Employee.xls";
        public const string ExportedFileName = "D:/Employee.xlsx";
        
        public const int SizeOfEmployeePage = 20;
        public const int SizeOfProvincePage = 25;
        public const int SizeOfDistrictPage = 50;
        public const int SizeOfCommunePage = 60;
        public const int SizeOfDiplomaPage = 10;
        public const int SizeOfAwardDiplomaPage = 20;
    }
}

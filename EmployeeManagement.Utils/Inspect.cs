using System.Globalization;
using System.Reflection;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.Utils
{
    public static class Inspect
    {
        public static bool IsValidNameWithDiacritics(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            string decomposedName = RemoveDiacritics(name.Replace("đ", "d").Replace("Đ", "D")); // Bỏ dấu tiếng Việt
            return !decomposedName.Any(n => !char.IsAsciiLetter(n) && !char.IsWhiteSpace(n));
        }

        public static bool IsValidDiplomaNameWithDiacritics(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var decomposedName = RemoveDiacritics(name.Replace("đ", "d").Replace("Đ", "D")); // Bỏ dấu tiếng Việt
            return !decomposedName.Any(n =>
                !char.IsAsciiLetter(n) && !char.IsWhiteSpace(n) && !Equals(n, ',') && !Equals(n, '(') &&
                !Equals(n, ')') && !Equals(n, '&'));
        }

        public static bool IsExistent(
            Employee employee,
            List<Employee> employeeList,
            string type
        )
        {
            return type switch
            {
                "PhoneNumber" => employeeList.Where(e => e.Id != employee.Id)
                    .Any(e => e.PhoneNumber?.CompareTo(employee.PhoneNumber) == 0),
                "CitizenNumber" => employeeList.Where(e => e.Id != employee.Id)
                    .Any(e => e.CitizenIdentityCard?.CompareTo(employee.CitizenIdentityCard) == 0),
                _ => true
            };
        }

        public static bool IsDuplicatedName<T>(string? name, int id, List<T> list) where T : GenericEntity
        {
            list = list.Where(e => e.Id != id).ToList();
            if (string.IsNullOrEmpty(name)) return false;
            name = name.Trim();
            List<string?> nameList = new();
            foreach (var stuff in list)
            {
                Type stuffType = stuff.GetType();
                PropertyInfo[] stuffProperties = stuffType.GetProperties();
                foreach (var prop in stuffProperties)
                {
                    if (prop.Name.Equals("Name"))
                    {
                        var value = prop.GetValue(stuff);
                        nameList.Add(Convert.ToString(value));
                    }
                }
            }

            return nameList.Any(n => string.CompareOrdinal(n, name) == 0);
        }


        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

        public static bool IsDuplicatedAwardDiploma(List<AwardDiploma> awardingList, int id, AwardDiploma award)
        {
            List<AwardDiploma> awardingDiplomas = awardingList.Where(a =>
                    a.EmployeeId == award.EmployeeId && GetYear(a.GrantingDate) < a.Duration &&
                    award.Id != id)
                .ToList();
            return awardingDiplomas.Count > 0 && awardingDiplomas.Any(a => a.DiplomaId == award.DiplomaId);
        }

        public static bool IsValidNumberOfDiplomaOfEachEmployee(List<AwardDiploma> awardingList, int employeeId, int id)
        {
            int numberOfDiplomas = awardingList
                .Where(a => a.EmployeeId == employeeId && GetYear(a.GrantingDate) < a.Duration && a.Id != id )
                .ToList().Count;
            return numberOfDiplomas <= Constant.Constant.MaxNumberOfDiplomasOfEachEmployee - 1;
        }
        
        public static int GetYear(DateTime datetime)
        {
            DateTime firstDay = new(1, 1, 1);
            TimeSpan difference = DateTime.Now.Subtract(datetime);
            int age = (firstDay + difference).Year - 1;
            return age;
        }
        
        public static bool IsValidDatetime(DateTime datetime)
        {
            return datetime.CompareTo(DateTime.Now) <= 0 && datetime.CompareTo(new DateTime( Constant.Constant.MinBirthdateYear,1,1)) >= 0;
        }

        public static bool IsContainOnlyNumber(string? a)
        {
            return string.IsNullOrEmpty(a) || a.All(char.IsDigit);
        }

        public static string GetDatetimeString(DateTime? datetime)
        {
            if(datetime == null) return string.Empty;
            return datetime.Value.Day + "/" + datetime.Value.Month + "/" + datetime.Value.Year;
        }
    }
}
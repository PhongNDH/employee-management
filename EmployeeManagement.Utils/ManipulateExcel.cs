using System.Data;
using System.Text;
using EmployeeManagement.Models;
using ClosedXML.Excel;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Helper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;


namespace EmployeeManagement.Utils;

public static class ManipulateExcel
{
    // Export xls file
    // public static void SaveEmployee(List<Employee> employeeList)
    // {
    //     HSSFWorkbook workbook = new();
    //     ISheet sheet = workbook.CreateSheet("Employees");
    //
    //     IRow headerRow = sheet.CreateRow(0);
    //     headerRow.CreateCell(0).SetCellValue("ID");
    //     headerRow.CreateCell(1).SetCellValue("Name");
    //     headerRow.CreateCell(2).SetCellValue("Date Of Birth");
    //     headerRow.CreateCell(3).SetCellValue("Ethnic Group");
    //     headerRow.CreateCell(4).SetCellValue("Occupation");
    //     headerRow.CreateCell(5).SetCellValue("Citizen Identity Card");
    //     headerRow.CreateCell(6).SetCellValue("Phone Number");
    //     headerRow.CreateCell(7).SetCellValue("Province");
    //     headerRow.CreateCell(8).SetCellValue("District");
    //     headerRow.CreateCell(9).SetCellValue("Commune");
    //
    //     var rowNumber = 1;
    //     foreach (var employee in employeeList)
    //     {
    //         IRow row = sheet.CreateRow(rowNumber);
    //         row.CreateCell(0).SetCellValue(employee.Id);
    //         row.CreateCell(1).SetCellValue(employee.Name);
    //         row.CreateCell(2).SetCellValue(Inspect.GetDatetimeString(employee.Dob));
    //         row.CreateCell(3).SetCellValue(employee.EthnicGroup?.Name);
    //         row.CreateCell(4).SetCellValue(employee.Occupation?.Name);
    //         row.CreateCell(5).SetCellValue(employee.CitizenIdentityCard);
    //         row.CreateCell(6).SetCellValue(employee.PhoneNumber);
    //         row.CreateCell(7).SetCellValue(employee.Commune?.District?.Province?.Name);
    //         row.CreateCell(8).SetCellValue(employee.Commune?.District?.Name);
    //         row.CreateCell(9).SetCellValue(employee.Commune?.Name);
    //         rowNumber++;
    //     }
    //
    //     FileStream fileStream = new(Constant.Constant.SaveFileName, FileMode.Create, FileAccess.Write);
    //     workbook.Write(fileStream);
    // }

    public static void ExportEmployee(List<Employee> employeeList)
    {
        // Export xlsx file
        XLWorkbook workbook = new();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        WriteEmployeeHeader(worksheet);

        WriteEmployee(employeeList, worksheet);

        workbook.SaveAs(Constant.Constant.ExportedFileName);
    }

    public static Response ImportEmployee(XLWorkbook excelFile, List<EthnicGroup> ethnicGroups,
        List<Occupation> occupations, List<Commune> communes)
    {
        var employees = new List<Employee>();
        try
        {
            var startRow = 2;
            var sheet = excelFile.Worksheet(1);
            for (var i = startRow; i <= sheet.LastRowUsed()!.RowNumber(); i++)
            {
                var row = sheet.Row(i);
                Employee employee = new();
                StringBuilder errorMessage = new();

                GetEmployee(sheet, row, errorMessage, employee, occupations, ethnicGroups, communes);

                if (!string.IsNullOrEmpty(errorMessage.ToString()))
                {
                    return new Response(new ErrorMessage(errorMessage.ToString()));
                }

                employees.Add(employee);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new Response(employees);
    }

    private static void GetEmployee(IXLWorksheet sheet, IXLRow row, StringBuilder errorMessage,
        Employee employee, List<Occupation> occupations, List<EthnicGroup> ethnicGroups, List<Commune> communes)
    {
        var startColumn = 1;
        for (var column = startColumn; column <= row.CellCount(); column++)
        {
            switch (column)
            {
                case 1:
                    GetName(sheet, row.RowNumber(), column, errorMessage, employee);
                    break;
                case 2:
                    GetDateOfBirth(sheet, row.RowNumber(), column, errorMessage, employee);
                    break;
                case 3:
                    GetEthnicGroup(sheet, row.RowNumber(), column, errorMessage, employee, ethnicGroups);
                    break;
                case 4:
                    GetOccupation(sheet, row.RowNumber(), column, errorMessage, employee, occupations);
                    break;
                case 5:
                    GetCitizenIdentityCardNumber(sheet, row.RowNumber(), column, errorMessage, employee);
                    break;
                case 6:
                    GetPhoneNumber(sheet, row.RowNumber(), column, errorMessage, employee);
                    break;
                case 9:
                    GetCommune(sheet, row.RowNumber(), column, errorMessage, employee, communes);
                    break;
            }
        }
    }

    private static void GetName(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee)
    {
        if (!Inspect.IsValidNameWithDiacritics(sheet.Cell(row, column).Value.ToString()))
        {
            errorMessage.Append($"Name must contain only letter. Error in row {row} column {column}.\n");
            return;
        }

        employee.Name = sheet.Cell(row, column).Value.ToString();
    }

    private static void GetDateOfBirth(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee)
    {
        var isValid = true;
        var dateString = sheet.Cell(row, column).Value.ToString();
        var isCorrectFormat = DateTime.TryParseExact(dateString,
            "d/M/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime date);
        if (!isCorrectFormat)
        {
            errorMessage.Append($"Birthday is not in d/M/yyyy format in row {row} column {column}.\n");
            isValid = false;
        }

        if (!Inspect.IsValidDatetime(date))
        {
            errorMessage.Append(
                $"Birthday must in the past and after 1/1/{Constant.Constant.MinBirthdateYear}. Error in row {row}  column {column}.\n");
            isValid = false;
        }

        if (isValid)
        {
            employee.Dob = date;
        }
    }

    private static void GetEthnicGroup(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee, List<EthnicGroup> ethnicGroups)
    {
        var ethnicId = ethnicGroups
            .Find(x => x.Name == sheet.Cell(row, column).Value.ToString())?.Id;
        if (ethnicId == null)
        {
            errorMessage.Append($"Ethnic Group is not specified row {row}, column {column}.\n");
            return;
        }

        employee.EthnicGroupId = (int)ethnicId;
    }

    private static void GetOccupation(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee, List<Occupation> occupations)
    {
        var jobId = occupations.Find(x => x.Name == sheet.Cell(row, column).Value.ToString())
            ?.Id;
        if (jobId == null)
        {
            errorMessage.Append($"Occupation is not specified in row {row}, column {column}.\n");
            return;
        }

        employee.OccupationId = (int)jobId;
    }

    private static void GetCitizenIdentityCardNumber(IXLWorksheet sheet, int row, int column,
        StringBuilder errorMessage, Employee employee)
    {
        var citizenIdentityCard = sheet.Cell(row, column).Value.ToString();
        if (!string.IsNullOrEmpty(citizenIdentityCard)
            && (citizenIdentityCard.Length != Constant.Constant.LengthOfCitizenIdentityCardNumber
                || !Inspect.IsContainOnlyNumber(citizenIdentityCard)))

        {
            errorMessage.Append(
                $"Citizen Identity Card Number must contains {Constant.Constant.LengthOfCitizenIdentityCardNumber} digits. Error in row {row}, column {column}.\n");
            return;
        }

        employee.CitizenIdentityCard = citizenIdentityCard;
    }

    private static void GetPhoneNumber(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee)
    {
        var phoneNumber = sheet.Cell(row, column).Value.ToString();
        if (!string.IsNullOrEmpty(phoneNumber)
            && (phoneNumber.Length != Constant.Constant.LengthOfPhoneNumber
                || !Inspect.IsContainOnlyNumber(phoneNumber)))
        {
            errorMessage.Append(
                $"Phone Number must contains {Constant.Constant.LengthOfPhoneNumber} digits. Error in row {row}, column {column}.\n");
            return;
        }

        employee.PhoneNumber = phoneNumber;
    }

    private static void GetCommune(IXLWorksheet sheet, int row, int column, StringBuilder errorMessage,
        Employee employee, List<Commune> communes)
    {
        var isValid = true;
        var communeName = sheet.Cell(row, column).Value.ToString();
        var districtName = sheet.Cell(row, column - 1).Value.ToString();
        var provinceName = sheet.Cell(row, column - 2).Value.ToString();
        var commune = communes
            .Find(x => x.Name == sheet.Cell(row, column).Value.ToString());
        if (string.IsNullOrEmpty(communeName) && string.IsNullOrEmpty(districtName) &&
            string.IsNullOrEmpty(provinceName))
            return;
        if (commune == null || commune?.District?.Name != districtName || commune?.District?.Province?.Name != provinceName)
        {
            isValid = false;
        }

        if (!isValid)
        {
            errorMessage.Append($"Commune, District, Province are not match in row {row}.\n");
            return;
        }

        employee.CommuneId = commune?.Id;
    }

    private static void WriteEmployeeHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell(1, 1).Value = "Name";
        worksheet.Cell(1, 2).Value = "Date Of Birth";
        worksheet.Cell(1, 3).Value = "Ethnic Group";
        worksheet.Cell(1, 4).Value = "Occupation";
        worksheet.Cell(1, 5).Value = "Citizen Identity Card";
        worksheet.Cell(1, 6).Value = "Phone Number";
        worksheet.Cell(1, 7).Value = "Province";
        worksheet.Cell(1, 8).Value = "District";
        worksheet.Cell(1, 9).Value = "Commune";
    }

    private static void WriteEmployee(List<Employee> employeeList, IXLWorksheet worksheet)
    {
        var row = 2;
        foreach (var employee in employeeList)
        {
            worksheet.Cell(row, 1).Value = employee.Name;
            worksheet.Cell(row, 2).Value = Inspect.GetDatetimeString(employee.Dob);
            worksheet.Cell(row, 3).Value = employee.EthnicGroup?.Name;
            worksheet.Cell(row, 4).Value = employee.Occupation?.Name;
            worksheet.Cell(row, 5).Value = employee.CitizenIdentityCard;
            worksheet.Cell(row, 6).Value = employee.PhoneNumber;
            worksheet.Cell(row, 7).Value = employee.Commune?.District?.Province?.Name;
            worksheet.Cell(row, 8).Value = employee.Commune?.District?.Name;
            worksheet.Cell(row, 9).Value = employee.Commune?.Name;
            row++;
        }
    }
}
using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EmployeeManagement.Utils
{
    public static class InitialiseEntityFromExcel
    {
        public static List<Province> GetProvinces(string filename)
        {
            List<Province> provinces = new();
            try
            {
                var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);
                for (var i = 1; i <= sheet.LastRowNum - 1; i++)
                {
                    var row = sheet.GetRow(i);
                    var province = ExtractProvinceDetails(row);
                    provinces.Add(province);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return provinces;
        }

        public static List<District> GetDistricts(string filename)
        {
            List<District> districts = new();
            try
            {
                using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);
                for (var i = 1; i <= sheet.LastRowNum - 1; i++)
                {
                    var row = sheet.GetRow(i);
                    var district = ExtractDistrictDetails(row);
                    districts.Add(district);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return districts;
        }

        public static List<Commune> GetCommunes(string filename)
        {
            List<Commune> communes = new();
            try
            {
                using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);
                for (var i = 1; i <= sheet.LastRowNum - 1; i++)
                {
                    var row = sheet.GetRow(i);
                    var commune = ExtractCommuneDetails(row);
                    communes.Add(commune);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return communes;
        }

        private static Province ExtractProvinceDetails(IRow? row)
        {
            Province province = new();
            if (row == null) return province;
            for (var j = 0; j < row.LastCellNum; j++)
            {
                var cell = row.GetCell(j);
                if (cell != null)
                {
                    SetProvinceDetails(province, j, cell);
                }
            }
            return province;
        }
        
        private static District ExtractDistrictDetails(IRow? row)
        {
            District district = new();
            if (row == null) return district;
            for (var j = 0; j < row.LastCellNum; j++)
            {
                var cell = row.GetCell(j);
                if (cell != null)
                {
                    SetDistrictDetails(district, j, cell);
                }
            }
            return district;
        }
        
        private static Commune ExtractCommuneDetails(IRow? row)
        {
            Commune commune = new();
            if (row == null) return commune;
            for (var j = 0; j < row.LastCellNum; j++)
            {
                var cell = row.GetCell(j);
                if (cell != null)
                {
                    SetCommuneDetails(commune, j, cell);
                }
            }
            return commune;
        }

        private static void SetProvinceDetails(Province province, int cellIndex, ICell cell)
        {
            switch (cellIndex)
            {
                case 0:
                    province.Id = int.Parse(cell.StringCellValue);
                    break;
                case 1:
                    province.Name = cell.StringCellValue;
                    break;
                case 2:
                    province.Level = cell.StringCellValue;
                    break;
            }
        }
        
        private static void SetDistrictDetails(District district, int cellIndex, ICell cell)
        {
            switch (cellIndex)
            {
                case 0:
                    district.Id = int.Parse(cell.StringCellValue);
                    break;
                case 1:
                    district.Name = cell.StringCellValue;
                    break;
                case 2:
                    district.Level = cell.StringCellValue;
                    break;
                case 3:
                    district.ProvinceId = int.Parse(cell.StringCellValue);
                    break;
            }
        }
        
        private static void SetCommuneDetails(Commune commune, int cellIndex, ICell cell)
        {
            switch (cellIndex)
            {
                case 0:
                    commune.Id = int.Parse(cell.StringCellValue);
                    break;
                case 1:
                    commune.Name = cell.StringCellValue;
                    break;
                case 2:
                    commune.Level = cell.StringCellValue;
                    break;
                case 3:
                    commune.DistrictId = int.Parse(cell.StringCellValue);
                    break;
            }
        }
    }
}
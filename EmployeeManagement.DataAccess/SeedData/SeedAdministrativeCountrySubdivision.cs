using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Utils;
using EmployeeManagement.Utils.Constant;

namespace EmployeeManagement.DataAccess.SeedData
{
    public class SeedAdministrativeCountrySubdivision
    {
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
        public List<Commune> Communes { get; set; }

        public SeedAdministrativeCountrySubdivision() {
            Provinces = InitialiseEntityFromExcel.GetProvinces(Constant.ProvinceFileName);
            Districts = InitialiseEntityFromExcel.GetDistricts(Constant.DistrictFileName);
            Communes = InitialiseEntityFromExcel.GetCommunes(Constant.CommuneFileName);
        }


    }
}

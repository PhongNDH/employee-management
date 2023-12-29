// using EmployeeManagement.DataAccess.Data;
// using EmployeeManagement.Models;
//
// namespace EmployeeManagement.DataAccess.Repository;
//
// public class ProvinceRepository : GenericRepository<Province>
// {
//     private readonly DatabaseContext _dbContext;
//     
//     public ProvinceRepository(DatabaseContext dbContext) : base(dbContext)
//     {
//         _dbContext = dbContext;
//     }
//     
//     public async Task DeleteProvince(Province province)
//     {
//         var districtsToDelete = await _dbContext.Districts.ToListAsync(province.)
//         await _dbContext.SaveChangesAsync();
//     }
// }
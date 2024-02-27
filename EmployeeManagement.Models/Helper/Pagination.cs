using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.Models.Helper;

public class Pagination<T> where T : GenericEntity
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int PageTotal { get; set; }

    public List<T> Data { get; set; }

    public Pagination(int page, int size, int total, List<T> data)
    {
        this.PageTotal = total;
        this.PageSize = size;
        this.PageNumber = page;
        this.Data = data;
    }
}
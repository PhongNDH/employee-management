namespace EmployeeManagement.Models.Entity;

public class Response
{
    public Response(ErrorMessage errorMessage)
    {
        this.ErrorMessage = errorMessage;
        this.Employee = new List<Employee>();
    }
    
    public Response(List<Employee> employee)
    {
        this.Employee = employee;
        this.ErrorMessage = null;
    }
    public ErrorMessage? ErrorMessage { get; set; }

    public List<Employee> Employee { get; set; }
}
namespace EmployeeManagement.Models.Entity;

public class ErrorMessage
{
    public ErrorMessage(string content)
    {
        this.Content = content;
    }
    public string? Content { get;  set; }
}
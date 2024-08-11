namespace BlazorUserManagerApp.Models.Responses;

public class GetAllEmployeesResponse :BaseResponse
{
    public List<Employee>? Employees { get; set; }
}

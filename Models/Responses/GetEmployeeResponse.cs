namespace BlazorUserManagerApp.Models.Responses
{
    public class GetEmployeeResponse : BaseResponse
    {
        public Employee Employee { get; set; }

        public GetEmployeeResponse(Employee employee) => Employee = employee;
    }
}

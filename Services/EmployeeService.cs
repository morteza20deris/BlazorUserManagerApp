using BlazorUserManagerApp.Data;
using BlazorUserManagerApp.Models.Responses;
using Microsoft.EntityFrameworkCore;
using BlazorUserManagerApp.Models;
using Microsoft.AspNetCore.Identity;


namespace BlazorUserManagerApp.Services;

public interface IEmployeeService
{
    Task<GetAllEmployeesResponse> GetEmployees();
    Task<BaseResponse> AddEmployee(Employee employee);
    Task<GetEmployeeResponse> GetEmployeeFromId(int id);
    Task<GetEmployeeResponse> GetEmployeeFromUsername(string userName);
    Task<BaseResponse> DeleteEmployee(Employee employee);
    Task<BaseResponse> UpdateEmployee(Employee employee);

    Task<BaseResponse> ResetPassword(Employee employee,string password);
}

public class EmployeeService : IEmployeeService
{
    private readonly IDbContextFactory<DataContext> _factory;
    private readonly UserManager<IdentityUser> _userManager;

    public EmployeeService(IDbContextFactory<DataContext> factory, UserManager<IdentityUser> userManager)
    {
        _factory = factory;
        _userManager = userManager;
    }

    public async Task<BaseResponse> AddEmployee(Employee form)
    {
        var response = new BaseResponse();
        try
        {
            var identity = new Employee
            {
                UserName = form.UserName,
                Id = form.UserName.GetHashCode().ToString(),
                Email = form.Email,
                Avatar = form.Avatar,
                FullName = form.FullName,
                PhoneNumber = form.PhoneNumber,
                Role = form.Role,
                Salary = form.Salary,
                Type = form.Type,
                ChangePaswword = true
                
            };
            var result = await _userManager.CreateAsync(identity, form.PasswordHash);
            if (result.Succeeded)
            {
                response.StatusCode = 200;
                response.Message = "Employee Added succesfully";
            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Error accurred while adding new Employee";
            }

            //using (var context = _factory.CreateDbContext())
            //{
            //    context.Add(new Employee
            //    {
            //        FullName = form.Name,
            //        Role = form.Position,

            //    });

            //    var result = await context.SaveChangesAsync();

            //    if (result == 1)
            //    {
            //        response.StatusCode = 200;
            //        response.Message = "Employee Added succesfully";
            //    }
            //    else
            //    {
            //        response.StatusCode = 400;
            //        response.Message = "Error accurred while adding new Employee";
            //    }
            //}
        }
        catch (Exception ex) { 
            response.StatusCode=500;
            response.Message ="Adding new Employee Error: "+ ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse> DeleteEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            using var context = _factory.CreateDbContext();
            context.Remove(employee);

            var result = await context.SaveChangesAsync();

            if (result == 1)
            {
                response.StatusCode = 200;
                response.Message = "Employee Removed succesfully";
            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Error accurred while Removing Employee";
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Removing Employee Error: " + ex.Message;
        }

        return response;
    }

    public async Task<GetEmployeeResponse> GetEmployeeFromId(int id)
    {
        var response = new GetEmployeeResponse();

        try
        {
            using var context = _factory.CreateDbContext();
            var employee = await context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == id.ToString());
            response.StatusCode = 200;
            response.Message = "Success";
            response.Employee = employee;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employees: " + ex.Message;
            response.Employee = null;
        }

        return response;
    }

    public async Task<GetEmployeeResponse> GetEmployeeFromUsername(string userName)
    {
        var response = new GetEmployeeResponse();

        try
        {
            using var context = _factory.CreateDbContext();
            var employee = await context.AspNetUsers.FirstOrDefaultAsync(x => x.UserName == userName);
            response.StatusCode = 200;
            response.Message = "Success";
            response.Employee = employee;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employees: " + ex.Message;
            response.Employee = null;
        }

        return response;
    }

    public async Task<GetAllEmployeesResponse> GetEmployees()
    {
        var response = new GetAllEmployeesResponse();
        try
        {
            using var context = _factory.CreateDbContext();
            var employees = await context.AspNetUsers.ToListAsync();
            response.StatusCode = 200;
            response.Message = "Success";
            response.Employees = employees;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employees: " + ex.Message;
            response.Employees = null;
        }

        return response;
    }

    public async Task<BaseResponse> ResetPassword(Employee employee, string password)
    {
        var getUser = _userManager.FindByIdAsync(employee.Id).Result;
        var token = _userManager.GeneratePasswordResetTokenAsync(getUser);
        var result = await _userManager.ResetPasswordAsync(getUser, token.Result,password);
        var response = new BaseResponse();
        if (result.Succeeded)
        {
            response.StatusCode = 200;
            response.Message = result.ToString();
            
        }
        else
        {
            token.Dispose();
            response.StatusCode = 500;
            response.Message = "error" + result.ToString();
        }

        return response;
    }

    public async Task<BaseResponse> UpdateEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            using var context = _factory.CreateDbContext();
            context.Update(employee);

            var result = await context.SaveChangesAsync();

            if (result == 1)
            {
                response.StatusCode = 200;
                response.Message = "Employee Updated succesfully";
            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Error accurred while Updating Employee";
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Updating Employee Error: " + ex.Message;
        }

        return response;
    }
}
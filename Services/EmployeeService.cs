using System.Security.Claims;
using BlazorUserManagerApp.Data;
using BlazorUserManagerApp.Models;
using BlazorUserManagerApp.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorUserManagerApp.Services;

public interface IEmployeeService
{
    Task<GetAllEmployeesResponse> GetEmployees();
    Task<BaseResponse> AddEmployee(Employee employee);
    Task<GetEmployeeResponse> GetEmployeeFromId(int id);
    Task<GetEmployeeResponse> GetEmployeeFromUsername(string userName);
    Task<BaseResponse> DeleteEmployee(Employee employee);
    Task<BaseResponse> UpdateEmployee(Employee employee);

    Task<BaseResponse> ResetPassword(Employee employee, string password);

    Task<int> GetUserCountWithRole(string role);
    Task<BaseResponse> SetEmployeeToRole(Employee employee, Roles role);
    Task<BaseResponse> DeleteEmployeeRole(Employee employee, string role);
    Task<BaseResponse> SetEmployeeRoles(Employee employee);
    Task<IList<string>> GetEmployeeRoles(Employee employee);
}

public class EmployeeService(
    IDbContextFactory<DataContext> factory,
    UserManager<IdentityUser> userManager
) : IEmployeeService
{
    private readonly IDbContextFactory<DataContext> _factory = factory;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<BaseResponse> AddEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            var roleCount = await GetUserCountWithRole(employee.Role.GetDisplayName());
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var identity = new Employee
            {
                UserName = employee.UserName,
                Id = employee.UserName.GetHashCode().ToString(),
                Email = employee.Email,
                Avatar = employee.Avatar,
                FullName = employee.FullName,
                PhoneNumber = employee.PhoneNumber,
                Role = employee.Role,
                Salary = employee.Salary,
                Type = employee.Type,
                ChangePaswword = true
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userManager.CreateAsync(identity, employee.PasswordHash);
#pragma warning restore CS8604 // Possible null reference argument.

            if (result.Succeeded)
            {
                var roleRes = await SetEmployeeToRole(identity, employee.Role);
                response.StatusCode = 200;
                response.Message = "Employee Added succesfully";
                if (roleRes.StatusCode == 200)
                {
                    response.Message += "\b Role Added to employee succesfully";
                }
                else
                {
                    response.Message +=
                        "\b Failed to add role to Employee Error: " + roleRes.Message;
                }
            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Error accurred while adding new Employee";
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message =
                "Adding new Employee Error: "
                + ex.Message
                + "\bInner Exception: "
                + ex.InnerException;
        }

        return response;
    }

    public async Task<BaseResponse> DeleteEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            var dbEmployee = await _userManager.FindByIdAsync(employee.Id);
            using var context = _factory.CreateDbContext();
            context.Remove(dbEmployee!);

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
        GetEmployeeResponse response = new(new());

        try
        {
            using var context = _factory.CreateDbContext();
            var employee = await context.AspNetUsers.FirstOrDefaultAsync(x =>
                x.Id == id.ToString()
            );
            if (employee != null)
            {
                response.StatusCode = 200;
                response.Message = "Success";
                response.Employee = employee;
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employees: " + ex.Message;
        }

        return response;
    }

    public async Task<GetEmployeeResponse> GetEmployeeFromUsername(string userName)
    {
        GetEmployeeResponse response = new(new());

        try
        {
            using var context = _factory.CreateDbContext();
            var employee = await context.AspNetUsers.FirstOrDefaultAsync(x =>
                x.UserName == userName
            );
            if (employee != null)
            {
                response.StatusCode = 200;
                response.Message = "Success";
                response.Employee = employee;
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Error retrieving employees: " + ex.Message;
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

    public async Task<int> GetUserCountWithRole(string role)
    {
        var employeeRoles = await _userManager.GetUsersInRoleAsync(role);
        return employeeRoles.Count;
    }

    public async Task<BaseResponse> ResetPassword(Employee employee, string password)
    {
        var getUser = await _userManager.FindByIdAsync(employee.Id);
        var response = new BaseResponse();
        if (getUser != null)
        {
            var token = _userManager.GeneratePasswordResetTokenAsync(getUser);
            var result = await _userManager.ResetPasswordAsync(getUser, token.Result, password);

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
        }
        else
        {
            response.StatusCode = 500;
            response.Message = "User Not Found";
        }
        return response;
    }

    public async Task<IList<string>> GetEmployeeRoles(Employee employee)
    {
        var dbEmployee = await _userManager.FindByIdAsync(employee.Id);
        var rolesRes = await _userManager.GetRolesAsync(dbEmployee!);
        return rolesRes;
    }

    public async Task<BaseResponse> SetEmployeeToRole(Employee employee, Roles role)
    {
        var response = new BaseResponse();
        try
        {
            var dbEmployee = await _userManager.FindByIdAsync(employee.Id);
            if (dbEmployee != null)
            {
                var roleAddedResult = await _userManager.AddToRoleAsync(
                    dbEmployee,
                    role.GetDisplayName()
                );
                if (roleAddedResult.Succeeded)
                {
                    var claimRes = await _userManager.AddClaimAsync(
                        dbEmployee,
                        new Claim(role.GetDisplayName(), role.GetDisplayName())
                    );
                    await SetEmployeeRoles((Employee)dbEmployee);
                    response.StatusCode = 200;
                    response.Message = "Role Added Successfully";
                }
                else
                {
                    response.StatusCode = 500;
                    response.Message = "Error acured while adding Role: " + roleAddedResult.Errors;
                }
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message =
                "Error acured while adding Role: "
                + ex.Message
                + "\b innerException: "
                + ex.InnerException;
        }
        return response;
    }

    public async Task<BaseResponse> UpdateEmployee(Employee employee)
    {
        var response = new BaseResponse();
        try
        {
            //using var context = _factory.CreateDbContext();
            if (await _userManager.FindByIdAsync(employee.Id) is Employee dbEmployee)
            { //var newEmployee = MergeObjects(dbEmployee, employee);
                dbEmployee.FullName = employee.FullName;
                dbEmployee.UserName = employee.UserName;
                dbEmployee.Email = employee.Email;
                dbEmployee.Salary = employee.Salary;
                dbEmployee.Type = employee.Type;
                dbEmployee.Avatar = employee.Avatar;
                dbEmployee.Role = employee.Role;
                dbEmployee.Active = employee.Active;
                dbEmployee.LastPasswordChange = employee.LastPasswordChange;
                dbEmployee.ChangePaswword = employee.ChangePaswword;

                //context.Update(employee);
                var updateRes = await _userManager.UpdateAsync(dbEmployee);

                //var result = await context.SaveChangesAsync();

                if (updateRes.Succeeded)
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
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = "Updating Employee Error: " + ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse> SetEmployeeRoles(Employee employee)
    {
        BaseResponse response = new();
        var employeeRoles = await _userManager.GetRolesAsync(employee);
        if (employeeRoles.Count > 0)
        {
            employee.Role = (Roles)Enum.Parse(typeof(Roles), employeeRoles.ToArray()[0]);
        }
        else
        {
            employee.Role = Roles.WhithoutRole;
        }
        var updateRes = await UpdateEmployee(employee);
        if (updateRes.StatusCode == 200)
        {
            response.StatusCode = 200;
            response.Message = "User Role Set Successfully";
        }
        else
        {
            response.StatusCode = 500;
            response.Message = "Error in setting user Role" + updateRes.Message;
        }
        return response;
    }

    public async Task<BaseResponse> DeleteEmployeeRole(Employee employee, string role)
    {
        var response = new BaseResponse();
        try
        {
            var dbEmployee = await _userManager.FindByIdAsync(employee.Id);
            if (dbEmployee != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(dbEmployee, role);
                if (result.Succeeded && dbEmployee != null)
                {
                    var ClaimRes = await _userManager.RemoveClaimAsync(
                        dbEmployee,
                        new Claim(role, role)
                    );
                    await SetEmployeeRoles((Employee)dbEmployee);
                    response.StatusCode = 200;
                    response.Message = "Role Removed";
                }
                else
                {
                    response.StatusCode = 500;
                    response.Message =
                        "error Removing Role" + result.Errors.ToArray()[0].Description;
                }
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message =
                "error Removing Role:" + ex.Message + "\bInnerException:" + ex.InnerException;
        }
        return response;
    }
}

using BlazorUserManagerApp.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorUserManagerApp.Data;

public class DataContext : IdentityDbContext
{
    public DbSet<Employee> AspNetUsers { get; set; }
    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(getRoles());
        modelBuilder.Entity<Employee>().HasData(GetEmployees());
        
        
    }

    private IdentityRole[] getRoles()
    {
        //var roles = Enum.GetValues(typeof(Roles));
        var roles = System.Enum.GetNames(typeof(Roles));
        return roles.Select((x, i) => new IdentityRole() { Id = i.ToString(), Name = x.ToString(), NormalizedName = x.ToString().ToUpper() }).ToArray();
    }

    private List<Employee> GetEmployees()
    {
        var employees = new List<Employee>();
        var faker = new Faker("en"); // Specify the language for name generation

        for (int i = 1; i <= 50; i++)
        {
            string userName = faker.Internet.UserName();
            var employee = new Employee
            {
                UserName = userName,
                Id = userName.GetHashCode().ToString(),
                Avatar = faker.Internet.Avatar(),
                FullName = faker.Name.FullName(),
                Salary = GetRandomSalary(),
                Type = GetRandomEmployeeType(),
                Role = GetRandomRole(),
                Email = faker.Internet.Email(),
                PhoneNumber = faker.Phone.PhoneNumber(),
                Active = true,
                ChangePaswword = true,
                

            };
            

            employees.Add(employee);
        }

        return employees;
    }

    private decimal GetRandomSalary()
    {
        var random = new Random();
        decimal salary = random.Next(30000, 100000); // Generates a random salary between $30,000 and $100,000
        return salary;
    }

    // Method to get a random employee type
    private EmployeeType GetRandomEmployeeType()
    {
        var random = new Random();
        var types = Enum.GetValues(typeof(EmployeeType));
        return (EmployeeType)types.GetValue(random.Next(types.Length));
    }

    // Method to get a random position
    private Roles GetRandomRole()
    {
        var random = new Random();
        var roles = Enum.GetValues(typeof(Roles));
        return (Roles)roles.GetValue(random.Next(roles.Length));
    }

    private Roles GetRole(int index)
    {
        var random = new Random();
        var roles = Enum.GetValues(typeof(Roles));
        return (Roles)roles.GetValue(index);
    }


}
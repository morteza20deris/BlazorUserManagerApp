﻿using BlazorUserManagerApp.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorUserManagerApp.Data;

public class DataContext : IdentityDbContext
{
    public DbSet<Employee> AspNetUsers { get; set; }

    public DataContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(GetRoles());
        modelBuilder.Entity<Employee>().HasData(GetEmployees());
    }

    private static IdentityRole[] GetRoles()
    {
        //var roles = Enum.GetValues(typeof(Roles));
        var roles = System.Enum.GetNames(typeof(Roles));
        return roles
            .Select(
                (x, i) =>
                    new IdentityRole()
                    {
                        Id = i.ToString(),
                        Name = x.ToString(),
                        NormalizedName = x.ToString().ToUpper()
                    }
            )
            .ToArray();
    }

    private static List<Employee> GetEmployees()
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
                EmailConfirmed = true
            };

            employees.Add(employee);
        }

        return employees;
    }

    private static decimal GetRandomSalary()
    {
        var random = new Random();
        decimal salary = random.Next(30000, 100000); // Generates a random salary between $30,000 and $100,000
        return salary;
    }

    // Method to get a random employee type
    private static EmployeeType GetRandomEmployeeType()
    {
        var random = new Random();
        var types = Enum.GetValues(typeof(EmployeeType));
#pragma warning disable CS8605 // Unboxing a possibly null value.
        return (EmployeeType)types.GetValue(random.Next(types.Length));
#pragma warning restore CS8605 // Unboxing a possibly null value.
    }

    // Method to get a random position
    private static Roles GetRandomRole()
    {
        return Models.Roles.WhithoutRole;
    }

    //    private Roles GetRole(int index)
    //    {
    //        var random = new Random();
    //        var roles = Enum.GetValues(typeof(Roles));
    //#pragma warning disable CS8605 // Unboxing a possibly null value.
    //        return (Roles)roles.GetValue(index);
    //    }
}

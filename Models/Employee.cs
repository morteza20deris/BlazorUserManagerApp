using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlazorUserManagerApp.Models;

public class Employee : IdentityUser
{
    [Required]
    public bool Active { get; set; }
    
    [Required]
    public string FullName { get; set; }
    public string Avatar { get; set; }
    [Required]
    public decimal Salary { get; set; }
    [Required]
    public EmployeeType Type { get; set; }
    [Required]
    public Roles Role { get; set; }

    public bool ChangePaswword { get; set; }
    //[Required(ErrorMessage ="Password is required")]
    //[DataType(DataType.Password)]
    //[MinLength(8,ErrorMessage ="Password must be at least 8 characters")]
    //[MaxLength(16, ErrorMessage = "Password must be at most 16 characters")]
    //[RegularExpression("^(?=.*[A-Za-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}",ErrorMessage ="Invalid Password")]
    //public override string PasswordHash { get; set; }
    
    



}

public enum EmployeeType
{
    [Display(Name = "Freelance")]
    Freelance,

    [Display(Name = "Casual")]
    Casual,

    [Display(Name = "Part Time")]
    PartTime,

    [Display(Name = "Full Time")]
    FullTime
}



public enum Roles
{
    [Display(Name = "CEO")]
    CEO,

    [Display(Name = "CFO")]
    CFO,

    [Display(Name = "CTO")]
    CTO,

    [Display(Name = "Accountant")]
    Accountant,

    [Display(Name = "HR Manager")]
    HRManager,

    [Display(Name = "Marketing Manager")]
    MarketingManager,

    [Display(Name = "Sales Manager")]
    SalesManager,

    [Display(Name = "Software Engineer")]
    SoftwareEngineer,

    [Display(Name = "Data Analyst")]
    DataAnalyst,

    [Display(Name = "Customer Support")]
    CustomerSupport,

    [Display(Name = "New")]
    New
}

public static class EnumExtensions
{
    
    public static string? GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
          .GetMember(enumValue.ToString())
          .First()
          .GetCustomAttribute<DisplayAttribute>()?
          .GetName();
    }
}
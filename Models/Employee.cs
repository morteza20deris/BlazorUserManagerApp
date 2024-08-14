﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlazorUserManagerApp.Models;

public class Employee : IdentityUser
{
    [Required]
    public bool Active { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(32)]
    public string FullName { get; set; }
    [DataType(DataType.Url)]
    public string Avatar { get; set; }
    [Required]
    public decimal Salary { get; set; }
    [Required]
    public EmployeeType Type { get; set; }
    [Required]
    public Roles Role { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastPasswordChange { get; set; }

    public bool ChangePaswword { get; set; }
    





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
    [Display(Name = "Admin")]
    Admin,

    [Display(Name = "Supervisor")]
    Supervisor,

    [Display(Name = "Operator")]
    Operator,
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
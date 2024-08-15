using BlazorUserManagerApp.Models;
using BlazorUserManagerApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BlazorUserManagerApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [MinLength(3)]
            [MaxLength(32)]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
            [MaxLength(16, ErrorMessage = "Password must be at most 16 characters")]
            [RegularExpression("^(?=.*[A-Za-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}", ErrorMessage = "Invalid Password")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
            [MaxLength(16, ErrorMessage = "Password must be at most 16 characters")]
            [RegularExpression("^(?=.*[A-Za-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}", ErrorMessage = "Invalid Password")]
            public string ConfirmPassword { get; set; }
            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && Input.Password == Input.ConfirmPassword)
            {
                
                var identity = new Employee { UserName=Input.UserName,Email=Input.EmailAddress,Id= Input.UserName.GetHashCode().ToString() ,Active=true,EmailConfirmed=true}; 
                
                var result = await _userManager.CreateAsync(identity,Input.Password);
                
                if (result.Succeeded)
                {
                    var user =await _userManager.FindByNameAsync(Input.UserName);
                    var roleRes = await _userManager.AddToRoleAsync(user, "Admin");
                    var claimRes = await _userManager.AddClaimAsync(user, new Claim("Admin", "Admin"));
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect("~/");
                }
            }
            
            return Page();
            
        }
    }
}

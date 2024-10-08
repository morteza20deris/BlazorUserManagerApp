using System.ComponentModel.DataAnnotations;
using BlazorUserManagerApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorUserManagerApp.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public inputModel? Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && Input != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    Input.UserName,
                    Input.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );
                if (result.Succeeded)
                {
                    return LocalRedirect("~/");
                }
                else
                {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    Employee user = await _userManager.FindByNameAsync(Input.UserName) as Employee;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (user != null && user.AccessFailedCount < 3)
                    {
                        await _userManager.AccessFailedAsync(user);
                    }
                    else if (user != null && user.AccessFailedCount >= 3)
                    {
                        user.Active = false;
                        await _userManager.UpdateAsync(user);
                    }
                }
            }

            return Page();
        }

        public class inputModel
        {
            [Required]
            [MinLength(3)]
            public string UserName { get; set; } = string.Empty;

            [DataType(DataType.Password)]
            [Required]
            [MinLength(8)]
            [MaxLength(16)]
            public string Password { get; set; } = string.Empty;
        }
    }
}

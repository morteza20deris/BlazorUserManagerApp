using BlazorUserManagerApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BlazorUserManagerApp.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [BindProperty]
        public inputModel? Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect("~/");
                }
                else
                {
                    Employee user =await _userManager.FindByNameAsync(Input.UserName) as Employee;
                    if (user != null && user.AccessFailedCount < 3)
                    {
                        await _userManager.AccessFailedAsync(user);

                    }
                    else if (user != null && user.AccessFailedCount >=3) {
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
            public string UserName { get; set; }
            [DataType(DataType.Password)]
            [Required]
            [MinLength(8)]
            [MaxLength(16)]
            public string Password { get; set; }
        }
    }
}

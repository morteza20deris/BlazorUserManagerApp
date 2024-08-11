using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BlazorUserManagerApp.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;

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
            }

            return Page();
        }

        public class inputModel
        {
            [Required]
            public string UserName { get; set; }
            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }
        }
    }
}

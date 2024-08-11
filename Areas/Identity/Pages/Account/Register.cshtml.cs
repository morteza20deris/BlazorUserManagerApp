using BlazorUserManagerApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BlazorUserManagerApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            
        }

        [BindProperty]
        public inputModel Input { get; set; }

        public class inputModel
        {
            [Required]
            public string UserName { get; set; }
            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identity = new Employee { UserName = Input.UserName,Id=Input.UserName.GetHashCode().ToString() };
                var result = await _userManager.CreateAsync(identity, Input.Password);
                
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect("~/");
                }
            }
            
            return Page();
            
        }
    }
}

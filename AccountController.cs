using IDBeAcademy.Data;
using IDBeAcademy.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IDBeAcademy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountUser user, string? returnUrl)
        {
            if (ModelState.IsValid)  // Ensure model validation before continuing
            {
                var appUser = new AppUser()
                {
                    UserName = user.UserEmail,
                    Email = user.UserEmail
                };

                var result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser, false);

                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Course");
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                }
            }

           
            return View(user);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginUser user, string? returnUrl)
        {
            if (ModelState.IsValid)  
            {
                var appUser = await userManager.FindByEmailAsync(user.UserEmail);

                if (appUser == null)
                {
                    ModelState.AddModelError("", "Invalid Credential! Try Again Later....");
                    return View(user);
                }

                var result = await signInManager.CheckPasswordSignInAsync(appUser, user.Password, false);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser, user.RememberMe);

                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Course");
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }

            return View(user);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogOut()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return View(); 
            }

           
            return RedirectToAction("Index", "Course");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut(string? returnUrl)
        {
            await signInManager.SignOutAsync(); // Sign the user out

            
            TempData["LogoutMessage"] = "Thanks for your contribution! You have successfully logged out.";

            
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction("LogOutConfirmation");  
            }

            return Redirect(returnUrl); 
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogOutConfirmation()
        {
           
            return View();
        }


    }
}

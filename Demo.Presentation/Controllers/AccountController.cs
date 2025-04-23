using Demo.DataAccess.Models.IdentityModels;
using Demo.Presentation.Helper;
using Demo.Presentation.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager ,
                                   SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                // Mapping from RegisterViewModel to ApplicationUser
                var user = new ApplicationUser()
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.UserName,
                };
                var result = _userManager.CreateAsync(user , registerViewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(LogIn));
                else 
                {
                    foreach(var  error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
            }
            return View(registerViewModel);
        }

        #endregion


        #region LogIn

        [HttpGet]
        public IActionResult LogIn() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInViewModel) 
        {
            if (ModelState.IsValid) 
            {
                var user = _userManager.FindByEmailAsync(logInViewModel.Email).Result;
                if (user is null) 
                    ModelState.AddModelError(string.Empty, "Invalid LogIn");
                else
                {
                    var flag = _userManager.CheckPasswordAsync(user, logInViewModel.Password).Result;
                    if (flag)
                    {
                        // Generate the token
                        var res = _signInManager.PasswordSignInAsync(user, logInViewModel.Password , false , false).Result;
                        if(res.Succeeded) 
                            return RedirectToAction(actionName: "Index", controllerName: "Home");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Invalid Password");
				}
            }
            return View(logInViewModel);
        }

        #endregion


        #region LogOut
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid) 
            {
				var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {
                    // generate token
                    var token =_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    //create url
                    var url = Url.Action(action: "ResetPassword" , controller: "Account" , values: new {email=viewModel.Email , token} , protocol: Request.Scheme);
                    //create email
                    var email = new Helper.Email() 
                    {
                        To= viewModel.Email,
                        Subject="Reset Password",
                        Body=url
					};
                    //send email
                    bool isMailSent = EmailSettings.SendEmail(email);
                    if (isMailSent)
                        return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                    ModelState.AddModelError(string.Empty, "Email not found");
			}
			return View(nameof(ForgetPassword));
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid) 
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null)
                    return BadRequest();
                else
                {
                    var user = _userManager.FindByEmailAsync(email).Result;
                    if (user is not null)
                    {
                        var res = _userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;
                        if (res.Succeeded) return RedirectToAction(nameof(LogIn));
                    }
                    else ModelState.AddModelError(string.Empty, "Some thing went wrong");
                }
            }
            return View(viewModel);
        }
        #endregion
    }
}

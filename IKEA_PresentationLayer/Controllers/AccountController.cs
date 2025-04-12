using Ikea_Data_Acsess_Layer.Models.Identity;
using IKEA_PresentationLayer.ViewModel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Threading.Tasks;

namespace IKEA_PresentationLayer.Controllers


{

    public class AccountController : Controller
    {



        #region Services

        private readonly UserManager<AplicationUser> userManager;
        private readonly SignInManager<AplicationUser> signInManager;

        public AccountController(UserManager<AplicationUser> userManager,SignInManager<AplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
         

        #endregion

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
              return BadRequest();

            var User= await userManager.FindByNameAsync(signUpViewModel.UserName);

            if (User != null)
            {
                ModelState.AddModelError(signUpViewModel.UserName, "This UserName is already taken");
                return View(signUpViewModel);
            }


            User = new AplicationUser()
            {
                FirstName = signUpViewModel.FirstName,
                LastName = signUpViewModel.LastName,
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                IsAgreed = signUpViewModel.IsAgree
            }; 
           var Result= await userManager.CreateAsync(User, signUpViewModel.Password);

            if (Result.Succeeded)
                return RedirectToAction(nameof(LogIn));


            foreach (var error in Result.Errors)
                ModelState.AddModelError(nameof(signUpViewModel.UserName), "This UserName is already taken");




            return View(signUpViewModel);

        }





        #endregion

        #region LogIn

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();



            var User = await userManager.FindByEmailAsync(logInViewModel.Email);
            if (User is not null)
            {
               var result1= await signInManager.PasswordSignInAsync(User,logInViewModel.Password, logInViewModel.RememberMe, true);
                if (result1.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed");

                if (result1.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Your Account Is Locked Out");

                if (result1.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");


            }
            ModelState.AddModelError(string.Empty, "Invalid LogIn");
            return View(logInViewModel);





            var result = await userManager.CheckPasswordAsync(User, logInViewModel.Password);
            if (!result)
            {
                return View(logInViewModel);
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        #endregion



        #region SignOut


        public async Task<ActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));

        }
        #endregion
    }
}

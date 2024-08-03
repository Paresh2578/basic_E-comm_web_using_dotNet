using bulkyApp.Data.Interface;
using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace bulkyApp.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly UserInterface _IUser;

        public AuthenticationController(UserInterface IUser)
        {
            _IUser = IUser;
        }

        public IActionResult LogIn()
        {
            
            return View(new UserModel());
        }

        [HttpPost]
        public IActionResult LogIn(String email, String password)
        {

            if (ModelState.IsValid)
            {
                ApiResponseModel response = _IUser.LogInUser(email.Trim()??"", password.Trim() ?? "").Value as ApiResponseModel;
                if (response.success)
                {
                    TempData["success"] = response.msg;
                    return RedirectToAction("Index","Home", new {area = "Customer" });
                }
                else
                {
                    TempData["InValid"] = response.msg;
                }
            }

            return View(new UserModel { Email = email , Password = password});
        }

        [Route("/Authantication/Authentication/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		[Route("/Authantication/Authentication/Register")]
		public IActionResult Register(UserModel user)
        {
            Console.WriteLine("demo");
            if (ModelState.IsValid)
            {
                ApiResponseModel response = _IUser.RegesterUser(user).Value as ApiResponseModel;

                if (response.success)
                {
                    TempData["success"] = response.msg;
                    return RedirectToAction("LogIn");
                }
                else
                    TempData["error"] = response.msg;

            }
            return View(user);
        }

		[HttpPost]
        public IActionResult ForgetPass()
        {
            return View();
        }

        [HttpDelete]
        [Route("/logOut")]
        public IActionResult LogOut()
        {
            JsonResult result =_IUser.LogOutUser();

            return result;

        }
	}
}

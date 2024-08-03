using bulkyApp.Data.Interface;
using bulkyApp.Models;
using bulkyApp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace bulkyApp.Data.Repository
{
	public class UserRepo : UserInterface
	{
		private readonly ApplicationDbContext _db;
		private readonly IHttpContextAccessor _IHttpContextAccessor;

		public UserRepo(ApplicationDbContext db , IHttpContextAccessor IHttpContextAccessor)
        {
			_db = db;
			_IHttpContextAccessor = IHttpContextAccessor;

        }
		JsonResult UserInterface.LogInUser(string email, string password)
		{
			try
			{
				UserModel? user = _db.users.FirstOrDefault(u => u.Email == email && u.Password == password);

				if (user == null)
					return new JsonResult(new ApiResponseModel { success = false, msg = "Invalid email or Password" });
				else
				{
					SessionAccess.setLogInAndRoleDataAndUserID(user.Role!,user.UserID.ToString(), _IHttpContextAccessor.HttpContext!);
					SessionAccess.setCardLength(_IHttpContextAccessor.HttpContext! , _db.cards.Count());
					return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully  Log In", singleData = user });
				}
			}
			catch(Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Fail to Log In" });
			}
		}

		JsonResult UserInterface.LogOutUser()
		{
			SessionAccess.clearSeesion(_IHttpContextAccessor.HttpContext!);

			return new JsonResult(new ApiResponseModel { success = true, msg = "LogOut sussfully" });
		}

		JsonResult UserInterface.RegesterUser(UserModel user)
		{
			try{
				UserModel? exitUser = _db.users.FirstOrDefault(u=>u.Email == user.Email);
				if(exitUser == null)
				{
					_db.users.Add(user);
					_db.SaveChanges();
					return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully  Register" });
				}
				else
				{
					return new JsonResult(new ApiResponseModel { success = false, msg = "Email is alredy used" });
				}
				 
			}
			catch (Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Fail to Register" });
			}
		}
	}
}

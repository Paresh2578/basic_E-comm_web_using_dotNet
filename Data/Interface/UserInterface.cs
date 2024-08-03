using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyApp.Data.Interface
{
	public interface UserInterface
	{
		public JsonResult RegesterUser(UserModel user);
		public JsonResult LogInUser(string email , string password);
		public JsonResult LogOutUser();

	}
}

using bulkyApp.CV;
using bulkyApp.Data;
using bulkyApp.Data.Interface;
using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bulkyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
	[IsAdmin]
    public class CategoryController : Controller
    {


		private readonly CategoryInterface _ICategory;

		public CategoryController(CategoryInterface ICategory)
		{
			_ICategory = ICategory;
		}

        public async Task<IActionResult> CategoryList()
        {
			List<CategoryModel> categorys =  _ICategory.getCategoryList();

            return View(categorys);
        }
        public async Task<IActionResult> AddEditCategory(int? categoryID)
        {
			if(categoryID != null && categoryID != 0)

			{
				return View(_ICategory.GetCategoryById(categoryID??0));
			}
			else
			{
			return View(new CategoryModel());
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddEditCategory(CategoryModel category)
		{
			ModelState.Remove("CategoryId");

			if (ModelState.IsValid)
			{
				if (category.CategoryId == 0)
				{
					//insert
					_ICategory.addCategory(category);
					TempData["success"] = "Category add successfully";
				}
				else
				{
					// Update
					_ICategory.EditCategory(category);
                    TempData["success"] = "Category edit successfully";

				}
				return RedirectToAction("CategoryList");
			}
			return View();
		}


		[HttpDelete]
		[Route("/api/category/{categoryId}")]
		public  IActionResult deleteCategory(int categoryId)
		{
            JsonResult jsonResult =  _ICategory.DeteleCategory(categoryId) as JsonResult;
            ApiResponseModel result = (ApiResponseModel)jsonResult.Value!;


			if (result.success)
			{
				TempData["success"] = "successfully deleted category";
			}
			else
			{
				TempData["error"] = "Faild deleted category";
			}
				return jsonResult;
		}

	}
}

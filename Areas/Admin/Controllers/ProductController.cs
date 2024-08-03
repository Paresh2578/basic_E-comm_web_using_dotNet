using Azure;
using bulkyApp.CV;
using bulkyApp.Data;
using bulkyApp.Data.Interface;
using bulkyApp.Models;
using bulkyApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace bulkyApp.Areas.Admin.Controllers
{

    [Area("Admin")]
	[IsAdmin]
	public class ProductController : Controller
	{

		private readonly ProductInterface _IProduct;
		private readonly CategoryInterface _ICategory;
		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(ProductInterface IProduct, CategoryInterface ICategory,ApplicationDbContext db , IWebHostEnvironment webHostEnvironment)
		{
			_IProduct = IProduct;
			_ICategory = ICategory;
			_db = db;
			_webHostEnvironment = webHostEnvironment;
        }
		public async Task<IActionResult> ProductList()
		{
			ApiResponseModel response = _IProduct.ProductList().Value as ApiResponseModel;

			List<ProductModel> products = [];

			if (response!.success)
			{
				return View(response.listData.Cast<ProductModel>().ToList());
			}
			else
			{
				TempData["error"] = response.msg;
				return View(products);
			}
		}

		public async Task<IActionResult> productCreateOrEdit(int? productID)
		{
			await setCategorys();

			if (productID ==null || productID == 0)
			{
				//create
				return View(new ProductModel());
			}
			else
			{
				//edit

				ApiResponseModel response = _IProduct.ProductGetById(productID ?? 0).Value as ApiResponseModel;


				if (response!.success)
				{
					TempData["productImageURL"] = response.singleData.ImageUrl;
					return View(response.singleData);
				}
				else
				{
					TempData["error"] = response.msg;
					return RedirectToAction("ProductList");
				}
			}
		}

		[HttpPost]
		public async Task<IActionResult> productCreateOrEdit(ProductModel product  , IFormFile file)
		{
            //\Images/Product\86559aaa-5357-4671-bb31-8f4dbe727a5c.png
           // ModelState.Remove("productImage");

			//remove file modelState when edit file
			if(product.ProductId != 0 && file == null)
			{
				ModelState.Remove("file");
			}

			if (ModelState.IsValid) { 
				ApiResponseModel response = new ApiResponseModel { };

                if (file == null && product.ProductId == 0 )
                {
                    ModelState.AddModelError("productImage", "Please upload a valid image.");
					//setCategorys();
                    return RedirectToAction("productCreateOrEdit" , product.ProductId); // Return the view with the model state errors
				}
				if(file != null)
				{
                 product.ImageUrl = BasicFileOPration.createFile(file!, getWwwRootPath()).ToString();
				}else if (TempData["productImageURL"] != null)
				{
					product.ImageUrl = (string)TempData["productImageURL"]!;
				}
				
				
                if (product.ProductId == 0)
				{
					//create
					response = _IProduct.createProduct(product).Value as ApiResponseModel;
				}
			else
			{
				//edit
				response = _IProduct.EditProduct(product).Value as ApiResponseModel;
			}

				if (response.success)
				{
					TempData["success"] = response.msg;
					return RedirectToAction("ProductList");
				}
				else
				{
					TempData["error"] = response.msg;
				await	setCategorys();
                    return View(product);
				}
			}
			else
			{
				/*etCategorys();*/
                return RedirectToAction("productCreateOrEdit", product.ProductId); 
            }
		}

		[HttpDelete]
		[Route("/api/product/detele/{productID}")]
		public async Task<IActionResult> productDelete(int? productID)
		{
			JsonResult jsonResult = _IProduct.DeleteProduct(productID ?? 0,getWwwRootPath());
			ApiResponseModel response = jsonResult.Value as ApiResponseModel;


			if (response.success)
			{
                TempData["success"] = response.msg;
			}
			else
			{
				TempData["error"] = response.msg;
			}
			return jsonResult;
		}


		//my Function
		public string getWwwRootPath()
		{
			return _webHostEnvironment.WebRootPath;
		}

		public async Task setCategorys()
		{
            ViewBag.categorieslist = await _db.categories.Select(c => new SelectListItem() { Value = c.CategoryId.ToString(), Text = c.CategoryName }).ToListAsync();
        }
	}
}


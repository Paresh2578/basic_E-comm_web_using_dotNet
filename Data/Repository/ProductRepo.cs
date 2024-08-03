using bulkyApp.Data.Interface;
using bulkyApp.Models;
using bulkyApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace bulkyApp.Data.Repository
{
    public class ProductRepo : ProductInterface
	{
		private readonly ApplicationDbContext _db;

		public ProductRepo(ApplicationDbContext db)
		{
			_db = db;
		}
		JsonResult ProductInterface.createProduct(ProductModel product)
		{
			Console.WriteLine(product);
			try
			{
				_db.products.Add(product);
				_db.SaveChanges();
				return new JsonResult(new ApiResponseModel {success = true , msg="Successfully create product" });
			}
			catch (Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Faild to create product" });
			}
		}

		JsonResult ProductInterface.DeleteProduct(int productID, String wwwRootPath)
		{
			try
			{
				ProductModel? product = _db.products.Find(productID);
				
				if(product == null)
				{
					return new JsonResult(new ApiResponseModel { success = false, msg = "Product Id is Invalid" });
				}
				else
				{
					string imgURL = wwwRootPath + product.ImageUrl;
					_db.products.Remove(product);
					BasicFileOPration.deleteFile(imgURL);
                    _db.SaveChanges();
					return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully delete product" });
				}
			}
			catch (Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Faild to detele product" });
			}
		}

		JsonResult ProductInterface.EditProduct(ProductModel product)
		{
			try
			{
				_db.products.Update(product);
				_db.SaveChanges();
				return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully Update product" });
			}
			catch (Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Faild to Update product" });
			}
		}

		JsonResult ProductInterface.ProductGetById(int productID)
		{
			try
			{
				ProductModel? product = _db.products.Include(e => e.category).FirstOrDefault(p => p.ProductId == productID);
				 
				if(product != null)
				{
					return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully fetch product" ,singleData=product});
				}
				else
				{
					return new JsonResult(new ApiResponseModel { success = false, msg = "InValid product Id" });
				}

			}
			catch (Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Faild to fetch product" });
			}
		}

		JsonResult ProductInterface.ProductList()
		{
			try
			{
				List<ProductModel> products = _db.products.Include(e=>e.category).ToList();
				return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully fetch products" , listData= products != null ? products.Cast<dynamic>().ToList() :  new List<dynamic>() });
			}
			catch (Exception e)
			{
				return new JsonResult(new ApiResponseModel { success = false, msg = "Faild to fetch product" });
			}
		}
	}
}

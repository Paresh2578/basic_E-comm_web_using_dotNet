using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyApp.Data.Interface
{
    public interface ProductInterface
	{
		public JsonResult createProduct(ProductModel product);

		public JsonResult EditProduct(ProductModel product);

		public JsonResult DeleteProduct(int productID, String wwwRootPath);
		public JsonResult ProductList();
		public JsonResult ProductGetById(int productID);
	}
}

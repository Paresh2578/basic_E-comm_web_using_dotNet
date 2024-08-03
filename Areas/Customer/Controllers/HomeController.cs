using bulkyApp.Data.Interface;
using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Security.AccessControl;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bulkyApp.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ProductInterface _IProduct;

        public HomeController(ProductInterface IProduct)
        {
            _IProduct = IProduct;
        }
        public IActionResult Index()
        {
            List<ProductModel> products = [];

            ApiResponseModel response = _IProduct.ProductList().Value as ApiResponseModel;

            if (response.success)
            {
                return View(response.listData.Cast<ProductModel>().ToList());
            }
            else
            {
                return View(products);
            }
        }

        public IActionResult Details(int productId)
        {
              ApiResponseModel response  = _IProduct.ProductGetById(productId).Value as ApiResponseModel;

                if(response.success == true)
                    return View(response.singleData);
                else
                    return RedirectToAction("Index");
        }


     
    }
}

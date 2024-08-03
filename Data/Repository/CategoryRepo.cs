using bulkyApp.Data.Interface;
using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyApp.Data.Repository
{
    public class CategoryRepo : CategoryInterface
    {

        private readonly ApplicationDbContext _db;
        public CategoryRepo(ApplicationDbContext db)
        {
            _db = db;
        }

         void CategoryInterface.addCategory(CategoryModel category)
        {
            Console.WriteLine(category);
            _db.categories.AddAsync(category);
            _db.SaveChangesAsync();
        }

        JsonResult CategoryInterface.DeteleCategory(int categoryID)
        {

            CategoryModel? category = _db.categories.Find(categoryID);

            if (category != null)
            {
                //delete
                _db.categories.Remove(category);
                 _db.SaveChangesAsync();
                return new JsonResult(new ApiResponseModel { success = true, msg = "delete successfully" });
            }
            else
            {
                return new JsonResult(new ApiResponseModel { success = false, msg = "delete Faild" });
            }
        }

          void CategoryInterface.EditCategory(CategoryModel category)
        {
             _db.categories.Update(category);
           _db.SaveChangesAsync();
        }

        List<CategoryModel> CategoryInterface.getCategoryList()
        {
            List<CategoryModel> categories =  _db.categories.ToList();

            return categories;
        }

        CategoryModel? CategoryInterface.GetCategoryById(int categoryID)
        {
            CategoryModel? category = _db.categories.Find(categoryID);
                return category;
        }

    }
}

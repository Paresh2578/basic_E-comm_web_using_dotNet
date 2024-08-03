using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyApp.Data.Interface
{
    public interface CategoryInterface
    {
        List<CategoryModel> getCategoryList();

        void addCategory(CategoryModel category);

        void EditCategory(CategoryModel category);

        JsonResult DeteleCategory(int categoryID);

        CategoryModel? GetCategoryById(int categoryID);
    }
}

using Microsoft.AspNetCore.Mvc;

namespace bulkyApp.Utils
{
    public class BasicFileOPration
    {
        public static string createFile(IFormFile? file , string wwwRootPath)
        {
            try{
                String fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                String filePath = Path.Combine(wwwRootPath, @"Images\Product", fileName);

                if(!System.IO.File.Exists(filePath))

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                return filePath.Substring(wwwRootPath.Length);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static void deleteFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
               
            }catch(Exception e)
            {
            }
        }
    }
}

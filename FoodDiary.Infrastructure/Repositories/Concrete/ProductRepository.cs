using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Infrastructure.Repositories.Common;

namespace FoodDiary.Infrastructure.Repositories.Concrete
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository()
        {
            filePath = @"D:\Work\products.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }
            else
            {
                items = GetAll().ToList();
            }
        }

        public void Edit(Product productToEdit)
        {
            var existingProduct = items.FirstOrDefault(i => i.Id == productToEdit.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = productToEdit.Name;
                existingProduct.Calories = productToEdit.Calories;
                existingProduct.Protein = productToEdit.Protein;
                existingProduct.Carbs = productToEdit.Carbs;
                existingProduct.Fat = productToEdit.Fat;
                SaveToFile();
            }
        }
    }
}

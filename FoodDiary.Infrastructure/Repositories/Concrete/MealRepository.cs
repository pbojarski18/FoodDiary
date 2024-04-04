using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Infrastructure.Repositories.Common;

namespace FoodDiary.Infrastructure.Repositories.Concrete
{
    public class MealRepository : BaseRepository<Meal>, IMealRepository
    {
        public MealRepository()
        {
            items = new List<Meal>();
            filePath = @"D:\Work\meals.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }
            else
            {
                items = GetAll().ToList();
            }
        }

        public void Edit(Meal editedMeal)
        {
            var existingMeal = items.FirstOrDefault(i => i.Id == editedMeal.Id);
            if (existingMeal != null)
            {                
                existingMeal.Products = editedMeal.Products;
                SaveToFile();
            }
        }
    }
}
    




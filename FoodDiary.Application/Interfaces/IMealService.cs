using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Models;

namespace FoodDiary.Application.Interfaces
{
    public interface IMealService
    {
        public int AddNew(Meal newMeal);

        public List<Meal> GetAll();

        public Meal Edit(int mealId, List<Product> Products);

        public Meal CalculateNutrition(Meal meal);
    }
}

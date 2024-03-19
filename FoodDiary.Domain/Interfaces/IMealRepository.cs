using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Models;

namespace FoodDiary.Domain.Interfaces
{
    public interface IMealRepository
    {       
        public Meal GetPreDefinedMealByType(MealType mealType);

        public void AddProductToPreDefinedMeal(MealType mealType, Product product);

        public Product EditProductInPreDefinedMeal(MealType mealType, int productId, Product editedProduct);

        public (double, double, double, double) CalculateMealsNutrition(MealType mealType);
    }
}

using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Infrastructure.Repositories.Common;

namespace FoodDiary.Infrastructure.Repositories.Concrete
{
    public class MealRepository : IMealRepository
    {
        private Dictionary<MealType, Meal> preDefinedMeals;
        public MealRepository()
        {
            preDefinedMeals = new Dictionary<MealType, Meal>
            {
                { MealType.Breakfast, new Meal { MealType = MealType.Breakfast, Products = new List<Product>() } },
                { MealType.Dinner, new Meal { MealType = MealType.Dinner, Products = new List<Product>() } },
                { MealType.Supper, new Meal { MealType = MealType.Supper, Products = new List<Product>() } }
            };

        }

        public Meal GetPreDefinedMealByType(MealType mealType)
        {
            if (preDefinedMeals.ContainsKey(mealType))
            {
                return preDefinedMeals[mealType];
            }
            else
            {
                return null;
            }
        }

        public void AddProductToPreDefinedMeal(MealType mealType, Product product)
        {
            if (preDefinedMeals.ContainsKey(mealType))
            {
                preDefinedMeals[mealType].Products.Add(product);
            }
        }

        public void EditProductInPreDefinedMeal (MealType mealType, int productId, Product editedProduct)
        {
            Meal meal = GetPreDefinedMealByType (mealType);
            if (meal != null)
            {
                Product productToEdit = meal.Products.FirstOrDefault(p => p.Id == productId);
                if (productToEdit != null)
                {
                    productToEdit.Name = editedProduct.Name;
                    productToEdit.Carbo = editedProduct.Carbo;
                    productToEdit.Protein = editedProduct.Protein;
                    productToEdit.Fat = editedProduct.Fat;
                    productToEdit.Calories = editedProduct.Calories;
                }
            }
        } 
    }
}














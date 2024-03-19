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

        public Product EditProductInPreDefinedMeal(MealType mealType, int productId, Product sreditedProduct)
        {
            Meal meal = GetPreDefinedMealByType(mealType);

            Product productToEdit = meal.Products.FirstOrDefault(p => p.Id == productId);

            productToEdit.Name = sreditedProduct.Name;
            productToEdit.Carbo = sreditedProduct.Carbo;
            productToEdit.Protein = sreditedProduct.Protein;
            productToEdit.Fat = sreditedProduct.Fat;
            productToEdit.Calories = sreditedProduct.Calories;
            return productToEdit;



        }

        public (double, double, double, double) CalculateMealsNutrition(MealType mealType)
        {
            Meal meal = GetPreDefinedMealByType(mealType);
            if (meal != null)
            {
                double totalCalories = 0;
                double totalProtein = 0;
                double totalCarbs = 0;
                double totalFat = 0;

                foreach (var product in meal.Products)
                {
                    totalCalories += product.Calories;
                    totalProtein += product.Protein;
                    totalCarbs += product.Carbo;
                    totalFat += product.Fat;
                }

                return (totalCalories, totalProtein, totalCarbs, totalFat);
            }
            else
            {
                return (0, 0, 0, 0);
            }
        }
    }
}














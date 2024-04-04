using FoodDiary.Application.Interfaces;
using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;

namespace FoodDiary.Application.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        public MealService(IMealRepository repository)
        {
            _mealRepository = repository;
        }

        public int AddNew(Meal newMeal)
        {
            newMeal.dateTime = DateTime.Now;
            var mealId = _mealRepository.Create(newMeal);
            return mealId;
        }

        public List<Meal> GetAll()
        {
            var meals = _mealRepository.GetAll();
            return meals.ToList();
        }

        public Meal Edit(int mealId, List<Product> Products)
        {
            Meal mealToEdit = new Meal()
            {
                Id = mealId,
                Products = Products
            };
            _mealRepository.Edit(mealToEdit);
            return mealToEdit;
        }

        public Meal CalculateNutrition(Meal meal)
        {
            

            foreach (var product in meal.Products)
            {
                meal.TotalFat += (product.Fat * product.Weight) / 100;
                meal.TotalProtein += (product.Protein * product.Weight) / 100;
                meal.TotalCarbs += (product.Carbs * product.Weight) / 100;
                meal.TotalCalories += (product.Calories * product.Weight) / 100;
            }
            return meal;

        }
    }




}

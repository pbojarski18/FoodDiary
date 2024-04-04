using FluentAssertions;
using FoodDiary.Application.Interfaces;
using FoodDiary.Application.Services;
using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDairy.Tests
{
    public class MealServiceTests
    {
        private Mock<IMealRepository> mockMealRepository = new Mock<IMealRepository>();
        private MealService mealService;

        [Fact]
        public void AddNew_ShouldAddNewMealWithCurrentDate()
        {
            //Arrange           
            var mealType = MealType.Supper;

            var product = new Product { Id = 1, Name = "Produkt", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 };
            var product2 = new Product { Id = 2, Name = "Produkt2", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 };

            Meal meal = new Meal()
            {
                Id = 1,
                MealType = mealType,
                Products = new List<Product> { product, product2 }
            };
            mockMealRepository.Setup(m => m.Create(It.IsAny<Meal>())).Returns(meal.Id);
            mealService = new MealService(mockMealRepository.Object);

            //Act
            var newMealId = mealService.AddNew(meal);

            //Assert
            newMealId.Should().Be(1);
        }

        [Fact]
        public void GetAll_ShouldReturnAllMealsFromTheList()
        {
            //Arrange
            var meals = new List<Meal>()
            {
                new Meal() { Id = 1, MealType = MealType.Supper, dateTime = DateTime.Now, Products = new List<Product>() },
                new Meal() { Id = 2, MealType = MealType.Dinner, dateTime = DateTime.Now, Products = new List<Product>() },
                new Meal() { Id = 3, MealType = MealType.Breakfast, dateTime = DateTime.Now, Products = new List<Product>() },
            };
            mockMealRepository.Setup(m => m.GetAll()).Returns(meals);
            mealService = new MealService(mockMealRepository.Object);

            //Act
            var viewMeals = mealService.GetAll();

            //Assert
            viewMeals.Should().HaveCount(3);
        }

        [Fact]
        public void Edit_ShouldEditExistingMeal()
        {
            //Arrange
            int mealId = 1;
            var products = new List<Product>();
            Meal meal = new Meal() { Id = mealId, Products = products };
            mockMealRepository.Setup(m => m.Edit(It.IsAny<Meal>()));
            mealService = new MealService(mockMealRepository.Object);

            //Act
            var editedMeal = mealService.Edit(mealId, products);

            //Assert
            editedMeal.Id.Should().Be(mealId);
            editedMeal.Products.Should().HaveCount(0);
        }

        [Fact]
        public void CalculateNutrition_ShouldCalculateAllMealNutrition()
        {
            //Arrange
            int mealId = 1;
            var product = new Product { Id = 1, Name = "Produkt", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 200 };
            var product2 = new Product { Id = 2, Name = "Produkt2", Calories = 100, Carbs = 100, Protein = 100, Fat = 100, Weight = 100 };
            var products = new List<Product>() { product, product2 };
            Meal meal = new Meal() { Id = mealId, Products = products };
            mealService = new MealService(mockMealRepository.Object);

            //Act
            var calculatedMeal = mealService.CalculateNutrition(meal);

            //Assert
            calculatedMeal.Id.Should().Be(mealId);
            calculatedMeal.TotalCalories.Should().Be(300);
            calculatedMeal.TotalCarbs.Should().Be(300);
            calculatedMeal.TotalFat.Should().Be(300);
            calculatedMeal.TotalProtein.Should().Be(300);
        }
    }
}

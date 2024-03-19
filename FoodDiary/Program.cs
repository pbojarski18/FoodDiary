using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Application.Services;
using FoodDiary.Domain.Models;
using FoodDiary.Application.Interfaces;
using FoodDiary.Infrastructure.Repositories.Concrete;
using FoodDiary.Domain.Enums;
using FoodDiary.Handlers;

namespace FoodDiary
{
    public class Program
    {
        static void Main(string[] args)
        {
            //1. Produkty (utworzenie modelu produktu { id, nazwa, kcal, b/w/t } , repozytorium do dodawania, pobierania, modyfikowania produktow)
            //2. Zapisywanie produktow do pliku
            //3. Wyswietlanie produktow
            //4. Zarzadzanie posilkami(sniadanie obiad kolacja w danym dniu + dodawanie produktow)
            //5. Licznik kalori + makro(sniadanie, obiad, kolacja + suma wszystkich)
            //6. Raport tygodniowy wyszczegolniajac kazdy dzien

            IProductRepository productRepository = new ProductRepository();
            IProductService productService = new ProductService(productRepository);
            IMealRepository mealRepository = new MealRepository();
            ProductHandler productHandler = new ProductHandler(productService);
            Console.WriteLine("Welcome to Daily Food Dairy");
            while (true)
            {
                Console.WriteLine("Choose one of the following option");
                Console.WriteLine(" 1. Create new product\r\n 2. Show all products\r\n 3. Remove product\r\n 4. Edit product\r\n 5. Add existing product\r\n 6. Display all meals\r\n 7. Meal management\r\n 8. Meals calories counter");
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        productHandler.CreateNew();
                        break;
                    case "2":
                        productHandler.ShowAll();
                        break;
                    case "3":
                        productHandler.Remove();
                        break;
                    case "4":
                        productHandler.Edit();
                        break;
                    case "5":
                        var availableProducts = productService.ShowAll();
                        foreach (var existingProduct in availableProducts)
                        {
                            Console.WriteLine($"Id: {existingProduct.Id} Name: {existingProduct.Name} Calories: {existingProduct.Calories} Protein: {existingProduct.Protein} Carbo: {existingProduct.Carbo} Fat: {existingProduct.Fat}");
                        }
                        Console.WriteLine("Enter the Id of the product you want to add:");
                        int selectedProductId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter the type of meal (Breakfast, Dinner, Supper):");
                        string mealTypeInput = Console.ReadLine();
                        mealTypeInput = mealTypeInput.ToLower();
                        mealTypeInput = char.ToUpper(mealTypeInput[0]) + mealTypeInput.Substring(1);
                        if (Enum.TryParse<MealType>(mealTypeInput, out MealType mealType))
                        {
                            Product selectedProduct = availableProducts.FirstOrDefault(p => p.Id == selectedProductId);
                            if (selectedProduct != null)
                            {
                                mealRepository.AddProductToPreDefinedMeal(mealType, selectedProduct);
                                Console.WriteLine($"Product '{selectedProduct.Name}' added to {mealType} ");
                            }
                            else
                            {
                                Console.WriteLine("Invalid product Id.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid meal type.");
                        }

                        Meal selectedMeal = mealRepository.GetPreDefinedMealByType(mealType);
                        Console.WriteLine($"{mealType} products:");
                        foreach (var produkt in selectedMeal.Products)
                        {
                            Console.WriteLine($"ID: {produkt.Id}\r\nName: {produkt.Name}\r\n{produkt.Calories}kcal \r\n{produkt.Protein}b \r\n{produkt.Carbo}c \r\n{produkt.Fat}f");
                        }
                        break;
                    case "6":
                        foreach (MealType type in Enum.GetValues(typeof(MealType)))
                        {
                            Meal meal = mealRepository.GetPreDefinedMealByType(type);
                            Console.WriteLine($"{type} products:");
                            foreach (var produkt in meal.Products)
                            {
                                Console.WriteLine($"ID: {produkt.Id}\r\nName: {produkt.Name}\r\n{produkt.Calories}kcal \r\n{produkt.Protein}b \r\n{produkt.Carbo}c \r\n{produkt.Fat}f");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "7":
                        List<Meal> meals = new List<Meal>();
                        foreach (MealType type in Enum.GetValues(typeof(MealType)))
                        {
                            meals.Add(mealRepository.GetPreDefinedMealByType(type));
                            Console.WriteLine($"{type} products:");
                            foreach (var produkt in meals.Where(x => x.MealType == type).SelectMany(x => x.Products))
                            {
                                Console.WriteLine($"ID: {produkt.Id}\r\nName: {produkt.Name}\r\n{produkt.Calories}kcal \r\n{produkt.Protein}p \r\n{produkt.Carbo}c \r\n{produkt.Fat}f");
                            }
                            Console.WriteLine();
                        }


                        Console.WriteLine("Enter ID you want to edit");
                        int productToEditId = Convert.ToInt32(Console.ReadLine());
                        if (meals.SelectMany(p => p.Products).FirstOrDefault(p => p.Id == productToEditId) == null)
                        {
                            Console.WriteLine("Invalid product ID");
                            break;
                        }

                        Console.WriteLine("From which meal");
                        string mealTypeToEditInput = Console.ReadLine();
                        mealTypeToEditInput = mealTypeToEditInput.ToLower();
                        mealTypeToEditInput = char.ToUpper(mealTypeToEditInput[0]) + mealTypeToEditInput.Substring(1);

                        if (Enum.TryParse<MealType>(mealTypeToEditInput, out MealType mealTypeToEdit))

                        {
                            Console.WriteLine("Enter name:");
                            var newwName = Console.ReadLine();
                            Console.WriteLine("Enter calories:");
                            Double.TryParse(Console.ReadLine(), out double newwCalories);
                            Console.WriteLine("Enter amount of protein:");
                            Double.TryParse(Console.ReadLine(), out double newwProtein);
                            Console.WriteLine("Enter amount of carbo:");
                            Double.TryParse(Console.ReadLine(), out double newwCarbo);
                            Console.WriteLine("Enter amount of fat:");
                            Double.TryParse(Console.ReadLine(), out double newwFat);

                            Product productToEdit = new Product();
                            productToEdit.Name = newwName;
                            productToEdit.Calories = newwCalories;
                            productToEdit.Protein = newwProtein;
                            productToEdit.Carbo = newwCarbo;
                            productToEdit.Fat = newwFat;

                            mealRepository.EditProductInPreDefinedMeal(mealTypeToEdit, productToEditId, productToEdit);
                            Console.WriteLine($"New details:\r\n Id: {productToEdit.Id}\r\n Name: {productToEdit.Name}\r\n Calories: {productToEdit.Calories}\r\n Protein: {productToEdit.Protein}\r\n Carbo: {productToEdit.Carbo}\r\n Fat: {productToEdit.Fat}");
                            Console.WriteLine($"Product {productToEdit.Name} edited in {mealTypeToEdit}");

                        }
                        else
                        {
                            Console.WriteLine("Invalid meal");
                        }
                        break;

                    case "8":
                        var breakfastNutrition = mealRepository.CalculateMealsNutrition(MealType.Breakfast);
                        var dinnerNutrition = mealRepository.CalculateMealsNutrition(MealType.Dinner);
                        var supperNutrition = mealRepository.CalculateMealsNutrition(MealType.Supper);

                        Console.WriteLine("Breakfast:");
                        Console.WriteLine($"Calories: {breakfastNutrition.Item1}");
                        Console.WriteLine($"Protein: {breakfastNutrition.Item2}");
                        Console.WriteLine($"Carbs: {breakfastNutrition.Item3}");
                        Console.WriteLine($"Fat: {breakfastNutrition.Item4}");
                        Console.WriteLine();

                        Console.WriteLine("Dinner:");
                        Console.WriteLine($"Calories: {dinnerNutrition.Item1}");
                        Console.WriteLine($"Protein: {dinnerNutrition.Item2}");
                        Console.WriteLine($"Carbs: {dinnerNutrition.Item3}");
                        Console.WriteLine($"Fat: {dinnerNutrition.Item4}");
                        Console.WriteLine();

                        Console.WriteLine("Supper:");
                        Console.WriteLine($"Calories: {supperNutrition.Item1}");
                        Console.WriteLine($"Protein: {supperNutrition.Item2}");
                        Console.WriteLine($"Carbs: {supperNutrition.Item3}");
                        Console.WriteLine($"Fat: {supperNutrition.Item4}");
                        break;
                }

            }
        }
    }
}

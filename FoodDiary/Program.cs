using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Application.Services;
using FoodDiary.Domain.Models;
using FoodDiary.Application.Interfaces;
using FoodDiary.Infrastructure.Repositories.Concrete;
using FoodDiary.Domain.Enums;

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
            Console.WriteLine("Welcome to Daily Food Dairy");
            while (true)
            {
                Console.WriteLine("Choose one of the following option");
                Console.WriteLine(" 1. Create new product\r\n 2. Show all products\r\n 3. Remove product\r\n 4. Edit product\r\n 5. Add existing product\r\n 6. Display all meals\r\n 7. Meal management");
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Product product = new Product();
                        Console.WriteLine("Enter name:");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("Enter calories:");
                        Double.TryParse(Console.ReadLine(), out double calories);
                        Console.WriteLine("Enter amount of protein:");
                        Double.TryParse(Console.ReadLine(), out double protein);
                        Console.WriteLine("Enter amount of carbo:");
                        Double.TryParse(Console.ReadLine(), out double carbo);
                        Console.WriteLine("Enter amount of fat:");
                        Double.TryParse(Console.ReadLine(), out double fat);
                        product.Calories = calories;
                        product.Protein = protein;
                        product.Carbo = carbo;
                        product.Fat = fat;
                        var status = productService.AddNew(product);
                        if (status == -1)
                        {
                            Console.WriteLine("Wrong data");
                        }
                        else
                        {
                            Console.WriteLine($"Product created with id {status}");
                        }
                        break;
                    case "2":
                        var productsList = productService.ShowAll();
                        foreach (var existingProduct in productsList)
                        {
                            Console.WriteLine($"Id: {existingProduct.Id} Name: {existingProduct.Name} Calories: {existingProduct.Calories} Protein: {existingProduct.Protein} Carbo: {existingProduct.Carbo} Fat: {existingProduct.Fat}");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Which product would you like to remove?");
                        int.TryParse(Console.ReadLine(), out int idToRemove);
                        productService.RemoveProduct(idToRemove);
                        break;
                    case "4":
                        Console.WriteLine("Which product would you like to edit?");
                        int.TryParse(Console.ReadLine(), out int idToEdit);
                        Console.WriteLine("Enter name:");
                        var newName = Console.ReadLine();
                        Console.WriteLine("Enter calories:");
                        Double.TryParse(Console.ReadLine(), out double newCalories);
                        Console.WriteLine("Enter amount of protein:");
                        Double.TryParse(Console.ReadLine(), out double newProtein);
                        Console.WriteLine("Enter amount of carbo:");
                        Double.TryParse(Console.ReadLine(), out double newCarbo);
                        Console.WriteLine("Enter amount of fat:");
                        Double.TryParse(Console.ReadLine(), out double newFat);
                        var editedProduct = productService.Edit(idToEdit, newName, newCalories, newProtein, newCarbo, newFat);
                        Console.WriteLine($"Id: {editedProduct.Id}\r\n Name: {editedProduct.Name}\r\n Calories: {editedProduct.Calories}\r\n Protein: {editedProduct.Protein}\r\n Carbo: {editedProduct.Carbo}\r\n Fat: {editedProduct.Fat}");
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
                        List<Meal> meal1 = new List<Meal>();
                        foreach (MealType type in Enum.GetValues(typeof(MealType)))
                        {
                            meal1.Add(mealRepository.GetPreDefinedMealByType(type));
                            Console.WriteLine($"{type} products:");
                            foreach (var produkt in meal1.Where(x => x.MealType == type).SelectMany(x => x.Products))
                            {
                                Console.WriteLine($"ID: {produkt.Id}\r\nName: {produkt.Name}\r\n{produkt.Calories}kcal \r\n{produkt.Protein}p \r\n{produkt.Carbo}c \r\n{produkt.Fat}f");
                            }
                            Console.WriteLine();
                        }


                        Console.WriteLine("Enter ID you want to edit");
                        int productToEditId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("From which meal");
                        string mealTypeToEditInput = Console.ReadLine();
                        mealTypeToEditInput = mealTypeToEditInput.ToLower();
                        mealTypeToEditInput = char.ToUpper(mealTypeToEditInput[0]) + mealTypeToEditInput.Substring(1);

                        if (Enum.TryParse<MealType>(mealTypeToEditInput, out MealType mealTypeToEdit))
                        {
                            Product sreditedProduct = meal1.SelectMany(x => x.Products).FirstOrDefault(p => p.Id == productToEditId);
                            if (sreditedProduct != null)
                            {
                                mealRepository.EditProductInPreDefinedMeal(mealTypeToEdit, productToEditId, sreditedProduct);
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
                                Console.WriteLine($"Id: {sreditedProduct.Id}\r\n Name: {sreditedProduct.Name}\r\n Calories: {sreditedProduct.Calories}\r\n Protein: {sreditedProduct.Protein}\r\n Carbo: {sreditedProduct.Carbo}\r\n Fat: {sreditedProduct.Fat}");
                                Console.WriteLine($"Product {sreditedProduct.Name} edited in {mealTypeToEdit}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid product Id");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid meal");
                        }
                        break;
                }
            }
        }
    }
}

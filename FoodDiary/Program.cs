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
            IMealService mealService = new MealService(mealRepository);
            ProductHandler productHandler = new ProductHandler(productService);
            MealHandler mealHandler = new MealHandler(mealService, productService);
            Console.WriteLine("Welcome to Daily Food Dairy");
            while (true)
            {
                Console.WriteLine("Choose one of the following option");
                Console.WriteLine(" 1. Create new product\r\n 2. Show all products\r\n 3. Remove product\r\n 4. Edit product\r\n 5. Add new meal\r\n 6. Display all meals\r\n 7. Meal management\r\n 8. Meals calories counter");
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
                        mealHandler.AddNew();
                        break;
                    case "6":
                        mealHandler.ShowAll();                       
                        break;
                    case "7":
                        mealHandler.ManageMeal();
                        break;
                    case "8":
                        mealHandler.CalculateMealNutrition();
                        break;
                       
                }

            }
        }
    }
}

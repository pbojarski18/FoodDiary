using FoodDiary.Application.Interfaces;
using FoodDiary.Application.Services;
using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Infrastructure.Repositories.Concrete;

namespace FoodDiary.Handlers
{
    public class MealHandler
    {
        private readonly IMealService _mealService;
        private readonly IProductService _productService;
        public MealHandler(IMealService mealService, IProductService productService)
        {
            _mealService = mealService;
            _productService = productService;
        }
        public void AddNew()
        {
            Console.WriteLine("What type of meal you want to create (Breakfast, Dinner or Supper):");
            string mealTypeInput = Console.ReadLine();
            mealTypeInput = mealTypeInput.ToLower();
            mealTypeInput = char.ToUpper(mealTypeInput[0]) + mealTypeInput.Substring(1);
            Meal meal = new Meal();
            string userInput = "y";

            if (Enum.TryParse<MealType>(mealTypeInput, out MealType mealType))
            {
                meal.MealType = mealType;
            }
            else
            {
                Console.WriteLine("Invalid meal");
                return;
            }
            do
            {
                var products = _productService.GetAll();
                foreach (var productToShow in products)
                {
                    Console.WriteLine($"ID: {productToShow.Id} | Name: {productToShow.Name} | Calories: {productToShow.Calories} | Protein: {productToShow.Protein}g | Carbs: {productToShow.Carbs}g | Fat: {productToShow.Fat}g | Weight: {productToShow.Weight}g");
                }
                Console.WriteLine("Choose ID of products you want to add");
                int.TryParse(Console.ReadLine(), out int productId);
                var product = products.FirstOrDefault(p => p.Id == productId);


                if (product != null)
                {
                    Console.WriteLine("Enter product weight");
                    int.TryParse(Console.ReadLine(), out int userWeight);
                    if (meal.Products.Any(p => p.Id == productId))
                    {
                        meal.Products.FirstOrDefault(p => p.Id == productId).Weight += userWeight;
                    }
                    else
                    {
                        product.Weight = userWeight;
                        meal.Products.Add(product);
                    }
                }
                Console.WriteLine("Do you want to add another one? Y/N");
                userInput = Console.ReadLine();

            }
            while (userInput == "y");
            userInput = userInput.ToLower();
            if (userInput == "n") Console.WriteLine("Products added successfuly");

            _mealService.AddNew(meal);
        }

        public void ShowAll()
        {
            var meals = _mealService.GetAll();
            foreach (var meal in meals)
            {
                Console.WriteLine($"{meal.dateTime} | Meal: {meal.MealType} | Meal Id: {meal.Id}");
                foreach (var product in meal.Products)
                {
                    Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Calories: {product.Calories} | Protein: {product.Protein}g | Carbs: {product.Carbs}g | Fat: {product.Fat}g | Weight: {product.Weight}g");
                }
                Console.WriteLine();
            }
        }

        public void ManageMeal()
        {
            Console.WriteLine("Enter meal ID you want to manage");
            var meals = _mealService.GetAll();
            foreach (var mealToEdit in meals)
            {
                Console.WriteLine($"{mealToEdit.dateTime} | Meal: {mealToEdit.MealType} | Meal Id: {mealToEdit.Id}");
            }
            int.TryParse(Console.ReadLine(), out int mealId);
            var meal = meals.FirstOrDefault(m => m.Id == mealId);
            if (meal != null)
            {
                Console.WriteLine("What do you want to do");
                Console.WriteLine("1. Edit products");
                Console.WriteLine("2. Add product");
                Console.WriteLine("3. Remove product");
                Console.WriteLine("4. Exit");

                int.TryParse(Console.ReadLine(), out int userChoice);
                switch (userChoice)
                {
                    case 1:
                        editProductInMeal(meal);
                        break;
                    case 2:
                        addProductToMeal(meal);
                        break;
                    case 3:
                        removeProdutFromMeal(meal);
                        break;
                    case 4:
                        Console.WriteLine("Operation cancelled");
                        break;
                }

            }
            else
            {
                Console.WriteLine("Invalid meal ID");
                return;
            }
        }

        private void addProductToMeal(Meal editedMeal)
        {

            string userInput = "y";

            do
            {
                var products = _productService.GetAll();
                foreach (var productToShow in products)
                {
                    Console.WriteLine($"ID: {productToShow.Id} | Name: {productToShow.Name} | Calories: {productToShow.Calories} | Protein: {productToShow.Protein}g | Carbs: {productToShow.Carbs}g | Fat: {productToShow.Fat}g | Weight: {productToShow.Weight}g");
                }
                Console.WriteLine("Choose ID of products you want to add");
                int.TryParse(Console.ReadLine(), out int productId);
                var product = products.FirstOrDefault(p => p.Id == productId);


                if (product != null)
                {
                    Console.WriteLine("Enter product weight");
                    int.TryParse(Console.ReadLine(), out int userWeight);

                    if (editedMeal.Products.Any(p => p.Id == productId))
                    {
                        editedMeal.Products.FirstOrDefault(p => p.Id == productId).Weight += userWeight;
                    }
                    else
                    {
                        product.Weight = userWeight;
                        editedMeal.Products.Add(product);
                    }
                }
                Console.WriteLine("Do you want to add another one? Y/N");
                userInput = Console.ReadLine();
            }
            while (userInput == "y");
            userInput = userInput.ToLower();
            if (userInput == "n") Console.WriteLine("Products added successfuly");
            _mealService.Edit(editedMeal.Id, editedMeal.Products);
        }

        private void removeProdutFromMeal(Meal editedMeal)
        {
            string userInput = "y";

            do
            {
                var products = editedMeal.Products;
                foreach (var productToShow in products)
                {
                    Console.WriteLine($"ID: {productToShow.Id} | Name: {productToShow.Name} | Calories: {productToShow.Calories} | Protein: {productToShow.Protein}g | Carbs: {productToShow.Carbs}g | Fat: {productToShow.Fat}g | Weight: {productToShow.Weight}g");
                }
                Console.WriteLine("Choose ID of product you want to remove");
                int.TryParse(Console.ReadLine(), out int productId);
                var product = products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    editedMeal.Products.Remove(product);
                }
                Console.WriteLine("Do you want to remove another one? Y/N");
                userInput = Console.ReadLine();

            }
            while (userInput == "y");
            userInput = userInput.ToLower();
            if (userInput == "n") Console.WriteLine("Products removed successfuly");
            _mealService.Edit(editedMeal.Id, editedMeal.Products);
        }

        private void editProductInMeal(Meal editedMeal)
        {
            string userInput = "y";

            do
            {
                var products = editedMeal.Products;
                foreach (var productToShow in products)
                {
                    Console.WriteLine($"ID: {productToShow.Id} | Name: {productToShow.Name} | Calories: {productToShow.Calories} | Protein: {productToShow.Protein}g | Carbs: {productToShow.Carbs}g | Fat: {productToShow.Fat}g | Weight: {productToShow.Weight}g");
                }
                Console.WriteLine("Choose ID of product you want to edit");
                int.TryParse(Console.ReadLine(), out int productId);
                var product = products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    Console.WriteLine("Enter new weight");
                    int.TryParse(Console.ReadLine(), out int userWeight);
                    product.Weight = userWeight;
                }
                Console.WriteLine("Do you want to edit another one? Y/N");
                userInput = Console.ReadLine();

            }
            while (userInput == "y");
            userInput = userInput.ToLower();
            if (userInput == "n") Console.WriteLine("Products edited successfuly");
            _mealService.Edit(editedMeal.Id, editedMeal.Products);
        }

        public void CalculateMealNutrition()
        {
            var meals = _mealService.GetAll();
            foreach (var meal in meals)
            {
                Console.WriteLine($"{meal.dateTime} Meal: {meal.MealType} Id: {meal.Id} products:");
                foreach (var product in meal.Products)
                {
                    Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Calories: {product.Calories} | Protein: {product.Protein}g | Carbs: {product.Carbs}g | Fat: {product.Fat}g | Weight: {product.Weight}g");
                }
                _mealService.CalculateNutrition(meal);
                Console.WriteLine(meal.ToString());

                Console.WriteLine();
            }

        }
    }
}



















using FoodDiary.Application.Interfaces;
using FoodDiary.Application.Services;
using FoodDiary.Domain.Models;

namespace FoodDiary.Handlers
{
    public class ProductHandler
    {
        private readonly IProductService _productService;
        public ProductHandler(IProductService productService)
        {
            _productService = productService;
        }
        public void CreateNew()
        {
            Product product = new Product();
            Console.WriteLine("Enter name:");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter calories:");
            Double.TryParse(Console.ReadLine(), out double calories);
            Console.WriteLine("Enter amount of protein:");
            Double.TryParse(Console.ReadLine(), out double protein);
            Console.WriteLine("Enter amount of carbs:");
            Double.TryParse(Console.ReadLine(), out double carbs);
            Console.WriteLine("Enter amount of fat:");
            Double.TryParse(Console.ReadLine(), out double fat);
            product.Calories = calories;
            product.Protein = protein;
            product.Carbs = carbs;
            product.Fat = fat;
            var status = _productService.AddNew(product);
            if (status == -1)
            {
                Console.WriteLine("Wrong data");
            }
            else
            {
                Console.WriteLine($"Product created with id {status}");
            }
        }

        public void ShowAll()
        {
            var productsList = _productService.GetAll();
            foreach (var existingProduct in productsList)
            {
                Console.WriteLine($"Id: {existingProduct.Id} | Name: {existingProduct.Name} | Calories: {existingProduct.Calories} | Protein: {existingProduct.Protein}g | Carbs: {existingProduct.Carbs}g | Fat: {existingProduct.Fat}g | Weight: {existingProduct.Weight}g");
            }
        }

        public void Remove()
        {
            var productsToEdit = _productService.GetAll();
            foreach (var product in productsToEdit)
            {
                Console.WriteLine($"Id: {product.Id} | Name: {product.Name}");
            }

            Console.WriteLine("Which product would you like to remove?");
            int.TryParse(Console.ReadLine(), out int idToRemove);
            if (idToRemove <= productsToEdit.Count)
            {
                _productService.Remove(idToRemove);
                Console.WriteLine($"Product with ID {idToRemove} removed successfully");
            }
            else
            {
                Console.WriteLine("Product doesn't exist");
            }
        }

        public void Edit()
        {
            var productsToEdit = _productService.GetAll();
            foreach (var product in productsToEdit)
            {
                Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Calories: {product.Calories} | Protein: {product.Protein}g | Carbs: {product.Carbs}g | Fat: {product.Fat}g | Weight: {product.Weight}g");
            }

            Console.WriteLine("Which product would you like to edit?");
            int.TryParse(Console.ReadLine(), out int idToEdit);
            if (idToEdit <= productsToEdit.Count)
            {
                Console.WriteLine("Enter name:");
                var newName = Console.ReadLine();
                Console.WriteLine("Enter calories:");
                Double.TryParse(Console.ReadLine(), out double newCalories);
                Console.WriteLine("Enter amount of protein:");
                Double.TryParse(Console.ReadLine(), out double newProtein);
                Console.WriteLine("Enter amount of carbs:");
                Double.TryParse(Console.ReadLine(), out double newCarbs);
                Console.WriteLine("Enter amount of fat:");
                Double.TryParse(Console.ReadLine(), out double newFat);
                var editedProduct = _productService.Edit(idToEdit, newName, newCalories, newProtein, newCarbs, newFat);
                Console.WriteLine($"Id: {editedProduct.Id}\r\n Name: {editedProduct.Name}\r\n Calories: {editedProduct.Calories}\r\n Protein: {editedProduct.Protein}\r\n Carbs: {editedProduct.Carbs}\r\n Fat: {editedProduct.Fat}");
            }
            else
            {
                Console.WriteLine("Product doesn't exist");
            }
        }
    }
}

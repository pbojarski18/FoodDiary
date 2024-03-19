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
            Console.WriteLine("Enter amount of carbo:");
            Double.TryParse(Console.ReadLine(), out double carbo);
            Console.WriteLine("Enter amount of fat:");
            Double.TryParse(Console.ReadLine(), out double fat);
            product.Calories = calories;
            product.Protein = protein;
            product.Carbo = carbo;
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
            var productsList = _productService.ShowAll();
            foreach (var existingProduct in productsList)
            {
                Console.WriteLine($"Id: {existingProduct.Id} Name: {existingProduct.Name} Calories: {existingProduct.Calories} Protein: {existingProduct.Protein} Carbo: {existingProduct.Carbo} Fat: {existingProduct.Fat}");
            }
        }

        public void Remove()
        {
            Console.WriteLine("Which product would you like to remove?");
            int.TryParse(Console.ReadLine(), out int idToRemove);
            _productService.RemoveProduct(idToRemove);
        }

        public void Edit()
        {
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
            var editedProduct = _productService.Edit(idToEdit, newName, newCalories, newProtein, newCarbo, newFat);
            Console.WriteLine($"Id: {editedProduct.Id}\r\n Name: {editedProduct.Name}\r\n Calories: {editedProduct.Calories}\r\n Protein: {editedProduct.Protein}\r\n Carbo: {editedProduct.Carbo}\r\n Fat: {editedProduct.Fat}");
        }
    }
}

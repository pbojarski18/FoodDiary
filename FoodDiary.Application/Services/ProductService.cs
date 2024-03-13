using FoodDiary.Application.Interfaces;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;

namespace FoodDiary.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public int AddNew(Product newProduct)
        {
            if (newProduct.Name.Length < 1)
            {
                return -1;
            }
            else
            {
                var productId = _repository.Create(newProduct);
                return productId;
            }

        }

        public List<Product> ShowAll()
        {
            var products = _repository.GetAll();
            return products.ToList();
        }

        public void RemoveProduct(int productId)
        {
            
            _repository.Remove(productId);

        }

        public Product Edit(int productId, string name, double calories, double protein, double carbo, double fat)
        {
            Product productToEdit = new Product()
            {
                Id = productId,
                Name = name,
                Calories = calories,
                Protein = protein,
                Carbo = carbo,
                Fat = fat
            };
            _repository.Edit(productToEdit);
            return productToEdit;


        }
    }
}

using FoodDiary.Application.Services;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Domain.Models.Common;

namespace FoodDiary.Application.Interfaces
{
    public interface IProductService
    {      
        public int AddNew(Product newProduct);

        public List<Product> ShowAll();

        public void RemoveProduct(int productId);

        public Product Edit(int productId, string name, double calories, double protein, double carbo, double fat);




    }
}

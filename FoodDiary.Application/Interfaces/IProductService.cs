using FoodDiary.Application.Services;
using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Domain.Models.Common;

namespace FoodDiary.Application.Interfaces
{
    public interface IProductService
    {      
        public int AddNew(Product newProduct);

        public List<Product> GetAll();

        public void Remove(int productId);

        public Product Edit(int productId, string name, double calories, double protein, double carbs, double fat);




    }
}

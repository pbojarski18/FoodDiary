using FoodDiary.Domain.Models;

namespace FoodDiary.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public void Edit(Product productToEdit);
    }
}

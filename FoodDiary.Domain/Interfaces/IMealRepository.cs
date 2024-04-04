using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Models;

namespace FoodDiary.Domain.Interfaces
{
    public interface IMealRepository : IBaseRepository<Meal>
    {
        public void Edit(Meal editedMeal);
    }
}

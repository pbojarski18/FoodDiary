using FoodDiary.Domain.Models.Common;

namespace FoodDiary.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : AuditableEntity
    {
        public List<T> items { get; set; }

        public int Create(T newItem);

        public IEnumerable<T> GetAll();

        public void Remove(int itemToRemove);

        public void SaveToFile();





    }
}

using FoodDiary.Domain.Interfaces;
using FoodDiary.Domain.Models;
using FoodDiary.Domain.Models.Common;
using Newtonsoft.Json;


namespace FoodDiary.Infrastructure.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AuditableEntity
    {
        public List<T> items { get; set; } = new List<T>();

        protected string filePath { get; set; }
        public int Create(T newItem)
        {
            if (items.Count == 0)
            {
                newItem.Id = 1;
            }
            else
            {
                var lastId = items.Max(i => i.Id);
                newItem.Id = lastId + 1;
            }

            items.Add(newItem);
            SaveToFile();
            return newItem.Id;
        }

        public IEnumerable<T> GetAll()
        {
            var fileData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(fileData);
        }

        public void Remove(int idToRemove)
        {
            var itemToRemove = items.FirstOrDefault(i => i.Id == idToRemove);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                SaveToFile();
            }
        }

        public void SaveToFile()
        {
            using StreamWriter streamWriter = new StreamWriter(filePath);
            using JsonWriter writer = new JsonTextWriter(streamWriter);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, items);
        }      
    }
}

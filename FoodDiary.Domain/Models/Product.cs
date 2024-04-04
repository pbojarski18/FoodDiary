using FoodDiary.Domain.Models.Common;

namespace FoodDiary.Domain.Models
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carbs { get; set; }

        public double Fat { get; set; }

        public double Weight { get; set; } = 100;
    }
}

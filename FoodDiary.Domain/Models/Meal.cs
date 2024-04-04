using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Models.Common;

namespace FoodDiary.Domain.Models
{
    public class Meal : AuditableEntity
    {
        public DateTime dateTime { get; set; }
        public MealType MealType { get; set; }

        public double TotalCalories { get; set; }

        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public List<Product> Products { get; set; }

        public Meal()
        {
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return $"Total Calories: {TotalCalories}kcal\r\nTotal Fat: {TotalFat}g\r\nTotal Carbs: {TotalCarbs}g\r\nTotal Protein: {TotalProtein}g";
        }
}



}

﻿using FoodDiary.Domain.Enums;
using FoodDiary.Domain.Models.Common;

namespace FoodDiary.Domain.Models
{
    public class Meal : AuditableEntity
    {

        public MealType MealType { get; set; }

        public List<Product> Products { get; set; }

    }
}

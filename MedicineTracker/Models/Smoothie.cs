using System;
using System.Collections.Generic;
using SQLite;

namespace MedicineTracker.Models
{
    public class Smoothie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public List<Ingredient> ingredients = new List<Ingredient>();
        public string TestIngredient { get; set; }
    }


    public class Ingredient
    {
        public string name;
        public int kcal;
    }
}

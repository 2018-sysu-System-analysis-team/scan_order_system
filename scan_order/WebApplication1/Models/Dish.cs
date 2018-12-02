using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Dish
    {
            public int DishId { get; set; }
            public string DishName { get; set; }
            public decimal DishPrice { get; set; }
            public string DishIntroduction { get; set; }
            public string DishPhoto { get; set; }

    }
    public class DishDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
    }
}
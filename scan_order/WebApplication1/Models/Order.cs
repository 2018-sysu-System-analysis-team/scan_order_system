using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public decimal payment { get; set; }
        public int state { get; set; }
        public DateTime creatTime { get; set; }
        public DateTime endTime { get; set; }
        public int userId { get; set; }

    }

    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}
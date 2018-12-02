using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int num { get; set; }
        public string Title { get; set; }
        public decimal price { get; set; }
        public decimal TotalFree { get; set; }
    }
    public class OrderItemDbContext : DbContext
    {
        public DbSet<OrderItem> orderItems { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.User> Users { get; set; }
    }
}
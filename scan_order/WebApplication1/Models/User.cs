using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { set; get; }
        public string UserName { set; get; }
        public string UserPassword { get; set; }
        public DateTime CreateData { get; set; }
    }
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

}
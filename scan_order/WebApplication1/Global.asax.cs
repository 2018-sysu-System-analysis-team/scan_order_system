using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer<MovieDbContext>(new DropCreateDatabaseIfModelChanges<MovieDbContext>());
            Database.SetInitializer<OrderDbContext>(new DropCreateDatabaseIfModelChanges<OrderDbContext>());
            Database.SetInitializer<OrderItemDbContext>(new DropCreateDatabaseIfModelChanges<OrderItemDbContext>());
            Database.SetInitializer<DishDbContext>(new DropCreateDatabaseIfModelChanges<DishDbContext>());
            Database.SetInitializer<UserDbContext>(new DropCreateDatabaseIfModelChanges<UserDbContext>());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

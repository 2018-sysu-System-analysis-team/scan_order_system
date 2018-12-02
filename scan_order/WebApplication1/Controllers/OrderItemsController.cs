using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderItemsController : Controller
    {
        private OrderItemDbContext db = new OrderItemDbContext();
        private OrderDbContext orderDbContext = new OrderDbContext();

        [HttpPost]
        public JsonResult CreateOrderList(List<int> orderItemsList, List<int> num,int userId )
        {
            /*
                foreach (var order in orderItemsList)
                {
                    db.orderItems.Add(order);
                    db.SaveChanges();
                }
                */
             //在orderItem添加订单明细表
            DishDbContext DishDb = new DishDbContext();
            int neworderId ;
            Order LastOrder = orderDbContext.Orders.
                    OrderByDescending(p => p.orderId)
                    .FirstOrDefault();
            neworderId = LastOrder.orderId + 1;
            List<string> result = new List<string>();
            List<decimal> TotalFree  = new List<decimal>() ;        
            try
            {

                for (int i = 0; i < orderItemsList.Count; i++)
                {
                    var item = DishDb.Dishes.Find(orderItemsList[i]);
                    decimal Free = item.DishPrice * num[i];
                    var orderItem = new OrderItem {  ItemId = orderItemsList[i], OrderId = neworderId, num = num[i], price = item.DishPrice, Title = item.DishName, TotalFree = Free };
                    db.orderItems.Add(orderItem);
                    TotalFree.Add(Free);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json("False");

            }
            finally
            {
                decimal money = TotalFree.Sum();
                Order neworder = new Order { orderId = neworderId, payment = money, creatTime = DateTime.Now, endTime = DateTime.Now, userId = userId, state = 0 };  
                orderDbContext.Orders.Add(neworder);
                orderDbContext.SaveChanges();
                result.Add(Convert.ToString(neworderId));
                result.Add(Convert.ToString(money));
                result.Add("0");
            }

            return Json(result);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

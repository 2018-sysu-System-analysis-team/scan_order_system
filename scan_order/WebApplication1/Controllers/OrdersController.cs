using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        private OrderDbContext db = new OrderDbContext();
        private OrderItemDbContext OrderItemDb = new OrderItemDbContext();
        private DishDbContext DishDb = new DishDbContext();

        [HttpPost]
        public JsonResult PayOrder(int PayOrderId,decimal money)
        {
            Order order = db.Orders.Find(PayOrderId);
            decimal TotalFree = order.payment;
            if(money != TotalFree)
            {
                return Json("False");
            }
            order.state = 1;
            order.endTime = DateTime.Now;
            db.SaveChanges();
            int state = order.state;
            return Json("succeed");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrder(int orderId, List<int> OrderItemsList, List<int> num)

        {
            List<string> result = new List<string>();
            List<decimal> TotalFree = new List<decimal>();
            Order order = db.Orders.Find(orderId);
            IEnumerable<OrderItem> oldOrderItem =
                from orderItem in OrderItemDb.orderItems
                where orderItem.OrderId == orderId
                select orderItem;
            foreach (OrderItem item in oldOrderItem)
            {
                OrderItemDb.orderItems.Remove(item);
            }
            OrderItemDb.SaveChanges();
            int userId = order.userId;

            try
            {
                for (int i = 0; i < OrderItemsList.Count; i++)
                {
                    var item = DishDb.Dishes.Find(OrderItemsList[i]);
                    decimal Free = item.DishPrice * num[i];
                    var orderItem = new OrderItem { ItemId = OrderItemsList[i], OrderId = orderId, num = num[i], price = item.DishPrice, Title = item.DishName, TotalFree = Free };
                    OrderItemDb.orderItems.Add(orderItem);
                    TotalFree.Add(Free);
                }

                OrderItemDb.SaveChanges();

            }
            catch(Exception e)
            {
                return Json("false");
            }
            finally
            {
                //修改数据库中的订单Order
                decimal money = TotalFree.Sum();
                order.payment = money;
                order.state = 0;
                db.SaveChanges();
                result.Add(Convert.ToString(orderId));
                result.Add(Convert.ToString(money));
                result.Add("0");
            }
            return Json(result);
         }
        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderId,payment,state,creatTime,endTime,userId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return Json("deleteSucceed");
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

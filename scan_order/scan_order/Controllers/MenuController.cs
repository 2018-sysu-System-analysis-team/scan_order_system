using scan_order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scan_order.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
           
            return View();
        
        }

        public ViewResult hello()
        {
            System.DateTime currentTime = new System.DateTime();
            int hour = currentTime.Hour;
            ViewBag.Greeting = hour < 12 ? "good morning" : "good afternoon";
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse )
        {
            return View("Thank", guestResponse);
        }
    }
}
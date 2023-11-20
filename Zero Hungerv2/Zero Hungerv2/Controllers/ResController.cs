using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hungerv2.EF;

namespace Zero_Hungerv2.Controllers
{
    public class ResController : Controller
    {
        // GET: Res

        [HttpGet]
        public ActionResult Index()
        {
           

            user loggedInUser = (user)Session["logged"];


            if (loggedInUser != null && loggedInUser.role == "Restaurant")
            {

                ViewBag.user_name = loggedInUser.username;
                ViewBag.id = loggedInUser.id;
            }


            return View();
        }





        [HttpPost]
        public ActionResult Index(order o)
        {
            var db = new zero_hungerEntities();
            
            db.orders.Add(o);
            db.SaveChanges();

            return RedirectToAction("Order");
        }


        public ActionResult Order()
        {
            user loggedInUser = (user)Session["logged"];
            var db = new zero_hungerEntities();
            var data = db.orders.Where(r => r.restaurant_id == loggedInUser.id).OrderByDescending(o => o.order_id).ToList();
  
            return View(data);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new zero_hungerEntities();
            var data = (from c in db.orders
                        where c.order_id == id
                        select c).SingleOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(order c)
        {
            var db = new zero_hungerEntities();
            var exdata = db.orders.Find(c.order_id);
            exdata.order_name = c.order_name;
            exdata.production = c.production;
            exdata.expire = c.expire;
            exdata.status = c.status;
          
            db.SaveChanges();
            return RedirectToAction("Index");

        }





    }
}
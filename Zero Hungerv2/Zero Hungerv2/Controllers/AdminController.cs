using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hungerv2.EF;

namespace Zero_Hungerv2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
       
        public ActionResult Index()
        {

            user loggedInUser = (user)Session["logged"];


            if (loggedInUser != null && loggedInUser.role == "Admin")
            {

                ViewBag.name = loggedInUser.name;
                ViewBag.id = loggedInUser.id;
            }

            var db = new zero_hungerEntities();

         

            var data = db.orders.Where(t=> t.status == "Orderd").OrderByDescending(o => o.order_id).ToList();
            var dt = db.users.Where(u => u.role == "Employee").ToList();
            ViewBag.users = new SelectList(dt, "id", "name");


            return View(data);
        }

        public ActionResult Accept(int orderId, int empId)
        {
            string status = "ASSIGN";

          

            var db = new zero_hungerEntities();

            var st = db.orders.Find(orderId);
            st.status= status;

            var exdata = new asign
            {
                ord_id = orderId,
                emp_id = empId,
                status = status
            };

            db.asigns.Add(exdata);
            db.SaveChanges();

            // Redirect back to the Index action after the assignment is saved
            return RedirectToAction("Index");
        }



        public ActionResult Decline(int orderId)
        {
            string status = "Declined";
            var db = new zero_hungerEntities();
            var st = db.orders.Find(orderId);
            st.status = status;

            //db.orders.Add(exdata);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }








        public ActionResult Assign() 
        { 
         var db = new zero_hungerEntities();    
         var data= db.asigns.OrderByDescending(o => o.id).ToList(); 
         //  db.users.ToList();   
           // db.orders.ToList();




         return View(data);
        }

        public ActionResult Orders()
        {

            var db = new zero_hungerEntities(); 
            var data =db.orders.OrderByDescending(o => o.order_id).ToList();   


            return View(data);

        }



        public ActionResult Remove(int order_id) {


            var db = new zero_hungerEntities();
            var dt = db.asigns.SingleOrDefault(a => a.ord_id == order_id);
            var exdata = db.orders.Find(order_id);
            if (dt != null)
            {
                db.asigns.Remove(dt);
                db.SaveChanges();
            }
            if (exdata != null)
            {
                db.orders.Remove(exdata);
                db.SaveChanges();
            }

           
            
           
            return RedirectToAction("Orders");



             
        }















    }
}
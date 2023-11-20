using login.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Customer_Index()
        {
            var db = new crudEntities1();


            User loggedInUser = (User)Session["loggedInCustomer"];

            
            if (loggedInUser != null && loggedInUser.role == "Customer")
            {
               
                ViewBag.user_name = loggedInUser.user_name;
            }



            var data = db.Products.ToList();
           
           

            return View(data);
        }


        
        public ActionResult Viewcart()
        {
            List<item> cart = (List<item>)Session["cart"];

            
            return View(cart);

            
        }






        public ActionResult AddtoCart(int productId, int quantity)
        {
           
            List<item> cart = (List<item>)Session["cart"] ?? new List<item>();

            
            int index = cart.FindIndex(item => item.Product.id == productId);

            if (index != -1)
            {
                
                cart[index].quantity += quantity;
            }
            else
            {
                var db = new crudEntities1();

                
                var product = db.Products.Find(productId);

                if (product == null)
                {
                    
                    return HttpNotFound();
                }

                
                cart.Add(new item { Product = product, quantity = quantity });
            }

           
            Session["cart"] = cart;

          
            return RedirectToAction("Viewcart");
        }

        public ActionResult RemoveFromCart(int productId)
        {
           
            List<item> cart = (List<item>)Session["cart"] ?? new List<item>();

         
            int index = cart.FindIndex(item => item.Product.id == productId);

    
            if (index != -1)
            {
                cart.RemoveAt(index);

               
                Session["cart"] = cart;
            }

       
            return RedirectToAction("Viewcart");
        }







public ActionResult Order()
        {
            


            User loggedInUser = (User)Session["loggedInCustomer"];
            List<item> cart = (List<item>)Session["cart"];

    if (cart == null || !cart.Any())
    {
        return RedirectToAction("Viewcart");
    }


            var customerName = loggedInUser.user_name;
            using (var db = new crudEntities1())
    {
        var orders = cart.Select(cartItem => new login.EF.order
        {
            CustomerName = customerName,
            ProductId = cartItem.Product.id,
            ProductName = cartItem.Product.name,
            Quantity = cartItem.quantity,
            TotalPrice = cartItem.Product.price * cartItem.quantity,
            OrderDate = DateTime.Now,
            Status = "Ordered"
        });

        db.orders.AddRange(orders);
        db.SaveChanges();
                Session["cart"] = null;
            }

   

    return View("Viewcart");
}




        public ActionResult Logout()
        {
            
            Session.Clear();

            
            return RedirectToAction("Login", "Login");
        }




    }
}


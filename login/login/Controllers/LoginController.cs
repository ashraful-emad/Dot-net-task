using login.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {


            var db = new crudEntities1();
           var loginUser = db.Users.Where(a => a.user_name.Equals(user.user_name)
                                       && a.password.Equals(user.password)).FirstOrDefault();

            Session["loggedInCustomer"] = loginUser;



            if (loginUser != null)
                    {
                        if (loginUser.role == "Admin")
                        {
                            return RedirectToAction("Index", "Category");
                        }
                        else if (loginUser.role == "Customer")
                        {
                            return RedirectToAction("Customer_Index", "Customer");
                        }
                        //  Session["UserID"] = obj.Id.ToString();
                        //  Session["UserName"] = obj.UserName.ToString();
                        // return RedirectToAction("Dashboard");
                    }
                    else 
                    {
                ViewBag.Message = "Invalid username or password";

            }
                
               

           
            return View();
        }
       




        [HttpGet]
        public ActionResult signup() {
        
        
        
        return View();
        }

        [HttpPost]
        public ActionResult signup(User user)
        {

            var db = new crudEntities1();
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }




        public ActionResult Logout()
        {
            // Clear the session
            Session.Clear();

            // Redirect to the login page
            return RedirectToAction("Login", "Login");
        }



    }
}
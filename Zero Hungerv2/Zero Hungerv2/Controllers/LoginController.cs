using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hungerv2.EF;

namespace Zero_Hungerv2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user user)
        {


            var db = new zero_hungerEntities();
            var loginUser = db.users.Where(a => a.username.Equals(user.username)
                                        && a.password.Equals(user.password)).FirstOrDefault();

            Session["logged"] = loginUser;



            if (loginUser != null)
            {
                if (loginUser.role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (loginUser.role == "Restaurant")
                {
                    return RedirectToAction("Index", "Res");
                }
                else if (loginUser.role == "Employee")
                {
                    return RedirectToAction("Index", "Employee");
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
        public ActionResult Signup()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Signup(user user)
        {
            var db = new zero_hungerEntities();
            db.users.Add(user);
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
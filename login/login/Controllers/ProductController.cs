using login.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Product_index()
        {
            var db = new crudEntities1();
            var data = db.Products.ToList();

            return View(data);
        }


        [HttpGet]
        public ActionResult Product_add()
        {
            var db = new crudEntities1();
            var cate = db.Categories.ToList();
            ViewBag.catelist = new SelectList(cate, "id", "name");

            return View();

        }
        [HttpPost]
        public ActionResult Product_add(Product p)
        {
            var db = new crudEntities1();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Product_index");
        }

        public ActionResult product_delete(int id)
        {

            var db = new crudEntities1();
            var exdata = db.Products.Find(id);
            db.Products.Remove(exdata);
            db.SaveChanges();
            return RedirectToAction("Product_index");
        }





        [HttpGet]
        public ActionResult Edit_product(int id)
        {
            var db = new crudEntities1();
            var data = (from c in db.Products
                        where c.id == id
                        select c).SingleOrDefault();

            var cate = db.Categories.ToList();
            ViewBag.catelist = new SelectList(cate, "id", "name");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit_product(Product c)
        {
            var db = new crudEntities1();
            var exdata = db.Products.Find(c.id);
            exdata.name = c.name;
            exdata.cateid = c.cateid;
            exdata.price = c.price;


            db.SaveChanges();
            return RedirectToAction("Product_index");

        }





    }
}
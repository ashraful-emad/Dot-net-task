using crud.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Product_index()
        {
            var db = new productEntities();
            var data= db.products.ToList();
       
            return View(data);
        }


        [HttpGet]
        public ActionResult Product_add()
        {
            var db = new productEntities();
            var cate = db.categorizes.ToList();
            ViewBag.catelist = new SelectList(cate, "id", "name");

            return View();
            
        }
        [HttpPost]
        public ActionResult Product_add(product p ) { 
        var db = new productEntities();
            db.products.Add( p );
        db.SaveChanges();
        return RedirectToAction("Product_index");
        }

        public ActionResult product_delete(int id)
        {

            var db = new productEntities();
            var exdata = db.products.Find(id);
            db.products.Remove(exdata);
            db.SaveChanges();
            return RedirectToAction("Product_index");
        }





        [HttpGet]
        public ActionResult Edit_product(int id)
        {
            var db = new productEntities();
            var data = (from c in db.products
                        where c.id == id
                        select c).SingleOrDefault();

            var cate = db.categorizes.ToList();
            ViewBag.catelist = new SelectList(cate, "id", "name");
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit_product(product c)
        {
            var db = new productEntities();
            var exdata = db.products.Find(c.id);
            exdata.name = c.name;
            exdata.price = c.price;
            exdata.cateid = c.cateid;


            db.SaveChanges();
            return RedirectToAction("Product_index");

        }





    }
}
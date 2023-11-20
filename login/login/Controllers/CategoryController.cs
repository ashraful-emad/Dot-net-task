using login.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            var db = new crudEntities1();
            var data = db.Categories.ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult create(Category c)
        {
            var db = new crudEntities1();
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var db = new crudEntities1();
            var exdata = db.Categories.Find(id);
            db.Categories.Remove(exdata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new crudEntities1();
            var data = (from c in db.Categories
                        where c.id == id
                        select c).SingleOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Category c)
        {
            var db = new crudEntities1();
            var exdata = db.Categories.Find(c.id);
            exdata.name = c.name;
            db.SaveChanges();
            return RedirectToAction("Index");

        }





    }
}
using crud.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud.Controllers
{
    public class NewController : Controller
    {
        // GET: New
        public ActionResult Index()
        {
            var db = new productEntities();
            var data=db.categorizes.ToList();
            return View(data);
        }


        [HttpGet]
        public ActionResult create() {
        return View();
        
        }

        [HttpPost]
        public ActionResult create(categorize c)
        {
            var db = new productEntities();
            db.categorizes.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {

            var db = new productEntities();
            var exdata = db.categorizes.Find(id);
            db.categorizes.Remove(exdata);
            db.SaveChanges();
            return RedirectToAction("Index,New");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new productEntities();
            var data = (from c in db.categorizes
                        where c.id == id
                        select c).SingleOrDefault();  
            return View(data);  
        }

        [HttpPost]
        public ActionResult Edit(categorize c)
        {
            var db = new productEntities();
            var exdata = db.categorizes.Find(c.id);
            exdata.name = c.name;
            db.SaveChanges();
            return RedirectToAction("Index");

        }





    }
}
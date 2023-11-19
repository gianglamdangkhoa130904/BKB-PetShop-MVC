using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;

namespace PetShopWebBKB.Controllers
{
    public class MenuController : Controller
    {
        PetShopEntities database = new PetShopEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View(database.Menus.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Menu cate)
        {
            try
            {
                database.Menus.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Edit(int id)
        {
            return View(database.Menus.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Menu cate)
        {
            Menu a = database.Menus.Where(s => s.ID == id).FirstOrDefault();
            database.Menus.Remove(a);
            database.Menus.Add(cate);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Menus.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Menu cate)
        {
            try
            {
                cate = database.Menus.Where(s => s.ID == id).FirstOrDefault();
                database.Menus.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
    }
}
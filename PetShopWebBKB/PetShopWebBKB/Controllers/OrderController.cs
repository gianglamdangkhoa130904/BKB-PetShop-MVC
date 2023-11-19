using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;

namespace PetShopWebBKB.Controllers
{
    public class OrderController : Controller
    {
        PetShopEntities database = new PetShopEntities();
        public ActionResult Index()
        {
            return View(database.OrderProes.ToList());
        }
        public ActionResult Details(int id)
        {
            var order = from s in database.OrderDetails
                        where s.IDOrder == id
                        select s;
            return View(order);
        }
        public ActionResult Edit(int id)
        {
            return View(database.OrderDetails.Where(s => s.IDOrder == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, OrderDetail orderDetail)
        {
            try
            {
                database.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Edit");
            }
        }
        public ActionResult Delete(int id)
        {
            return View(database.OrderDetails.Where(s => s.IDOrder == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, OrderDetail orderDetail)
        {
            try
            {
                orderDetail = database.OrderDetails.Where(s => s.IDOrder == id).FirstOrDefault();
                database.OrderDetails.Remove(orderDetail);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
        public ActionResult AccountManagement(int? IDcus)
        {
            var productList = database.OrderProes.Where(x => x.IDcus == IDcus).ToList();
            return View(productList);
        }
    }
}
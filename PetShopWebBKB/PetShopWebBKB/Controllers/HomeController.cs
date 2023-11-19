using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;

namespace PetShopWebBKB.Controllers
{
    public class HomeController : Controller
    {
        PetShopEntities database = new PetShopEntities();
        public ActionResult Index(string menu)
        {
            if (menu == null)
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro);
                return View(productList);
            }
            else
            {
                var productList = database.Products.OrderByDescending(x => x.Price).Where(x => x.IDPro == menu);
                return View(productList);
            }

        }
        public ActionResult SearchByName(FormCollection form)
        {
            string name = form["searchname"];
            Session["Search"] = name;
            var pro = from s in database.Products
                      where s.NamePro.Contains(name)
                      select s;
            return View(pro);
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ProductCategory()
        {
            return View();
        }
        public ActionResult InforUser()
        {
            return View();
        }
        public ActionResult ProductDetail()
        {
            return View();
        }
        public ActionResult ShoppingCart()
        {
            return View();
        }
        public ActionResult ShoppedCart()
        {
            return View();
        }
    }
}
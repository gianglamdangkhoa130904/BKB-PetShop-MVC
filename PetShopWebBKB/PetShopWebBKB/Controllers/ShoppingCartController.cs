using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;

namespace PetShopWebBKB.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        PetShopEntities database = new PetShopEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return View("EmptyCart");
            }
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddToCart(int id)
        {
            var _pet = database.Products.FirstOrDefault(s => s.ID == id);
            if (_pet != null)
            {
                GetCart().Add_Product_Cart(_pet);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult Buy(int id)
        {
            var _pet = database.Products.FirstOrDefault(s => s.ID == id);
            if (_pet != null)
            {
                GetCart().Add_Product_Cart(_pet);
            }
            return RedirectToAction("CheckOut", "LoginCus");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pet = int.Parse(form["ID"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pet, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                total_quantity_item = cart.Total_quantity();
            }
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;
using System.IO;


namespace PetShopWebBKB.Controllers
{
    public class LoginCusController : Controller
    {
        PetShopEntities database = new PetShopEntities();
        // GET: LoginCus
        public ActionResult Index()
        {
            Customer a = Session["Thiscustomer"] as Customer;
            if (a == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("InforUser");
            }
        }
        [HttpPost]
        public ActionResult LoginAcountCus(Customer _user)
        {
            Customer check = database.Customers.Where(s => s.UserName == _user.UserName && s.PasswordCus == _user.PasswordCus).FirstOrDefault();
            AdminUser checkadmin = database.AdminUsers.Where(s => s.UserName == _user.UserName && s.PasswordCus == _user.PasswordCus).FirstOrDefault();
            if (checkadmin != null)
            {
                Session["Admin"] = checkadmin;
                Session["RoleAdmin"] = checkadmin.RoleAdmin;
                return RedirectToAction("Index", "Product");
            }
            if (check != null)
            {
                Session["Thiscustomer"] = check;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
        }
        public ActionResult InforUser()
        {
            Customer a = Session["ThisCustomer"] as Customer;
            var customer = from s in database.Customers
                           where s.ID == a.ID
                           select s;
            return View(customer);
        }
        public ActionResult RegisterCusForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCusForm(Customer _user)
        {
            Customer check_username = database.Customers.Where(s => s.UserName == _user.UserName).FirstOrDefault();
            bool failusername = check_username != null;
            if (_user.AddressCus == null || _user.PhoneNumber == null || _user.Email == null || _user.DateOfBirth == null || _user.NameCustomer == null)
            {
                ViewBag.ErrorNotEnough = "Bạn chưa điền đầy đủ thông tin!";
                return View();
            }
            bool faildatebirth = DateTime.Now.Year - _user.DateOfBirth.Value.Year <= 12;
            if (failusername)
            {
                ViewBag.ErrorUserName = "Tên đăng nhập đã tồn tại";
                return View();
            }
            if (faildatebirth)
            {
                ViewBag.ErrorDateofBirth = "Độ tuổi không hợp lệ";
                return View();
            }
            if (faildatebirth == false && failusername == false)
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                database.Customers.Add(_user);
                database.SaveChanges();
                return RedirectToAction("Index", "LoginCus");
            }
            else
            {
                ViewBag.ErrorRegisterForm = "Thông tin chưa hợp lệ";
                return View();
            }
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginCus");
        }
        public ActionResult ListAccount()
        {
            var list = database.Customers.ToList();
            return View(list);
        }
        public ActionResult CheckOut()
        {
            if (Session["Cart"] == null)
            {
                return View("EmptyCart");
            }
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            // Lấy giờ Việt Nam
            var second = DateTime.UtcNow.Ticks;
            DateTime b = new DateTime(second + 252000000000);

            Customer a = Session["Thiscustomer"] as Customer;
            if (a != null)
            {
                Cart cart = Session["Cart"] as Cart;
                OrderPro _order = new OrderPro();
                _order.DateOrder = b.ToString();
                _order.AddressDelivery = a.AddressCus;
                _order.IDcus = a.ID;
                _order.Total = cart.Total_money();
                database.OrderProes.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.IDOrder = _order.ID;
                    _order_detail.ImageOrder = item._pro.ImagePro;
                    _order_detail.IDPro = item._pro.ID;
                    _order_detail.UnitPrice = (double)cart.Total_money();
                    _order_detail.Quantity = item._quantity;
                    _order_detail.NameCustomer = a.NameCustomer;
                    _order_detail.AddressCus = a.AddressCus;
                    _order_detail.Email = a.Email;
                    _order_detail.PhoneNumber = a.PhoneNumber;
                    database.OrderDetails.Add(_order_detail);
                    foreach (var pro in database.Products.Where(s => s.ID == _order_detail.IDPro))
                    {
                        pro.SoldQuantity += item._quantity;
                        pro.RemainQuantity -= item._quantity;
                    }
                }
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Checkout_Success", "LoginCus");
            }
            else
            {
                return RedirectToAction("Index", "LoginCus");
            }
        }
        public ActionResult Receipt()
        {
            Customer a = Session["ThisCustomer"] as Customer;
            var receipt = database.OrderProes.Where(s => s.IDcus == a.ID).ToList();
            return View(receipt);
        }
        public ActionResult DeleteCus(int id)
        {
            return View(database.Customers.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult DeleteCus(int id, Customer customer)
        {
            try
            {
                customer = database.Customers.Where(s => s.ID == id).FirstOrDefault();
                database.Customers.Remove(customer);
                database.SaveChanges();
                return RedirectToAction("ListAccount", "LoginCus");
            }
            catch
            {
                return Content("Không thể xóa tài khoản này vì người dùng còn dùng để mua hàng!");
            }
        }
        public ActionResult Details(int id)
        {
            var order = from s in database.OrderDetails
                        where s.IDOrder == id
                        select s;
            return View(order);
        }
        public ActionResult Checkout_Success()
        {
            return View();
        }
    }
}
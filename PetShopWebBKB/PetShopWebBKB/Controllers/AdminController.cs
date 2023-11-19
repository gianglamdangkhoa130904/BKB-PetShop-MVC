using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;
namespace PetShopWebBKB.Controllers
{
    public class AdminController : Controller
    {
        PetShopEntities database = new PetShopEntities();
        // GET: Admin
        public ActionResult Index()
        {
            AdminUser a = Session["Admin"] as AdminUser;
            if (a == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (a.RoleAdmin == "Manager")
            {
                var user = from s in database.AdminUsers
                           where s.RoleAdmin != "Manager" select s;
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdminUser cate)
        {
            AdminUser check = (from s in database.AdminUsers
                           where s.UserName == cate.UserName
                           select s).FirstOrDefault();
            if (cate.UserName == null || cate.PasswordCus == null)
            {
                ViewBag.ErrorCreate = "Nhập thiếu thông tin";
                return View(cate);
            }
            if (check != null)
            {
                ViewBag.ErrorUsername = "Người dùng đã tồn tại";
                return View(cate);
            }
            else
            {
                cate.RoleAdmin = "Employee";
                database.AdminUsers.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index","Admin");
            }
        }
        public ActionResult Edit(int id)
        {
            return View(database.AdminUsers.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, AdminUser cate)
        {
            AdminUser check = database.AdminUsers.Where(s => s.UserName == cate.UserName).FirstOrDefault();
            if (cate.UserName == null || cate.PasswordCus == null || cate.RoleAdmin == null)
            {
                ViewBag.ErrorNotEnough = "Chưa nhập đủ thông tin";
                return View(cate);
            }
            if (cate.UserName.Count() >= 30)
            {
                ViewBag.ErrorLongUsername = "Tên đăng nhập quá dài";
                return View(cate);
            }
            if (cate.PasswordCus.Count() >= 30)
            {
                ViewBag.ErrorLongPassword = "Mật khẩu quá dài";
                return View(cate);
            }
            if (check != null)
            {
                ViewBag.ErrorUsername = "Người dùng đã tồn tại";
                return View(cate);
            }
            if (cate.RoleAdmin.Count() == 7 || cate.RoleAdmin.Count() >=30)
            {
                ViewBag.ErrorManager = "Role Admin chưa hợp lý";
                return View(cate);
            }
            else
            {
                AdminUser a = database.AdminUsers.Where(s => s.ID == id).FirstOrDefault();
                database.AdminUsers.Remove(a);
                database.AdminUsers.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            } 
        }
        public ActionResult Delete(int id)
        {
            return View(database.AdminUsers.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, AdminUser cate)
        {
            try
            {
                cate = database.AdminUsers.Where(s => s.ID == id).FirstOrDefault();
                database.AdminUsers.Remove(cate);
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
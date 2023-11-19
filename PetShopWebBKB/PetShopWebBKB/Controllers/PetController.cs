using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PetShopWebBKB.Models;

namespace PetShopWebBKB.Controllers
{
    public class PetController : Controller
    {
        PetShopEntities database = new PetShopEntities();
        // GET: Pet
        public ActionResult Index(string menu)
        {
            if (menu == null)
            {
                var productList = database.Pets.OrderByDescending(x => x.NamePet);
                return View(productList);
            }
            else
            {
                var productList = database.Pets.OrderByDescending(x => x.Price).Where(x => x.IDPet == menu);
                return View(productList);
            }

        }
        public ActionResult Create()
        {
            Pet pet = new Pet();
            return View(pet);
        }
        [HttpPost]
        public ActionResult Create(Pet petcate)
        {
            try

            {
                if (petcate.UploadImage != null)

                {
                    string extent = Path.GetExtension(petcate.UploadImage.FileName);

                    string filename = Path.GetFileNameWithoutExtension(petcate.UploadImage.FileName);

                    filename = filename + extent;

                    petcate.ImagePet = "~/Content/images/" + filename;

                    petcate.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }

                database.Pets.Add(petcate);
                database.SaveChanges();
                return RedirectToAction("Index");

            }

            catch

            {
                return View();
            }
        }
        public ActionResult SelectIDPet()
        {
            Menu se_cate = new Menu();

            se_cate.ListMenu = database.Menus.ToList<Menu>();

            return PartialView(se_cate);
        }
        public PartialViewResult TopPetPartial(string menu)
        {
            if (menu == null)
            {
                var productList = database.Pets.OrderByDescending(x => x.NamePet);
                return PartialView(productList);
            }
            else
            {
                var productList = database.Pets.OrderByDescending(x => x.NamePet).Where(x => x.IDPet == menu);
                return PartialView(productList);
            }
        }
        public PartialViewResult DogPartial(string menu)
        {
            if (menu == null)
            {
                var productList = database.Pets.OrderByDescending(x => x.NamePet);
                return PartialView(productList);
            }
            else
            {
                var productList = database.Pets.OrderByDescending(x => x.NamePet).Where(x => x.IDPet == menu);
                return PartialView(productList);
            }
        }
    }
}
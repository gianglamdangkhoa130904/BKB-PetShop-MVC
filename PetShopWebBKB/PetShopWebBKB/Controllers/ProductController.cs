using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using PetShopWebBKB.Models;
using PagedList;
namespace PetShopWebBKB.Controllers
{
    
    public class ProductController : Controller
    {
        // GET: Product
        PetShopEntities database = new PetShopEntities();
        // GET: Pet
        public ActionResult Index(string menu)
        {
            if (menu == null)
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro);
                return View(productList);
            }
            else
            {
                var productList = database.Products.OrderByDescending(x => x.Price).Where(x => x.Menu.NameID == menu);
                return View(productList);
            }
        }
        public ActionResult Details(int id)
        {
            var detailsPet = from s in database.Products where s.ID == id select s;
            return View(detailsPet.Single());
        }
        public ActionResult Detail(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            List<Menu> listMenu = database.Menus.ToList();
            List<Fur> listFur = database.Furs.ToList();
            List<Size> listSize = database.Sizes.ToList();
            List<Origin> listOrigin = database.Origins.ToList();
            List<Popular> listPopular = database.Populars.ToList();
            ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
            ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
            ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
            ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
            ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Product petcate)
        {
            Product a = database.Products.Where(s => s.ID == id).FirstOrDefault();
            petcate.RemainQuantity = a.RemainQuantity;
            petcate.SoldQuantity = a.SoldQuantity;
            petcate.ImagePro = a.ImagePro;
            List<Menu> listMenu = database.Menus.ToList();
            List<Fur> listFur = database.Furs.ToList();
            List<Size> listSize = database.Sizes.ToList();
            List<Origin> listOrigin = database.Origins.ToList();
            List<Popular> listPopular = database.Populars.ToList();
            
            if (petcate.NamePro == null || petcate.Price == null || petcate.Age == null || petcate.DescriptionPro == null || petcate.Rating == null || petcate.RemainQuantity == null)
            {
                ViewBag.ErrorNotEnough = "Thông tin nhập chưa hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                return View(petcate);
            }
            if (petcate.Price <= 0 || petcate.Price >= 1000000000)
            {
                ViewBag.ErrorPrice = "Giá thú cưng không hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                return View(petcate);
            }
            if (petcate.NamePro.Count() >= 50)
            {
                ViewBag.ErrorName = "Tên thú cưng quá dài";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                return View(petcate);
            }
            if (petcate.Age <= 0 || petcate.Age >= 5)
            {
                ViewBag.ErrorAge = "Tuổi thú cưng chưa hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                return View(petcate);
            }
            if (petcate.Rating <= 0 || petcate.Rating >= 6)
            {
                ViewBag.ErrorRating = "Đánh giá thú cưng chưa hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", 1);
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                return View(petcate);
            }
            if (petcate.RemainQuantity <= 0 || petcate.RemainQuantity >= 100)
            {
                ViewBag.ErrorQuantity = "Số lượng thú cưng không hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                return View(petcate);
            }
            else
            {
                try
                {
                    ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", 1);
                    ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", 1);
                    ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", 1);
                    ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", 1);
                    ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", 1);
                    database.Products.Remove(database.Products.Where(s => s.ID == id).FirstOrDefault());
                    database.Products.Add(petcate);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.ErrorEdit = "Sản phẩm đã được mua không thể chỉnh sửa";
                    ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                    ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                    ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                    ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                    ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                    return View(petcate);
                }
            }
        }
        public ActionResult Create()
        {
            List<Menu> listMenu = database.Menus.ToList();
            List<Fur> listFur = database.Furs.ToList();
            List<Size> listSize = database.Sizes.ToList();
            List<Origin> listOrigin = database.Origins.ToList();
            List<Popular> listPopular = database.Populars.ToList();
            ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
            ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
            ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
            ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
            ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
            Product pet = new Product();
            return View(pet);
        }
        [HttpPost]
        public ActionResult Create(Product petcate)
        {
            List<Menu> listMenu = database.Menus.ToList();
            List<Fur> listFur = database.Furs.ToList();
            List<Size> listSize = database.Sizes.ToList();
            List<Origin> listOrigin = database.Origins.ToList();
            List<Popular> listPopular = database.Populars.ToList();
            if (petcate.NamePro == null || petcate.Price == null || petcate.Age == null || petcate.DescriptionPro == null || petcate.Rating == null || petcate.RemainQuantity == null)
            {
                ViewBag.ErrorNotEnough = "Thông tin nhập chưa hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                Product pet = new Product();
                return View(pet);
            }
            if (petcate.Price <= 0 || petcate.Price >= 1000000000)
            {
                ViewBag.ErrorPrice = "Giá thú cưng không hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                Product pet = new Product();
                return View(pet);
            }
            if (petcate.NamePro.Count() >= 50)
            {
                ViewBag.ErrorName = "Tên thú cưng quá dài";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                Product pet = new Product();
                return View(pet);
            }
            if (petcate.Age <= 0 || petcate.Age >= 5)
            {
                ViewBag.ErrorAge = "Tuổi thú cưng chưa hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                Product pet = new Product();
                return View(pet);
            }
            if (petcate.Rating <= 0 || petcate.Rating >= 6)
            {
                ViewBag.ErrorRating = "Đánh giá thú cưng chưa hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", 1);
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                Product pet = new Product();
                return View(pet);
            }
            if (petcate.RemainQuantity <= 0 || petcate.RemainQuantity >= 100)
            {
                ViewBag.ErrorQuantity = "Số lượng thú cưng không hợp lệ";
                ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                Product pet = new Product();
                return View(pet);
            }
            else
            {
                try
                {
                    if (petcate.UploadImage != null)
                    {
                        string extent = Path.GetExtension(petcate.UploadImage.FileName);

                        string filename = Path.GetFileNameWithoutExtension(petcate.UploadImage.FileName);

                        filename += extent;

                        petcate.ImagePro = "~/Content/images/" + filename;

                        petcate.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                    else
                    {
                        ViewBag.ErrorImage = "Vui lòng thêm hình ảnh";
                        ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                        ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                        ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                        ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                        ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                        Product pet = new Product();
                        return View(pet);
                    }
                    ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", 1);
                    ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", 1);
                    ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", 1);
                    ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", 1);
                    ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", 1);
                    petcate.SoldQuantity = 0;
                    database.Products.Add(petcate);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.listMenu = new SelectList(listMenu, "ProductID", "NameID", "");
                    ViewBag.listFur = new SelectList(listFur, "FurID", "FurType", "");
                    ViewBag.listSize = new SelectList(listSize, "SizeID", "SizeType", "");
                    ViewBag.listOrigin = new SelectList(listOrigin, "OriginID", "Origination", "");
                    ViewBag.listPopular = new SelectList(listPopular, "PopularID", "Popularity", "");
                    Product pet = new Product();
                    return View(pet);
                }
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product cate)
        {
            try
            {
                cate = database.Products.Where(s => s.ID == id).FirstOrDefault();
                database.Products.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
        public ActionResult SelectIDProduct()
        {
            Menu se_cate = new Menu() { ListMenu = database.Menus.ToList<Menu>() };
            return PartialView(se_cate);
        }
        public ActionResult SelectFur()
        {
            Fur se_cate =  new Fur() { ListFur = database.Furs.ToList<Fur>() };
            return PartialView(se_cate);
        }
        public ActionResult SelectSize()
        {
            Size se_cate = new Size() { ListSize = database.Sizes.ToList<Size>() };
            return PartialView(se_cate);
        }
        public ActionResult SelectOrigin()
        {
            Origin se_cate = new Origin() { ListOrigin = database.Origins.ToList<Origin>() };
            return PartialView(se_cate);
        }
        public ActionResult SelectPopular()
        {
            Popular se_cate = new Popular() { ListPopular = database.Populars.ToList<Popular>() };
            return PartialView(se_cate);
        }

        public PartialViewResult TopPetPartial()
        {
            var productList = from s in  database.Products where s.Price >= (decimal)10000000 select s;
            return PartialView(productList);
        }
        public PartialViewResult DogPartial()
        {
            var productList = from s in database.Products where s.IDPro == "Dog" select s;
            return PartialView(productList);
        }
        public PartialViewResult CatPartial()
        {
            var productList = from s in database.Products where s.IDPro == "Cat" select s;
            return PartialView(productList);
        }
        public PartialViewResult HamsterPartial()
        {
            var productList = from s in database.Products where s.IDPro == "Hamster" select s;
            return PartialView(productList);
        }
        public PartialViewResult RabbitPartial()
        {
            var productList = from s in database.Products where s.IDPro == "Rabbit" select s;
            return PartialView(productList);
        }
        public PartialViewResult GroupDogProduct()
        {
            var catelist = from s in database.Products
                           where s.IDPro == "Dog"
                           select s;
            return PartialView(catelist);
        }
        public PartialViewResult GroupCatProduct()
        {
            var catelist = from s in database.Products
                           where s.IDPro == "Cat"
                           select s;
            return PartialView(catelist);
        }

        public ActionResult ProductMenu()
        {
            return View();
        }
        public PartialViewResult MenuPartial(string typepro, string sort, int? page, string newpro)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (typepro == null)
            {
                if (sort == "asc")
                {
                    var productList = database.Products.OrderBy(x => x.Price).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    ViewBag.sort = sort;
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
                if (sort == "desc")
                {
                    var productList = database.Products.OrderByDescending(x => x.Price).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    ViewBag.sort = sort;
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
                if (newpro == "yes")
                {
                    var productList = database.Products.OrderByDescending(x => x.Rating).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var productList = database.Products.ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
            }
            else
            {
                if (sort == "asc")
                {
                    var productList = database.Products.Where(x => x.Menu.ProductID == typepro).OrderBy(x => x.Price).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    ViewBag.typepro = typepro;
                    ViewBag.sort = sort;
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
                if (sort == "desc")
                {
                    var productList = database.Products.Where(x => x.Menu.ProductID == typepro).OrderByDescending(x => x.Price).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    ViewBag.typepro = typepro;
                    ViewBag.sort = sort;
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
                if (newpro == "yes")
                {
                    var productList = database.Products.Where(x => x.Menu.ProductID == typepro).OrderByDescending(x => x.Rating).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    ViewBag.typepro = typepro;
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    var productList = database.Products.Where(x => x.Menu.ProductID == typepro).ToList();
                    PagedList<Product> productultra = new PagedList<Product>(productList, pageNum, pageSize);
                    ViewBag.typepro = typepro;
                    ViewBag.sort = sort;
                    return PartialView(productList.ToPagedList(pageNum, pageSize));
                }
            }
        }

        public ActionResult DogView()
        {
            var pro = from s in database.Products 
                      where s.Menu.ProductID == "Dog" 
                      select s;
            return View(pro);
        }
        public ActionResult CatView()
        {
            var pro = from s in database.Products
                      where s.Menu.ProductID == "Cat"
                      select s;
            return View(pro);
        }
        public ActionResult HamsterView()
        {
            var pro = from s in database.Products
                      where s.Menu.ProductID == "Hamster"
                      select s;
            return View(pro);
        }
        public PartialViewResult FlashSale()
        {
            var pro = from s in database.Products
                      where s.SoldQuantity <= 10
                      select s;
            return PartialView(pro);
        }
        public ActionResult RabbitView()
        {
            var pro = from s in database.Products
                      where s.Menu.ProductID == "Rabbit"
                      select s;
            return View(pro);
        }

        public ActionResult QuantityManagement(string menu)
        {
            if (menu == null)
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro);
                return View(productList);
            }
            else
            {
                var productList = database.Products.OrderByDescending(x => x.Price).Where(x => x.Menu.NameID == menu);
                return View(productList);
            }
        }

        [HttpPost]
        public ActionResult Update_Remain_Quantity(FormCollection form)
        {
            int pro = int.Parse(form["ID"]);
            int quantity = int.Parse(form["RemainQuantity"]);
            if (quantity >= 0 && quantity <= 10000)
            {
                foreach (var product in database.Products.Where(s => s.ID == pro))
                {
                    product.RemainQuantity += quantity;
                }
                database.SaveChanges();
                return RedirectToAction("QuantityManagement", "Product");
            }
            else
            {
                ViewBag.ErrorQuantity = "Bạn bị khùng hả nhập dữ vậy!";
                return RedirectToAction("QuantityManagement", "Product");
            }
        }
    }
}
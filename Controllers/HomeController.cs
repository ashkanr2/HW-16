using Microsoft.AspNetCore.Mvc;
using My_App.Store.Demo.Models;
using Online_shop.Models;
using Online_shop.Models.ViewModels;
using Online_shop.Services;

namespace Online_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServices _services;

        public HomeController(IServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
          
            ViewBag.id = CurrentUser.Id;
            return View();
        }

        [HttpPost]
        public IActionResult LogInUser(string email, string password)
        {
            if (email != null)
            {
                CurrentUser.Id = int.Parse(email);
                ViewBag.Message = "شما با موفقیت وارد شدید!";
                ViewBag.id = CurrentUser.Id;
            }
            else
            {
                ViewBag.Message = "ورود ناموفق دوباره تلاش کنید";
            }
            return View("Index");
        }
     
        [HttpPost]
        public IActionResult LogOut()
        {
           CurrentUser.Id = 0;
          return View("Index");
        }


        [HttpGet]
        [HttpPost]
        public IActionResult ProductsList(string? search)
        {
            ViewBag.id = CurrentUser.Id;
            var result = _services.SearchedProductList(search);
            if (search != null) ViewBag.search = search;
            ViewBag.currentUser = CurrentUser.Id!;
            return View(result);

        }
        public IActionResult Delete(string id)
        {
            ViewBag.id = CurrentUser.Id;
            _services.Delete(int.Parse(id));
            return RedirectToAction("ProductsList");
        }

        public IActionResult CreateProduct()
        {
            ViewBag.id = CurrentUser.Id;
            return View(new CreateProductViewModel());
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel product)
        {
            ViewBag.id = CurrentUser.Id;
            _services.CreateProduct(product);
            return RedirectToAction("ProductsList");
        }

        public IActionResult EditProduct(string id)
        {
            ViewBag.id = CurrentUser.Id;
            return View(_services.GetById(int.Parse(id)));
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            ViewBag.id = CurrentUser.Id;
            _services.Edit(product);
            return RedirectToAction("ProductsList");
        }

        public IActionResult AddToBasket(string id)
        {
            ViewBag.id = CurrentUser.Id;
            _services.AddToBasket(int.Parse(id));

          return  RedirectToAction("ProductsList");
        }

        public IActionResult BasketList()
        {
            ViewBag.id = CurrentUser.Id;
            return View(_services.GetBasketList());
        }
        public IActionResult FinalPurchase()
        {
            ViewBag.id = CurrentUser.Id;
            _services.FinalPurchase();
            return RedirectToAction("ProductsList");
        }
        public IActionResult FactorsList()
        {
            ViewBag.id = CurrentUser.Id;
            ViewBag.message = CurrentUser.Id;
            return View(_services.FactorsList());
        }
        public IActionResult Factor(string id)
        {

            ViewBag.id = CurrentUser.Id;
            return View(_services.GetFactorsDetails(int.Parse(id)));
        }





    }
}
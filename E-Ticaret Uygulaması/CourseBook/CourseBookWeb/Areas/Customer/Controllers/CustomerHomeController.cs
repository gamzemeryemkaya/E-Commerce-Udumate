using CourseBook.DataAccess;
using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using CourseBook.Models.ViewModels;
using CourseBook.Utility;
using CourseBookWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerHomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public IndexViewModel IndexViewModel { get; set; }

        public CustomerHomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CourseFormatType");
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
            ViewBag.Categories = categories;
            return View(productList);
        }

        public IActionResult FilterByCategory(int categoryId)
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(
                filter: p => p.CategoryId == categoryId,
                includeProperties: "Category,CourseFormatType");

            ViewBag.Categories = _unitOfWork.Category.GetAll();
            return View("Index", productList);
        }

        public IActionResult CourseDetails(int productId)
        {
            

            var IndexViewModel = new IndexViewModel()
            {
                ProductId = productId,
                Products = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CourseFormatType"),
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == productId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderId == productId, includeProperties: "Product"),
                Courses = _unitOfWork.Course.GetAll(filter: c => c.ProductId == productId, includeProperties: "Product")

            };
           
   
           
            ViewBag.i = productId;
            return View(IndexViewModel);

        }

     
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CourseFormatType"),
            };

            return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


            if (cartFromDb == null)
            {

                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}

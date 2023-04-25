using Microsoft.AspNetCore.Mvc;

namespace CourseBookWeb.Areas.Admin.Controllers
{
    public class ProductyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

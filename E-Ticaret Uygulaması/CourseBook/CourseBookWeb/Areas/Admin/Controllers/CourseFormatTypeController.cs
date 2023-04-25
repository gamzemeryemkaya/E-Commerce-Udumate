using CourseBook.DataAccess;
using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using CourseBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CourseFormatTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CourseFormatTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CourseFormatType> objCourseFormatTypeList = _unitOfWork.CourseFormatType.GetAll();
            return View(objCourseFormatTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseFormatType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CourseFormatType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CourseFormatType created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CourseFormatTypeFromDbFirst = _unitOfWork.CourseFormatType.GetFirstOrDefault(u => u.Id == id);

            if (CourseFormatTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CourseFormatTypeFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseFormatType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CourseFormatType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CourseFormatType updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CourseFormatTypeFromDbFirst = _unitOfWork.CourseFormatType.GetFirstOrDefault(u => u.Id == id);

            if (CourseFormatTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CourseFormatTypeFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CourseFormatType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CourseFormatType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CourseFormatType deleted successfully";
            return RedirectToAction("Index");

        }
    }
}

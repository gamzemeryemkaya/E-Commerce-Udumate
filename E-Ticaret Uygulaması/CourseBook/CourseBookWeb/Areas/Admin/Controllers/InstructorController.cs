using CourseBook.DataAccess;
using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using CourseBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace CourseBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;



        public InstructorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Instructor instructor = new();

            if (id == null || id == 0)
            {
                return View(instructor);
            }
            else
            {
                instructor = _unitOfWork.Instructor.GetFirstOrDefault(u => u.Id == id);
                return View(instructor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Instructor obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                if (obj.Id == 0)
                {
                    _unitOfWork.Instructor.Add(obj);
                    TempData["success"] = "Instructor created successfully";
                }
                else
                {
                    _unitOfWork.Instructor.Update(obj);
                    TempData["success"] = "Instructor updated successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var instructorList = _unitOfWork.Instructor.GetAll();
            return Json(new { data = instructorList });
        }

        //POST
        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Instructor.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Instructor.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
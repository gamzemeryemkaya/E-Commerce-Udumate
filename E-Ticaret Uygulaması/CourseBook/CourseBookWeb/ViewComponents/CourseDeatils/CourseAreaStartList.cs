//using CourseBook.DataAccess.Repository;
//using CourseBook.DataAccess.Repository.IRepository;
//using CourseBook.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CourseBookWeb.ViewComponents.CourseDeatils
//{
//    public class CourseAreaStartList : ViewComponent
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public CourseAreaStartList(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public IViewComponentResult Invoke(int productId)
//        {
//            Course course = _unitOfWork.Course.GetFirstOrDefault(u => u.Id == productId);
//            IEnumerable<Course> courseList = new List<Course> { course };
//            return View(courseList);
//        }

//    }
//}


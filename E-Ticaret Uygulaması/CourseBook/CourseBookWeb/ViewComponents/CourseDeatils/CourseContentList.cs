//using CourseBook.DataAccess.Repository.IRepository;
//using CourseBook.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CourseBookWeb.ViewComponents.CourseDeatils
//{
//    public class CourseContentList : ViewComponent
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public CourseContentList(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public IViewComponentResult Invoke()
//        {

//            IEnumerable<Course> courseList = _unitOfWork.Course.GetAll();
//            return View(courseList);
//        }

//    }
//}
   


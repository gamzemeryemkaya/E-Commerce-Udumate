using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.DataAccess.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Course obj)
        {
            _db.Courses.Update(obj);
        }
    }
}

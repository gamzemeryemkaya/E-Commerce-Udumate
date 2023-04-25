using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.DataAccess.Repository
{
    public class CourseFormatTypeRepository : Repository<CourseFormatType>, ICourseFormatTypeRepository
    {
        private ApplicationDbContext _db;

        public CourseFormatTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(CourseFormatType obj)
        {
            _db.CourseFormatTypes.Update(obj);
        }
    }
}

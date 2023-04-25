using CourseBook.DataAccess.Repository.IRepository;
using CourseBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.DataAccess.Repository
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        private ApplicationDbContext _db;

        public InstructorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Instructor obj)
        {
            _db.Instructors.Update(obj);
        }
    }
}

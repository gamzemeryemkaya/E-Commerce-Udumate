using CourseBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.DataAccess.Repository.IRepository
{
    public interface ICourseFormatTypeRepository : IRepository<CourseFormatType>
    {
        void Update(CourseFormatType obj);
    }
}

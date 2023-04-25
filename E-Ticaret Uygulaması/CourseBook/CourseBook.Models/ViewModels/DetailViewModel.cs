using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.Models.ViewModels
{
    public class DetailViewModel
    {

        public Product Product { get; set; }

        public IEnumerable<Course> Course { get; set; }
    }
}

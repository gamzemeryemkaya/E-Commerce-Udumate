using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.Models.ViewModels
{
    public class IndexViewModel
    {
        public int ProductId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public Course Course { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public Product Products { get; set; }
    }


}

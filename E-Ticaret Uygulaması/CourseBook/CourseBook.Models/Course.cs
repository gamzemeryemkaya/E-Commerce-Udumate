using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseBook.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string? Title { get; set; } 
        public double Price { get; set; } 
        public double CampaignPrice { get; set; }
        public string? Rating { get; set; }
        public string? TotalVideo { get; set; }
        public string? CourseTime { get; set; }
        public string? Title1 { get; set; }
        public string? Detail { get; set; }
        public string? Title2 { get; set; }
        public string? InfoTitle { get; set; }
        public string? InfoContent { get; set; }
        public string? InstructorName { get; set; }
        public string? InstructorStatus { get; set; }
        public string? InstructorImage { get; set; }
        public string? InstructorDescription { get; set; }

        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? PdfUrl { get; set; }

    }
}

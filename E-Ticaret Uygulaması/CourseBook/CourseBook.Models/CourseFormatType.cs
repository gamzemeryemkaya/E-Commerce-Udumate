using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourseBook.Models
{
    public class CourseFormatType
    {
            [Key]
            public int Id { get; set; }

            [Display(Name = "Course Format")]
            [Required]
            [MaxLength(50)]
            public string Name { get; set; }
    }
}

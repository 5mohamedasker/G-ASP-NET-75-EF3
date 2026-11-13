using EF_Assignment03.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assignment03.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CourseLevel Level { get; set; }
        public bool IsPublished { get; set; } 
        public DateTime? PublishedDate { get; set; }

        public int PublsherId { get; set; }
        public Instructor Publisher { get; set; }

        public ICollection<Enrollment> EnrolledStudents{ get; set; } = new List<Enrollment>();


        public int CategoryId { get; set; }
        public Category BelongCategory { get; set; }


    }
}

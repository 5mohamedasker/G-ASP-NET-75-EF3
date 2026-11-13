using EF_Assignment03.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assignment03.Models
{
    public class Enrollment
    {
        public DateTime EnrollmentDate { get; set; } 
        public double ProgressPercentage { get; set; } 
        public EnrollmentStatus Status { get; set; } 
        public double? FinalGrade { get; set; }

        public int StudentId { get; set; }
        public Student EnrolledStudent { get; set; }

        public int CourseId { get; set; }
        public Course EnrolledCourse { get; set; }
    }
}

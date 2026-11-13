using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_Assignment03.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ICollection<Enrollment> EnrolledCourses { get; set; } = new List<Enrollment>();
    }
}

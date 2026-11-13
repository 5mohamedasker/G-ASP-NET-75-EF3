using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_Assignment03.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public DateOnly JoinedDate { get; set; }

        public ICollection<Course> PublishedCourses { get; set; } = new List<Course>();
    }
}

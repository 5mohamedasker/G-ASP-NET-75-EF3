using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_Assignment03.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? ParentId { get; set; }
        public Category? ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public ICollection<Course> Courses  { get; set; } = new List<Course>();

    }
}

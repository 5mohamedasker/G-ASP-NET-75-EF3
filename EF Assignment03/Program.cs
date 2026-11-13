using EF_Assignment03.Contexts;
using EF_Assignment03.Enums;
using EF_Assignment03.Models;
using EF_Assignment03.Operations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EF_Assignment03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AssignmentDbContext dbContext = new AssignmentDbContext();

            OperationsClass.AddInstructor(dbContext, new Instructor { FullName = "Ali Esam", Email = "aliesam@gmail.com", Bio = "Course instructor", JoinedDate = new DateOnly(2023, 10, 23) });
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.AddCourse(dbContext, new Course { Title = "OOP", Price = 1200, Level = CourseLevel.Beginner, PublsherId = 20, CategoryId = 20 });
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.AddSeveralStudents(dbContext , new List<Student>()
                {
                    new Student{FullName = "Amira Reda",Email = "amira@gmail.com",DateOfBirth = new DateOnly(2011,11,6) },                                                                    
                    new Student{FullName = "Saad Yaser",Email = "saad@gmail.com",DateOfBirth = new DateOnly(2001,1,14) },                                                                    
                    new Student{FullName = "Mahmoud Ali",Email = "mahmoud@gmail.com",DateOfBirth = new DateOnly(2005,7,27) }                                                                    
                });
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.EnrollStudent(dbContext, 30,20);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.FindCourse(dbContext,10,"C#");
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.FilterCourses(dbContext,100,2000);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.PrintSubCategoriesWithParents(dbContext,20);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.PrintCourseInfo(dbContext,20);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.ChangeCoursePrice(dbContext,10,2000);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.UpdateProgress(dbContext,20,10,100,76);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.PublishCourse(dbContext,30);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.UnenrollStudent(dbContext,10,10);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.DeleteCategory(dbContext,20);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.TryDeleteCategory(dbContext,10);
            
            Console.WriteLine("\n========================\n========================");

            OperationsClass.DeleteCourse(dbContext,20);
            
            Console.WriteLine("\n========================\n========================");


        }
    }
}

using EF_Assignment03.Contexts;
using EF_Assignment03.Enums;
using EF_Assignment03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_Assignment03.Operations
{
    internal static class OperationsClass
    {

        // Add an instructor: Insert a new instructor and print the generated Id.
        public static void AddInstructor(AssignmentDbContext dbContext, Instructor instructor)
        {
            if (dbContext is null || instructor is null)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }
            if (string.IsNullOrWhiteSpace(instructor.FullName))
            {
                Console.WriteLine("You have to send a valid Instructor Full Name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(instructor.Email))
            {
                Console.WriteLine("You have to send a valid Instructor Email.");
                return;
            }
            if (instructor.JoinedDate > DateOnly.FromDateTime(DateTime.Today))
            {
                Console.WriteLine("You have to send a valid Instructor Joined Date.");
                return;
            }

            try
            {
                dbContext.Instructors.Add(instructor);
                dbContext.SaveChanges();
                Console.WriteLine($"Added successfully instructor with Id: {instructor.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Add a course: Insert a course for an existing instructor and an existing category.
        // Check that both exist first, otherwise print a message.
        public static void AddCourse(AssignmentDbContext dbContext, Course course)
        {
            if (dbContext is null || course is null)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }
            if (string.IsNullOrWhiteSpace(course.Title))
            {
                Console.WriteLine("You have to send a valid Course Title.");
                return;
            }
            if (course.Price < 0)
            {
                Console.WriteLine("You have to send a valid Course Price.");
                return;
            }

            if (course.CategoryId <= 0 || course.CategoryId == null || !dbContext.Categories.Any(C => C.Id == course.CategoryId))
            {
                Console.WriteLine("Please enter an existed CategoryId.");
                return;
            }

            if (course.PublsherId <= 0 || course.PublsherId == null || !dbContext.Instructors.Any(I => I.Id == course.PublsherId))
            {
                Console.WriteLine("Please enter an existed PublsherId.");
                return;
            }

            try
            {
                dbContext.Courses.Add(course);
                dbContext.SaveChanges();
                Console.WriteLine("Course added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Add several students: Insert three students using one AddRange and one SaveChanges. Print their generated Ids.
        public static void AddSeveralStudents(AssignmentDbContext dbContext, List<Student> students)
        {
            if (dbContext is null || students == null || students.Count == 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                dbContext.Students.AddRange(students);
                dbContext.SaveChanges();
                Console.WriteLine("Students added successfully.");

                foreach (var s in students)
                {
                    Console.WriteLine($"Student: {s.FullName}, Id: {s.Id}.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        // Enroll a student: The student and the course must exist, the course must be published,
        // and the student must not be enrolled already. Otherwise print why the enrollment was refused.
        public static void EnrollStudent(AssignmentDbContext dbContext, int studentId, int courseId)
        {
            if (dbContext is null)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            if (courseId <= 0)
            {
                Console.WriteLine($"There is no course with this Id: {courseId}");
                return;
            }

            if (studentId <= 0 || !dbContext.Students.Any(S => S.Id == studentId))
            {
                Console.WriteLine($"There is no student with this Id: {studentId}");
                return;
            }

            var course = dbContext.Courses.FirstOrDefault(C => C.Id == courseId);
            if (course is null)
            {
                Console.WriteLine($"There is no course with this Id: {courseId}");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine("Enrollment refused becouse course is not published.");
                return;
            }

            if (dbContext.Enrollments.Any(E => E.StudentId == studentId && E.CourseId == courseId))
            {
                Console.WriteLine("Enrollment refused becouse student is already enrolled in this course");
                return;
            }

            try
            {
                dbContext.Enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId, EnrollmentDate = DateTime.Now, ProgressPercentage = 0, Status = EnrollmentStatus.Active });
                dbContext.SaveChanges(); Console.WriteLine("Student enrolled successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Find a course: Get a course by Id using Find, then by title using FirstOrDefault. Print "not found" when the result is null.
        public static void FindCourse(AssignmentDbContext dbContext, int courseId, string courseTitle)
        {
            if (dbContext is null || courseId <= 0 || string.IsNullOrWhiteSpace(courseTitle))
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                var courseById = dbContext.Courses.Find(courseId);
                if (courseById is null)
                {
                    Console.WriteLine($"Ther is no course with this Id: {courseId}");
                }
                else
                {
                    Console.WriteLine($"Found by Id -- Id = {courseById.Id} ,Title = {courseById.Title}.");
                }

                var courseByTitle = dbContext.Courses.FirstOrDefault(C => C.Title == courseTitle);
                if (courseByTitle is null)
                {
                    Console.WriteLine($"Ther is no course with this Title: {courseTitle}");
                }
                else
                {
                    Console.WriteLine($"Found by Id -- Id = {courseByTitle.Id} ,Title = {courseByTitle.Title}.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        // Filter courses: Courses priced between two values, ordered by price descending. Print only the title, level and price.
        public static void FilterCourses(AssignmentDbContext dbContext, decimal min, decimal max)
        {
            if (dbContext is null || min >= max || min < 0 || max < 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                var courses = dbContext.Courses.Where(C => C.Price >= min && C.Price <= max).OrderByDescending(C => C.Price).Select(C => new { C.Title, C.Level, C.Price }).ToList();

                if (!courses.Any())
                {
                    Console.WriteLine($"Ther is no Courses between {min} and {max}");
                    return;
                }
       
                Console.WriteLine($"Courses prise between {min} and {max}");
                foreach (var C in courses)
                {
                    Console.WriteLine($"Title: {C.Title} - Level: {C.Level} - Price: {C.Price}");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Sub-categories: Print the direct sub-categories of a given category, then print the top - level categories.
        public static void PrintSubCategoriesWithParents(AssignmentDbContext dbContext ,int categoryId)
        {
            if (dbContext is null || categoryId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {

                var parentCategory = dbContext.Categories.FirstOrDefault(C => C.Id == categoryId);

                if (parentCategory is null)
                {
                    Console.WriteLine($"There is no Category with this Id {categoryId}");
                    return;
                }

                var subCategories = dbContext.Categories.Where(C => C.ParentId == parentCategory.Id);

                if (!subCategories.Any())
                {
                    Console.WriteLine($"Ther is no any Sub-Categories belongs this Category -- Id: {categoryId}, Name: {parentCategory.Name}");
                }
                else
                {
                    Console.WriteLine($"{parentCategory.Name}'s Sub-Categories.");
                    foreach (var S in subCategories)
                    {
                        Console.WriteLine($"Sub-category: {S.Name}");
                    }
                }

                var toplevelCategories = dbContext.Categories.Where(C => C.ParentId == null);
                Console.WriteLine("All Top-level categorioes");
                foreach (var top in toplevelCategories)
                {
                    Console.WriteLine($"{top.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Course roster: Print how many students are enrolled in a course, then their names.
        // Do it without joins: get the student Ids from the enrollments first, then get the students with Contains.
        public static void PrintCourseInfo(AssignmentDbContext dbContext, int courseId)
        {
            if (dbContext is null || courseId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {

                var course = dbContext.Courses.Where(E => E.Id == courseId);
                if (course is null)
                {
                    Console.WriteLine($"There is no course with this Id: {courseId}");
                    return;
                }

                var enrolledStudentsIds = dbContext.Enrollments.Where(E => E.CourseId == courseId).Select(E => E.StudentId).ToList();

                Console.WriteLine($"Enrolled students Count: {enrolledStudentsIds.Count}");

                if (!enrolledStudentsIds.Any())
                {
                    Console.WriteLine($"Ther is no any enrolled Students in this Course.");
                    return;
                }

                var students = dbContext.Students.Where(S => enrolledStudentsIds.Contains(S.Id));
                foreach (var S in students)
                {
                    Console.WriteLine(S.FullName);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }


        // Change a price: Load a course, change its price, and save.
        public static void ChangeCoursePrice(AssignmentDbContext dbContext, int courseId, decimal newPrice)
        {
            if (dbContext is null || courseId <= 0 || newPrice < 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }
            try
            {
                var course = dbContext.Courses.Find(courseId);
                if (course is null)
                {
                    Console.WriteLine($"Ther is no course with this Id: {courseId}.");
                    return;
                }
                course.Price = newPrice;
                dbContext.SaveChanges();
                Console.WriteLine("Course price updated successfully."); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Update progress: Find the enrollment by its composite key and set the new progress.
        // At 100 set the status to Completed. Reject values outside 0–100.
        public static void UpdateProgress(AssignmentDbContext dbContext, int studentId, int courseId, double progress,double? finalGrade = null)
        {
            if (dbContext is null)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }
            if (progress < 0 || progress > 100)
            {
                Console.WriteLine("Progress must be between 0 and 100.");
                return;
            }
            if (progress == 100)
            {
                if (finalGrade is null)
                {
                    Console.WriteLine("please Enter the Final Grade.");
                    return;
                }
                if (finalGrade < 0 || finalGrade > 100)
                {
                    Console.WriteLine("Final Grade must be between 0 and 100.");
                    return;
                }
            }
            if (studentId <= 0)
            {
                Console.WriteLine($"There is no student with this Id: {studentId}");
                return;
            }
            if (courseId <= 0)
            {
                Console.WriteLine($"Ther is no course with this Id: {courseId}");
                return;
            }

            try
            {

                var enrollment = dbContext.Enrollments.Find(courseId, studentId);
                if (enrollment is null)
                {
                    Console.WriteLine("Ther is no enrollment with these Ids ");
                    return;
                }

                enrollment.ProgressPercentage = progress;

                if (progress == 100)
                {
                    enrollment.Status = EnrollmentStatus.Completed;
                    enrollment.FinalGrade = finalGrade;
                }

                dbContext.SaveChanges();
                Console.WriteLine("Enrollment progress updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //	Publish a course: Set the published flag and the published date.
        public static void PublishCourse(AssignmentDbContext dbContext, int courseId)
        {
            if (dbContext is null || courseId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                var course = dbContext.Courses.Find(courseId);
                if (course is null)
                {
                    Console.WriteLine($"Ther is no course with this Id:{courseId}.");
                    return;
                }

                if (course.IsPublished == true)
                {
                    Console.WriteLine("This course is already published");
                    return;
                }

                course.IsPublished = true;
                course.PublishedDate = DateTime.Now;

                dbContext.SaveChanges();
                Console.WriteLine("Course published successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Unenroll: Delete an enrollment using its composite key.
        public static void UnenrollStudent(AssignmentDbContext dbContext, int studentId, int courseId)
        {
            if (dbContext is null || courseId <= 0 || studentId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {

                var enrollment = dbContext.Enrollments.Find(courseId , studentId);
                if (enrollment is null)
                {
                    Console.WriteLine("Ther is no Enrollment with these Ids");
                    return;
                }

                dbContext.Enrollments.Remove(enrollment);
                dbContext.SaveChanges();
                Console.WriteLine("Student unenrolled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Delete a category: First try to remove a category that still has sub-categories
        // and catch the DbUpdateException, then print its message.
        // After that, write a safe version that checks for sub-categories before deleting.

        public static void DeleteCategory(AssignmentDbContext dbContext, int categoryId)
        {
            if (dbContext is null || categoryId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                var category = dbContext.Categories.FirstOrDefault(C => C.Id == categoryId);
                if (category is null)
                {
                    Console.WriteLine($"There is no Category with this Id: {categoryId}");
                    return;
                }

                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //safe version that checks for sub-categories before deleting.
        public static void TryDeleteCategory(AssignmentDbContext dbContext, int categoryId)
        {
            if (dbContext is null || categoryId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                var category = dbContext.Categories.FirstOrDefault(C => C.Id == categoryId);
                if (category is null)
                {
                    Console.WriteLine($"There is no Category with this Id: {categoryId}");
                    return;
                }
                if (dbContext.Categories.Any(C => C.ParentId == category.Id))
                {
                    Console.WriteLine("Safe version:\nCategory has sub-categories so deleting is skipped ");
                    return;
                }

                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Delete a course: Delete a course that has enrollments.
        // Print the number of its enrollments before and after, and explain in a comment why they were removed.
        public static void DeleteCourse(AssignmentDbContext dbContext, int courseId)
        {
            if (dbContext is null || courseId <= 0)
            {
                Console.WriteLine("Please send a valid inputs.");
                return;
            }

            try
            {
                var course = dbContext.Courses.Find(courseId);
                if (course is null)
                {
                    Console.WriteLine($"There is no Course with this Id: {courseId}");
                    return;
                }
                var enrollmenCourseNumberbeforeDeleting = dbContext.Enrollments.Count(E => E.CourseId == courseId);
                Console.WriteLine($"Enrollments before course deleting: {enrollmenCourseNumberbeforeDeleting}.");

                // SQL Server well remove its dependent enrollment rows with the course becouse it use cascade deleting.
                dbContext.Courses.Remove(course);
                dbContext.SaveChanges();

                Console.WriteLine("Course Deleted successfully.");
                Console.WriteLine($"Enrollments after course deleting: {dbContext.Enrollments.Count(E => E.CourseId == courseId)}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

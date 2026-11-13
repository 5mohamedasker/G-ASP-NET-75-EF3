using EF_Assignment03.Enums;
using EF_Assignment03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assignment03.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(C => C.Id);

            builder.Property(C => C.Id)
                .IsRequired(true)
                .UseIdentityColumn(10, 10);


            builder.HasOne(C => C.Publisher)
                .WithMany(I => I.PublishedCourses)
                .HasForeignKey(C => C.PublsherId)
                .IsRequired(true);


            builder.HasOne(C => C.BelongCategory)
                .WithMany(category => category.Courses)
                .HasForeignKey(category => category.CategoryId)
                .IsRequired(true);

            builder.Property(C => C.Title)
                .IsRequired(true)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);


            builder.Property(C => C.Description)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");

            builder.Property(C => C.Price)
                .IsRequired(true)
                .HasPrecision(10,3);

            builder.ToTable(t => t.HasCheckConstraint
                (
                    "CK_Courses_PublishedDate",
                    "(IsPublished = 1 and PublishedDate is not null) or (IsPublished = 0 and PublishedDate is null)"
                ));

            builder.HasData
                (
                    new Course() { Id = 10, Title = "C#", Description = "Fundamentals", Price = 999, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2026,12,3) ,PublsherId = 10, CategoryId = 20 },
                    new Course() { Id = 20, Title = "EF Core", Description = "Fundamentals", Price = 1500, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2026, 12, 4), PublsherId = 10, CategoryId = 30 }
                );
        }
    }
}

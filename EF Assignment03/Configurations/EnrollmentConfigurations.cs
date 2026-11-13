using EF_Assignment03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_Assignment03.Configurations
{
    public class EnrollmentConfigurations : IEntityTypeConfiguration<Enrollment>
    {

        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(E => new { E.CourseId , E.StudentId });


            builder.HasOne(E => E.EnrolledStudent)
                .WithMany(S => S.EnrolledCourses)
                .HasForeignKey(E => E.StudentId)
                .IsRequired(true);

            builder.HasOne(E => E.EnrolledCourse)
                .WithMany(C => C.EnrolledStudents)
                .HasForeignKey(E => E.CourseId)
                .IsRequired(true);

            builder.Property(E => E.EnrollmentDate)
                .IsRequired(true);

            builder.Property(E => E.ProgressPercentage)
                .IsRequired(true);

            builder.ToTable(T => T.HasCheckConstraint
                (
                    "CK_Enrollments_ProgressPercentage",
                    "ProgressPercentage Between 0 and 100"
                ));

            builder.Property(E => E.Status)
                .IsRequired(true);

            builder.ToTable(T => T.HasCheckConstraint
                (
                    "CK_Enrollments_Progress_Status",
                    "(ProgressPercentage = 100 and Status = 2) or (ProgressPercentage < 100 and Status != 2)"
                ));

            builder.Property(E => E.FinalGrade)
                .IsRequired(false)
                .HasPrecision(2, 2);

            builder.ToTable(T => T.HasCheckConstraint
                (
                    "CK_Enrollments_FinalGrade",
                    "(Status = 2 and FinalGrade is not null) or (Status != 2 and FinalGrade is null)"
                ));

            builder.ToTable(T => T.HasCheckConstraint(
                "CK_Enrollments_FinalGrade_Range",
                "FinalGrade is null or (FinalGrade Between 0 and 100)"));


        }
    }
}

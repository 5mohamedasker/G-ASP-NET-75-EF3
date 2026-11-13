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
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(S => S.Id);

            builder.Property(S => S.Id)
                .IsRequired(true)
                .UseIdentityColumn(10,10);

            builder.Property(S => S.FullName)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder.Property(S => S.Email)
                .HasColumnType("varchar(100)")
                .IsRequired(true);
            builder.ToTable(T => T.HasCheckConstraint
                (
                    "CK_Students_EmailFormate",
                    "Email like '%@%.%' "
                ));

            builder.Property(S => S.DateOfBirth)
                .IsRequired(true);

            builder.ToTable(T => T.HasCheckConstraint
                (
                    "CK_Students_DateOfBirth",
                    " DateOfBirth < cast(GetDate() as date) "
                ));


            builder.HasData
                (
                    new Student() { Id = 10, FullName = "Mohamed", Email = "mohamed@gmail.com", DateOfBirth = new DateOnly(2002, 4, 6) },
                    new Student() { Id = 20, FullName = "Asker", Email = "asker@gmail.com", DateOfBirth = new DateOnly(1999, 9, 2) },
                    new Student() { Id = 30, FullName = "Abdullah", Email = "abdullah@gmail.com", DateOfBirth = new DateOnly(2012, 11, 12) }
                );

        }
    }
}

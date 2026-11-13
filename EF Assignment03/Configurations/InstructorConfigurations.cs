using EF_Assignment03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_Assignment03.Configurations
{
    public class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {

        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(I => I.Id);

            builder.Property(I => I.Id)
                .IsRequired(true)
                .UseIdentityColumn(10, 10);

            builder.Property(I => I.FullName)
                .HasColumnType("nvarchar(200)")
                .IsRequired(true);

            builder.Property(I => I.Email)
                .HasColumnType("varchar(100)")
                .IsRequired(true);
            builder.ToTable(T => T.HasCheckConstraint
                (
                    "CK_Instructors_EmailFormate",
                    "Email like '%@%.%' "
                ));

            builder.Property(I => I.Bio)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");
            builder.Property(I => I.JoinedDate)
                .IsRequired(true);


            builder.HasData
                (
                    new Instructor() { Id = 10, FullName = "Ahmed", Email = "ahmed@gmail.com", Bio = "Good Instructor", JoinedDate = new DateOnly(2020, 3, 3) },
                    new Instructor() { Id = 20, FullName = "Amr", Email = "Amr@gmail.com", Bio = "Good Mentor", JoinedDate = new DateOnly(2025, 4, 4) }
                );


        }
    }
}

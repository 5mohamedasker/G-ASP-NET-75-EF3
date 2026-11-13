using EF_Assignment03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
 
namespace EF_Assignment03.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(C => C.Id);
 
            builder.Property(C => C.Id)
                .IsRequired(true)
                .UseIdentityColumn(10, 10);
 
            builder.HasOne(P => P.ParentCategory)
                .WithMany(S => S.SubCategories)
                .HasForeignKey(C => C.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
 
            builder.Property(C => C.Name)
                .IsRequired(true)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);
 
 
            builder.Property(C => C.Description)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");
 
            builder.Property(C => C.ParentId)
                .IsRequired(false);


            builder.HasData
                (
                    new Category() { Id = 10, Name = "Programming", Description = "Software development" },
                    new Category() { Id = 20, Name = "Backend", ParentId = 10 },
                    new Category() { Id = 30, Name = "DataBase", ParentId = 20 }
                );
        }
    }
}

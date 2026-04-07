using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NareshLearn.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NareshLearn.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.InstructorId)
                .IsRequired();

            builder.Property(x => x.IsPublished)
                .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();
        }
    }
}

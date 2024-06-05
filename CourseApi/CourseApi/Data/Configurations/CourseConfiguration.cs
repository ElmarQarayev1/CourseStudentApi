using System;
using CourseApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApi.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Limit).IsRequired(true);
        }
    }
}


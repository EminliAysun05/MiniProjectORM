using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMMiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FullName).IsRequired(true).HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(u => u.Password).IsRequired(true).HasMaxLength(256);
            builder.Property(u => u.Address).IsRequired(true).HasMaxLength(100);



        }
    }
}

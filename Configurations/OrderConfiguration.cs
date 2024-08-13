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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.TotalAmount).IsRequired(true).HasColumnType("decimal(5,2)");
            builder.Property(o => o.OrderDate).IsRequired(true).HasDefaultValueSql("GETDATE()");//getdate temasini sorus


        }
    }
}

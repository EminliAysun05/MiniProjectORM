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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(od => od.Quantity).IsRequired(true);
            builder.Property(od => od.PricePerItem).IsRequired(true).HasColumnType("decimal(5,2)");

            builder.HasCheckConstraint("CK_Quantity", "Quantity > 0");
        }
    }
}

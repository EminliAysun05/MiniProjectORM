using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMMiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.NewFolder
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(p => p.Price).IsRequired(true).HasColumnType("decimal(5,2)");
            builder.Property(p => p.Stock).IsRequired(true);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(550);
            builder.Property(p => p.CreatedDate).IsRequired(true).HasDefaultValueSql("GetDate()");
            builder.Property(p => p.UpdatedDate).IsRequired(true).HasDefaultValueSql("GetDate()");

            builder.HasCheckConstraint("CK_Price", "Price > 0");
            builder.HasCheckConstraint("CK_Stock", "Stock >=0");
        }
    }
}

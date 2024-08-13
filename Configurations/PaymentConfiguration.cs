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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.Amount).IsRequired(true).HasColumnType("decimal(5,2)");
            builder.Property(x => x.PaymentDate).IsRequired(true).HasDefaultValueSql("GETDATE()");
            builder.HasCheckConstraint("CK_Amount", "Amount>0");
        }
    }
}

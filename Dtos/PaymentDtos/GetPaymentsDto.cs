using ORMMiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Dtos.PaymentDtos
{
    public class GetPaymentsDto
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public int OrderId { get; set; }
       // public Order Order { get; set; } = null!;
    }
}

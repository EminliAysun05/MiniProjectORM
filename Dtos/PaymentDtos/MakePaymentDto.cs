using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Dtos.PaymentDtos
{
    public class MakePaymentDto
    {
        public decimal Amount { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

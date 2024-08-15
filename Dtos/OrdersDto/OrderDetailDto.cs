using ORMMiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Dtos.OrdersDto
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

 

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

    }
}

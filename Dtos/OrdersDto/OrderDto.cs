using ORMMiniProject.Models.Enums;
using ORMMiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Dtos.OrdersDto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
   
        public OrderStatusEnum OrderStatus { get; set; }

        public ICollection<OrderDetail> Details { get; set; } = null!;

    }
}

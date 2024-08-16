using ORMMiniProject.Dtos.OrdersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrders();
        Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task CreateOrderAsync(OrderDto orderDto);
        Task DeleteOrderSync(int id);
       // Task UpdateOrderAsync(OrderDto orderDto);
      //  Task<List<OrderDto>> GetOrderById(int id);
        
        Task CompleteOrderAsync(int id);
        Task AddOrderDetails(OrderDetailDto orderDetailDto);

    }
}

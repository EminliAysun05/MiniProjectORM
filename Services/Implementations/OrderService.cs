using ORMMiniProject.Dtos.OrdersDto;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Models;
using ORMMiniProject.Models.Enums;
using ORMMiniProject.Repostories.Implementations;
using ORMMiniProject.Repostories.Interfaces;
using ORMMiniProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReposity _orderReposity;
        private readonly IUserReposity _reposity;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductReposity _productReposity;

        public OrderService()
        {
            _orderReposity = new OrderReposity();
            _reposity = new UserReposity();
            _productReposity = new ProductReposity();
            _orderDetailRepository = new OrderDetailReposity();

        }

        public async Task AddOrderDetails(OrderDetailDto orderDetailDto)
        {

            if (orderDetailDto == null)
            {
                throw new NullReferenceException("Order detail cannot be null.");
            }
            if (orderDetailDto.Quantity < 0)
            {
                throw new InvalidOrderDetailException("The quantity of order shouldn't be lower tahn 0");
            }
            if (orderDetailDto.PricePerItem < 0)
            {
                throw new InvalidOrderDetailException("The price of order shouldn't be lower tahn zero");
            }
            var order = await _orderReposity.GetSingleAsync(o => o.Id == orderDetailDto.OrderId);
            if (order == null)
            {
                throw new NotFoundException("Order is not found");
            }
            var product = await _productReposity.GetSingleAsync(o => o.Id == orderDetailDto.ProductId);
            if (product == null)
            {
                throw new NotFoundException("Order is not found");
            }
            order.TotalAmount = product.Price * orderDetailDto.Quantity;
            var OrderDetail = new OrderDetail
            {
                OrderId = orderDetailDto.OrderId,
                ProductId = orderDetailDto.ProductId,
                PricePerItem = product.Price,
                Product = orderDetailDto.Product,
                Order = orderDetailDto.Order,
                Quantity=orderDetailDto.Quantity


            };
            //_orderReposity.Update(order);
            await _orderDetailRepository.AddAsync(OrderDetail);
            await _orderDetailRepository.SaveChangesAsync();

            await _orderReposity.SaveChangesAsync();
        }

        public async Task CompleteOrderAsync(int id)
        {
            var order = await _orderReposity.GetSingleAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("Order is not found");
            }
            if (order.OrderStatus == OrderStatusEnum.Completed)
            {
                throw new OrderAlreadyCompletedException("Orders= is already completed");
            }
            order.OrderStatus = OrderStatusEnum.Completed;
            await _orderReposity.SaveChangesAsync();
        }

        public async Task CreateOrderAsync(OrderDto orderDto)
        {

            var user = await _reposity.GetSingleAsync(u => u.Id == orderDto.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            // if(orderDto.TotalAmount<0)
            // {
            //     throw new InvalidOrderException("Order shoulnd't lower tahn 0");
            // }
            // var user = await _reposity.GetSingleAsync(u => u.Id == orderDto.Id);

            // if(user == null)
            // {
            //     throw new InvalidOrderDetailException("User is not found");
            // }

            decimal totalAmount = 0;
            //foreach (var detail in orderDto.Details)
            //{
            //    var product = await _productReposity.GetSingleAsync(p => p.Id == detail.ProductId);
            //    if (product == null)
            //    {
            //        throw new InvalidOrderDetailException($"Product with ID {detail.ProductId} is not found");
            //    }

            //    totalAmount += detail.Quantity * product.Price; // Burada qiyməti məhsuldan alırıq.
            //}

            //if (totalAmount < 0)
            //{
            //    throw new InvalidOrderException("Order total amount shouldn't be less than 0");
            //}

            var order = new Order
            {
                Id = orderDto.Id,
                TotalAmount = orderDto.TotalAmount,
                OrderStatus = orderDto.OrderStatus,
                OrderDate = DateTime.UtcNow,
                UserId = orderDto.UserId

            };
            await _orderReposity.AddAsync(order);
            await _orderReposity.SaveChangesAsync();
        }

        public async Task DeleteOrderSync(int id)
        {
            var order = await _orderReposity.GetSingleAsync(o => o.Id == id);
            if (order == null)
            {
                throw new NotFoundException("Order is not found");
            }
            //sifarisin legvi
            if (order.OrderStatus == OrderStatusEnum.Cancelled)
            {
                throw new OrderAlreadyCancelledException("Order is already cancelled");
            }
            order.OrderStatus = OrderStatusEnum.Cancelled;
            _orderReposity.Update(order);
            await _orderReposity.SaveChangesAsync();


        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var orders = await _orderReposity.GetAllAsync();
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus

            }).ToList();
        }



        public async Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var order = await _orderReposity.GetSingleAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new NotFoundException("Order is not found");
            }

            return order.Details.Select(od => new OrderDetailDto
            {
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                PricePerItem = od.PricePerItem,
            }).ToList();
        }



    }
}

using ORMMiniProject.Dtos.OrdersDto;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Models.Enums;
using ORMMiniProject.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.HandleServices
{
    public static class HandleOrderService
    {
       public static async Task CreateOrder(OrderService orderService)
        {
            try
            {


                Console.WriteLine("Enter user ID: ");
                int userId = int.Parse(Console.ReadLine());

                //Console.WriteLine("Enter total amount: ");
                //decimal totalAmount = decimal.Parse(Console.ReadLine());

                //Console.WriteLine("Enter Order Date (yyyy-mm-dd):");
                //DateTime orderDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Order Status (Pending/Completed/Cancelled):");
                string? status = Console.ReadLine();
                OrderStatusEnum orderStatus = Enum.Parse<OrderStatusEnum>(status, true);//BAXXX

                OrderDto newOrder = new OrderDto
                {
                    UserId = userId,
                    // TotalAmount = totalAmount,
                    // OrderDate = orderDate,
                    OrderStatus = orderStatus

                };

                await orderService.CreateOrderAsync(newOrder);
                Console.WriteLine("Order created succesfully!");

            }
            catch (InvalidOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       public static async Task CompleteOrder(OrderService orderService)
        {
            try
            {
                Console.WriteLine("Enter5 order ID to complete: ");
                int orderId = int.Parse(Console.ReadLine());

                await orderService.CompleteOrderAsync(orderId);
                Console.WriteLine("Order completed succesfully!");


            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OrderAlreadyCompletedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task DeleteOrder(OrderService orderService)
        {
            try
            {
                Console.WriteLine("Enter order id: ");
                int id = int.Parse(Console.ReadLine());

                await orderService.DeleteOrderSync(id);
                Console.WriteLine("Order deleted succesfully");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OrderAlreadyCancelledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }

        }
       public static async Task AddOrderDetails(OrderService orderService)
        {
            try
            {
                Console.WriteLine("Enter order ID:");
                int orderId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter product ID:");
                int productId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter quantity:");
                int quantity = int.Parse(Console.ReadLine());

                //Console.WriteLine("Enter price per item:");
                //decimal pricePerItem = decimal.Parse(Console.ReadLine());

                OrderDetailDto orderDetailDto = new OrderDetailDto
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = quantity,
                    // PricePerItem = pricePerItem
                };

                await orderService.AddOrderDetails(orderDetailDto);
                Console.WriteLine("Order details added successfully");

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOrderDetailException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task GetOrderDetailsByOrderId(OrderService orderService)
        {
            Console.WriteLine("Enter order ID to get details:");
            int orderId = int.Parse(Console.ReadLine());

            try
            {
                var orderDetails = await orderService.GetOrderDetailsByOrderIdAsync(orderId);

                foreach (var detail in orderDetails)
                {
                    Console.WriteLine($"Order ID: {detail.OrderId}, Product ID: {detail.ProductId}, Quantity: {detail.Quantity}, Price per Item: {detail.PricePerItem}");
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task GetAllOrders(OrderService orderService)
        {
            var orders = await orderService.GetAllOrders();

            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, User ID: {order.UserId}, Total Amount: {order.TotalAmount}, Status: {order.OrderStatus}, Date: {order.OrderDate}");
            }
        }
        public static async Task HandleOrderServiceMethod(OrderService orderService)
        {
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. Complete Order");
            Console.WriteLine("3. Delete Order");
            Console.WriteLine("4. Add Order Details");
            Console.WriteLine("5. Get Order Details by Order ID");
            Console.WriteLine("6. Get All Orders");

            string choice = Console.ReadLine();
            switch (choice)
            {

                case "1":
                    await HandleOrderService.CreateOrder(orderService);
                    break;
                case "2":
                    await HandleOrderService.CompleteOrder(orderService);
                    break;
                case "3":
                    await HandleOrderService.DeleteOrder(orderService);
                    break;
                case "4":
                    await HandleOrderService.AddOrderDetails(orderService);
                    break;
                case "5":
                    await HandleOrderService.GetOrderDetailsByOrderId(orderService);
                    break;
                case "6":
                    await HandleOrderService.GetAllOrders(orderService);
                    break;
                default:
                    Console.WriteLine("Invalid choice...");
                    break;

            }
        }


    }
}

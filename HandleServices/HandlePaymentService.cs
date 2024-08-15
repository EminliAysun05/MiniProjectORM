using ORMMiniProject.Dtos.PaymentDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.HandleServices
{
    public static class HandlePaymentService
    {
        public static async Task CreatePayment(PaymentService paymentService)
        {
            Console.WriteLine("Enter order ID:");
            int orderId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter amount:");
            decimal amount = decimal.Parse(Console.ReadLine());

            MakePaymentDto paymentDto = new MakePaymentDto
            {
                OrderId = orderId,
                Amount = amount
            };

            try
            {
                await paymentService.CreatePayment(paymentDto);
                Console.WriteLine("Payment created successfully");
            }
            catch (InvalidPaymentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task GetAllPayments(PaymentService paymentService)
        {
            try
            {
                var payments = await paymentService.GetAllPayments();
                foreach (var payment in payments)
                {
                    Console.WriteLine($"Order ID: {payment.OrderId}, Amount: {payment.Amount}, Payment Date: {payment.PaymentDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       public static async Task HandlePaymentServiceMethod(PaymentService paymentService)
        {
            Console.WriteLine("1. Create payment");
            Console.WriteLine("2. Get All payments");
            Console.WriteLine("Select an option: [?]");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await HandlePaymentService.CreatePayment(paymentService);
                    break;
                case "2":
                    await HandlePaymentService.GetAllPayments(paymentService);
                    break;
                default:
                    Console.WriteLine("Invalid choice...");
                    break;
            }
        }

    }
}

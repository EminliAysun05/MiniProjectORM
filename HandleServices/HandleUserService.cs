using ORMMiniProject.Dtos.UserDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.HandleServices
{
    public static class HandleUserService
    {
        public static async Task UpdateUser(UserService userService)
        {
            try
            {
                Console.WriteLine("Enter user ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter new fullname: ");
                string fullname = Console.ReadLine();

                Console.WriteLine("Enter new email: ");
                string email = Console.ReadLine();

                Console.WriteLine("Enter new password");
                string password = Console.ReadLine();

                Console.WriteLine("Enter new address");
                string address = Console.ReadLine();

                UpdateUserDto updateUser = new UpdateUserDto
                {
                    Id = id,
                    FullName = fullname,
                    Email = email,
                    Password = password,
                    Address = address
                };

                await userService.UpdateUser(updateUser);
                Console.WriteLine("User information updated successfully!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input format: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine("User not found: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Operation failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
       public static async Task GetUserOrders(UserService userService)
        {
            Console.WriteLine("Enter user ID:");
            int userId = int.Parse(Console.ReadLine());

            var orders = await userService.GetAllOrdersAsync(userId);
            foreach (var order in orders)
            {

                Console.WriteLine($"Order Date: {order.OrderDate}, Total Amount: {order.TotalAmount}");
            }
        }
        public static async Task HandleUserServiceMethod(UserService userService)
        {
            Console.WriteLine("1. Update User Info");
            Console.WriteLine("2. View orders");
            Console.WriteLine("Please, enter the choice[?]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await HandleUserService.UpdateUser(userService);
                    break;
                case "2":
                    await HandleUserService.GetUserOrders(userService);
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("Invalid choice...");
                    break;
            }
        }
    }
}

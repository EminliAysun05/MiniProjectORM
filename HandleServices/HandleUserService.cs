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
        private static readonly string AdminUsername = "admin";
        private static readonly string AdminPassword = "admin123";
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
        public static async Task<bool> CheckAdminAccess()
        {
            Console.WriteLine("Are you an admin? (yes/no):");
            string isAdminResponse = Console.ReadLine().Trim().ToLower();

            if (isAdminResponse == "yes")
            {
                Console.WriteLine("Enter admin username:");
                string username = Console.ReadLine().Trim();

                Console.WriteLine("Enter admin password:");
                string password = Console.ReadLine().Trim();

                if (username.Equals(AdminUsername, StringComparison.OrdinalIgnoreCase) &&
                    password.Equals(AdminPassword, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid admin credentials.");
                    return false;
                }
            }
            return false;
        }
        //public static async Task UpdateUserPassword(UserService userService)
        //{
        //    try
        //    {
        //        Console.WriteLine("Enter user ID: ");
        //        int id = int.Parse(Console.ReadLine());

        //        Console.WriteLine("Enter new password:");
        //        string newPassword = Console.ReadLine();

        //        await userService.UpdateUserPassword(id, newPassword);
        //        Console.WriteLine("User password updated successfully!");
        //    }
        //    catch (FormatException ex)
        //    {
        //        Console.WriteLine("Invalid input format: " + ex.Message);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        Console.WriteLine("User not found: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An unexpected error occurred: " + ex.Message);
        //    }
        //}
        //public static async Task ViewAllUsers(UserService userService)
        //{
        //    try
        //    {
        //        var users = await userService.GetAllUsersAsync();
        //        foreach (var user in users)
        //        {
        //            Console.WriteLine($"ID: {user.Id}, FullName: {user.FullName}, Email: {user.Email}, Address: {user.Address}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred while fetching users: " + ex.Message);
        //    }
        //}


        public static async Task HandleUserServiceMethod(UserService userService)
        {
            bool isAdmin = await CheckAdminAccess();

            if (isAdmin)
            {
                Console.WriteLine("1. Update User Info");
                Console.WriteLine("2. View orders");
                //Console.WriteLine("3. Update User Password");
                //Console.WriteLine("4. View All Users");
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
                    //case "3":
                    //    await HandleUserService.UpdateUserPassword(userService);
                    //    break;
                    //case "4":
                    //    await HandleUserService.ViewAllUsers(userService);
                    //    break;
                    default:
                        Console.WriteLine("Invalid choice...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("You do not have access to admin functionalities.");
            }
        }
    }
        }


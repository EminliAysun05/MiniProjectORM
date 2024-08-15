using ORMMiniProject.Dtos.UserDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Services.Implementations;
using ORMMiniProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.HandleServices
{
    public static class LoginAndRegisterSystem
    {
        public static async Task<bool> Login(UserService userService)
        {
            try
            {
                Console.WriteLine("Enter your email: ");
                string? email = Console.ReadLine();



                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine();

                var dto = new LoginUserDto
                {
                    Email = email,
                    Password = password
                };

                var user = await userService.LoginUserAsync(dto);
                Console.WriteLine($"Welcome{user.FullName}");
                return true;
            }
            catch (InvalidLoginException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This email is not registered.Would you like to register(yes/no)");
                string? answer = Console.ReadLine();
                if (answer?.ToLower() == "yes")
                {
                    await Register(userService);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return false;

        }
        public static async Task Register(IUserService userService)
        {
            try
            {

                Console.WriteLine("Enter your fullname: ");
                string? fullname = Console.ReadLine();

                Console.WriteLine("Enter your email:");
                string? email = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                string? password = Console.ReadLine();

                await Console.Out.WriteLineAsync("Enter your Address");
                string? address = Console.ReadLine();

                RegisterUserDto registerUser = new RegisterUserDto
                {
                    FullName = fullname,
                    Email = email,
                    Password = password,
                    Address = address
                };
                await userService.RegisterUserAsync(registerUser);
                Console.WriteLine("Registration succesfull");

            }
            catch (InvalidUserInformationException ex)
            {
                Console.WriteLine(ex.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

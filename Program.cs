using ORMMiniProject.Dtos.ProductDtos;
using ORMMiniProject.HandleServices;
using ORMMiniProject.Services.Implementations;

namespace ORMMiniProject
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ProductService productService = new ProductService();
            UserService userService = new UserService();
            OrderDetailService orderDetailService = new OrderDetailService();
            OrderService orderService = new OrderService();
            PaymentService paymentService = new PaymentService();
            await productService.CreateProductAsync(new() { Name = "Qara kula", Price = 100 });

            var list = await productService.GetAllProductsAsync();

            ExportExcel.Export<GetAllProductDto>(list, @"C:\Users\namjoon\source\repos\ORMMiniProject\ExcelFiles\Products.xlsx", "Products");
            while (true)
            {
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("[1]. Login");
                Console.WriteLine("[2]. Register");
                Console.WriteLine("[3]. Exit");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var result = await LoginAndRegisterSystem.Login(userService);
                        if (!result)
                            break;

                        while (true)
                        {
                            Console.WriteLine("Please, enter the choice");
                            Console.WriteLine("1. Go to Product service");
                            Console.WriteLine("2. Go to User service");
                            Console.WriteLine("3. Go to Order service");
                            Console.WriteLine("4. Go to Payment service");
                            Console.WriteLine("5. Logout");
                            string? choice1 = Console.ReadLine();
                            switch (choice1)
                            {
                                case "1":
                                    await HandleProductService.HandleProductServiceMethod(productService);
                                    break;
                                case "2":
                                    await HandleUserService.HandleUserServiceMethod(userService);
                                    break;
                                case "3":
                                    await HandleOrderService.HandleOrderServiceMethod(orderService);
                                    break;
                                case "4":
                                    await HandlePaymentService.HandlePaymentServiceMethod(paymentService);
                                    break;
                                case "5":
                                    Console.WriteLine("Logged out...");
                                    return;
                                default:
                                    Console.WriteLine("Invalid choice...");
                                    break;
                            }
                        }
                    case "2":
                        await LoginAndRegisterSystem.Register(userService);
                        break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice...");
                        break;
                }
            }
        }
       
       
    }
}

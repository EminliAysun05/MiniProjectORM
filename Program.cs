using ClosedXML.Excel;
using ORMMiniProject.Dtos.OrdersDto;
using ORMMiniProject.Dtos.PaymentDtos;
using ORMMiniProject.Dtos.ProductDtos;
using ORMMiniProject.Dtos.UserDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Models.Enums;
using ORMMiniProject.Services.Implementations;
using ORMMiniProject.Services.Interfaces;

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

            var list =await productService.GetAllProductsAsync();

            Export<GetAllProductDto>(list, @"C:\Users\namjoon\source\repos\ORMMiniProject\ExcelFiles\Products.xlsx", "Products");
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
                        var result = await Login(userService);
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
                                    await HandleProductService(productService);
                                    break;
                                case "2":
                                    await HandleUserService(userService);
                                    break;
                                case "3":
                                    await HandleOrderService(orderService);
                                    break;
                                case "4":
                                    await HandlePaymentService(paymentService);
                                    break;
                                case "5":
                                    Console.WriteLine("Logged out...");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice...");
                                    break;
                            }
                        }

                        break;
                    case "2":
                        await Register(userService);
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
        static async Task HandlePaymentService(PaymentService paymentService)
        {
            Console.WriteLine("1. Create payment");
            Console.WriteLine("2. Get All payments");
            Console.WriteLine("Select an option: [?]");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreatePayment(paymentService);
                    break;
                case "2":
                    await GetAllPayments(paymentService);
                    break;
                default:
                    Console.WriteLine("Invalid choice...");
                    break;
            }
        }
        static async Task CreatePayment(PaymentService paymentService)
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

        static async Task GetAllPayments(PaymentService paymentService)
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
        static async Task HandleOrderService(OrderService orderService)
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
                    await CreateOrder(orderService);
                    break;
                case "2":
                    await CompleteOrder(orderService);
                    break;
                case "3":
                    await DeleteOrder(orderService);
                    break;
                case "4":
                    await AddOrderDetails(orderService);
                    break;
                case "5":
                    await GetOrderDetailsByOrderId(orderService);
                    break;
                case "6":
                    await GetAllOrders(orderService);
                    break;
                default:
                    Console.WriteLine("Invalid choice...");
                    break;

            }
        }
        static async Task AddOrderDetails(OrderService orderService)
        {
            Console.WriteLine("Enter order ID:");
            int orderId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter product ID:");
            int productId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter quantity:");
            int quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter price per item:");
            decimal pricePerItem = decimal.Parse(Console.ReadLine());

            OrderDetailDto orderDetailDto = new OrderDetailDto
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                PricePerItem = pricePerItem
            };

            try
            {
                await orderService.AddOrderDetails(orderDetailDto);
                Console.WriteLine("Order details added successfully");
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
        static async Task GetAllOrders(OrderService orderService)
        {
            var orders = await orderService.GetAllOrders();

            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, User ID: {order.UserId}, Total Amount: {order.TotalAmount}, Status: {order.OrderStatus}, Date: {order.OrderDate}");
            }
        }
        static async Task GetOrderDetailsByOrderId(OrderService orderService)
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
        static async Task DeleteOrder(OrderService orderService)
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
        static async Task CreateOrder(OrderService orderService)
        {
            try
            {
                Console.WriteLine("Enter user ID: ");
                int userId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter total amount: ");
                decimal totalAmount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter Order Date (yyyy-mm-dd):");
                DateTime orderDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Order Status (Pending/Completed/Cancelled):");
                string status = Console.ReadLine();
                OrderStatusEnum orderStatus = Enum.Parse<OrderStatusEnum>(status, true);//BAXXX

                OrderDto newOrder = new OrderDto
                {
                    UserId = userId,
                    TotalAmount = totalAmount,
                    OrderDate = orderDate,
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
        static async Task CompleteOrder(OrderService orderService)
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
        static async Task HandleUserService(UserService userService)
        {
            Console.WriteLine("1. Update User Info");
            Console.WriteLine("2. View orders");
            Console.WriteLine("Please, enter the choice[?]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await UpdateUser(userService);
                    break;
                case "2":
                    await GetUserOrders(userService);
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("Invalid choice...");
                    break;
            }
        }

        static async Task GetUserOrders(UserService userService)
        {
            Console.WriteLine("Enter user ID:");
            int userId = int.Parse(Console.ReadLine());

            var orders = await userService.GetAllOrdersAsync(userId);
            foreach (var order in orders)
            {

                Console.WriteLine($"Order Date: {order.OrderDate}, Total Amount: {order.TotalAmount}");
            }
        }
        static async Task UpdateUser(UserService userService)
        {
            Console.WriteLine("Enter user ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new fullname: ");
            string fullname = Console.ReadLine();

            Console.WriteLine("Enter new email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter new password");
            string password = Console.ReadLine();

            Console.WriteLine("Enter new adress");
            string adress = Console.ReadLine();

            UpdateUserDto updateUser = new UpdateUserDto
            {
                Id = id,
                FullName = fullname,
                Email = email,
                Password = password,
                //   Address = address
            };

            await userService.UpdateUser(updateUser);
            Console.WriteLine("User information updated succesfully!");

        }
        static async Task HandleProductService(ProductService productService)
        {
            //while(true)
            //{
            Console.WriteLine("Please, enter your choice:");
            Console.WriteLine("[1]. Create Product");
            Console.WriteLine("[2]. Update Product");
            Console.WriteLine("[3]. Delete Product");
            Console.WriteLine("[4]. Get All Products");
            Console.WriteLine("[5]. Search Product");
            Console.WriteLine("[6]. Get Product By ID");
            Console.WriteLine("[7]. Back to Main Menu");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateProduct(productService);
                    break;
                case "2":
                    await UpdateProduct(productService);
                    break;
                case "3":
                    await DeleteProduct(productService);
                    break;
                case "4":
                    await GetAllProducts(productService);
                    break;
                case "5":
                    await SearchProduct(productService);
                    break;
                case "6":
                    await GetProductById(productService);
                    break;
                case "7":
                    return; // Ana menyuya qayıt
                default:
                    Console.WriteLine("Invalid choice...");
                    break;
                    //}
            }
        }

        static async Task CreateProduct(ProductService productService)
        {
            try
            {
                Console.WriteLine("Enter product name: ");
                string? name = Console.ReadLine();

                Console.WriteLine("Enter product price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                AddProductDto newProduct = new AddProductDto
                {
                    Name = name,
                    Price = price,
                };

                await productService.CreateProductAsync(newProduct);
                Console.WriteLine("Product created succesfully!");

            }
            catch (InvalidProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static async Task UpdateProduct(ProductService productService)
        {
            try
            {
                Console.WriteLine("Enter product ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter  new peoduct name: ");
                string? name = Console.ReadLine();

                Console.WriteLine("Enter new product price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                UpdateProductDto updateProductDto = new UpdateProductDto
                {
                    Id = id,
                    Name = name,
                    Price = price,
                };
                await productService.UpdateProduct(updateProductDto);
                Console.WriteLine("Product updated succesfully! ");




            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured : {ex.Message}");
            }
        }
        static async Task DeleteProduct(ProductService productService)
        {
            try
            {
                Console.WriteLine("Please enter product ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                await productService.DeleteProduct(id);
                Console.WriteLine("Product deleted succsfully!");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"NotFound {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static async Task GetAllProducts(ProductService productService)
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    Console.WriteLine($"Id: {product.Id}, Name : {product.Name},Price: {product.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured : " + ex.Message);

            }
        }
        static async Task SearchProduct(ProductService productService)
        {
            try
            {
                Console.WriteLine("Enter product name to search: ");
                string name = Console.ReadLine();

                var products = await productService.SearchProductAsync(name);
                foreach (var product in products)
                {
                    Console.WriteLine($"Id: {product.Id}, Name : {product.Name},Price: {product.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task GetProductById(ProductService productService)
        {
            try
            {
                Console.WriteLine("Enter product ID: ");
                int id = Convert.ToInt32(Console.ReadLine());


                var product = await productService.GetProductById(id);
                Console.WriteLine($"Id: {product.Id}, Name : {product.Name},Price: {product.Price}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured {ex.Message}");
            }
        }
        static async Task<bool> Login(UserService userService)
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

        static async Task Register(IUserService userService)
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

        static bool Export<T> (List<T> list, string file,string sheetName)
        {
            bool exported = false;
            using(IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(sheetName).FirstCell().InsertTable<T>(list, false);

                workbook.SaveAs(file);
                exported = true;
            }
            return exported;
        }
    }
}

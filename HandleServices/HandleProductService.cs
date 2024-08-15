using ORMMiniProject.Dtos.ProductDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.HandleServices;

public static class HandleProductService
{
    public static async Task CreateProduct(ProductService productService)
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
    public static async Task UpdateProduct(ProductService productService)
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
   public static async Task DeleteProduct(ProductService productService)
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
  public  static async Task GetAllProducts(ProductService productService)
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
   public static async Task SearchProduct(ProductService productService)
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
    public static async Task GetProductById(ProductService productService)
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
    public static async Task HandleProductServiceMethod(ProductService productService)
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
                await HandleProductService.CreateProduct(productService);
                break;
            case "2":
                await HandleProductService.UpdateProduct(productService);
                break;
            case "3":
                await HandleProductService.DeleteProduct(productService);
                break;
            case "4":
                await HandleProductService.GetAllProducts(productService);
                break;
            case "5":
                await HandleProductService.SearchProduct(productService);
                break;
            case "6":
                await HandleProductService.GetProductById(productService);
                break;
            case "7":
                return; // Ana menyuya qayıt
            default:
                Console.WriteLine("Invalid choice...");
                break;
                //}
        }
    }
}

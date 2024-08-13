using ORMMiniProject.Dtos.ProductDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Models;
using ORMMiniProject.Repostories.Implementations;
using ORMMiniProject.Repostories.Interfaces;
using ORMMiniProject.Services.Interfaces;

namespace ORMMiniProject.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductReposity _productReposity;
        public ProductService()
        {
            _productReposity = new ProductReposity();
        }
        public async Task CreateProductAsync(AddProductDto newProduct)
        {
            if (string.IsNullOrWhiteSpace(newProduct.Name) || newProduct.Price < 0)
            {
                throw new InvalidProductException("Invalid product data");
            }
            var product = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price
            };

            await _productReposity.AddAsync(product);




        }

        public async Task DeleteProduct(int id)
        {
            var product = await _productReposity.GetSingleAsync(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            _productReposity.Delete(product);
            await _productReposity.SaveChangesAsync();

        }

        public async Task<List<GetAllProductDro>> GetAllProductsAsync()
        {
            var products = await _productReposity.GetAllAsync();

            var productDtos = products.Select(p => new GetAllProductDro

            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }
            ).ToList();
            return productDtos;

        }

        public async Task<GetProductByIdDto> GetProductById(int id)
        {
            var product = await _productReposity.GetSingleAsync(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            return new GetProductByIdDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price


            };
        }

        public async Task<List<GetAllProductDro>> SearchProductAsync(string name)
        {
            var products = await _productReposity.GetFilterAsync(x => x.Name.Contains(name));
            //nullWhiteSpace yoxlaaaa
            return products.Select(p => new GetAllProductDro
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public async Task UpdateProduct(UpdateProductDto newProduct)
        {
            var product = await _productReposity.GetSingleAsync(x => x.Id == newProduct.Id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            if (string.IsNullOrWhiteSpace(newProduct.Name) || newProduct.Price < 0)
            {
                throw new InvalidProductException("Invalid product data");
            }
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
          

            _productReposity.Update(product);
            await _productReposity.SaveChangesAsync();



        }


    }
}

using ORMMiniProject.Dtos.ProductDtos;

namespace ORMMiniProject.Services.Interfaces;

public interface IProductService
{
    Task <List<GetAllProductDro>> GetAllProductsAsync ();
    Task CreateProductAsync(AddProductDto newProduct);

    Task UpdateProduct (UpdateProductDto newProduct);
    Task DeleteProduct(int id);
    Task<GetProductByIdDto> GetProductById(int id);
    Task<List<GetAllProductDro>> SearchProductAsync(string name);

}

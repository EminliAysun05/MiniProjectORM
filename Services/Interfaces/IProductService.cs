using ORMMiniProject.Dtos.ProductDtos;

namespace ORMMiniProject.Services.Interfaces;

public interface IProductService
{
    Task <List<GetAllProductDto>> GetAllProductsAsync ();
    Task CreateProductAsync(AddProductDto newProduct);

    Task UpdateProduct (UpdateProductDto newProduct);
    Task DeleteProduct(int id);
    Task<GetProductByIdDto> GetProductById(int id);
    Task<List<GetAllProductDto>> SearchProductAsync(string name);

}

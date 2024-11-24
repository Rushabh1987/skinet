using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task <IReadOnlyList<string>> GetBrandsAsync();
    Task <IReadOnlyList<string>> GetTypesAsync();
    //below 3 methods are not async because what we are effectively doing is we are adding something to Entity Framework tracking. We are not actually calling database at this Point. We only do that when we save the changes. So they are synchronous beacuse they are not interacting with database.
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}

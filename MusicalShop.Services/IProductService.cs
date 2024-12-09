namespace MusicalShop.Services
{
    using MusicalShop.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<bool> CreateProductTypeAsync(ProductTypeServiceModel model);
        Task<bool> CreateProductBrandAsync(ProductBrandServiceModel model);
        Task<bool> CreateProductAsync(ProductServiceModel model);
        IQueryable<ProductTypeServiceModel> GetAllProductTypes();
        IQueryable<ProductBrandServiceModel> GetAllProductBrands();
        IQueryable<ProductServiceModel> GetAllProducts();
        IQueryable<ProductServiceModel> GetAllProductsByPrice(decimal min, decimal max);
        IQueryable<ProductServiceModel> GetAllProductsByBrand(string brand);
        IQueryable<ProductServiceModel> GetAllProductsByType(string type);
        Task<ProductServiceModel> GetProductByIdAsync(string id);
        Task<bool> DeleteProductByIdAsync(string id);
        Task<bool> EditProductAsync(string id, ProductServiceModel model);
    }
}
namespace MusicalShop.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MusicalShop.Data;
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Services.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    public class ProductService : IProductService
    {
        private readonly MusicalShopDbContext context;

        public ProductService(MusicalShopDbContext context)
        {
            this.context = context;
        }
        //Criteria
        //Categorie
        //Price 
        //Brands

        public async Task<bool> CreateProductAsync(ProductServiceModel model)
        {
            var productTypeFromDb = await this.context.ProductTypes
                .SingleOrDefaultAsync(productType => productType.Name == model.ProductType.Name);

            if (productTypeFromDb == null)
            {
                throw new ArgumentNullException(nameof(productTypeFromDb));
            }

            var product = Mapper.Map<Product>(model);
            product.Id = Guid.NewGuid().ToString();
            product.ProductType = productTypeFromDb;

            await context.Products.AddAsync(product);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> CreateProductBrandAsync(ProductBrandServiceModel model)
        {
            var productBrand = Mapper.Map<ProductBrand>(model);

            await context.ProductBrands.AddAsync(productBrand);
            var result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> CreateProductTypeAsync(ProductTypeServiceModel model)
        {
            var productType = Mapper.Map<ProductType>(model);

            await context.ProductTypes.AddAsync(productType);
            var result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteProductByIdAsync(string id)
        {
            var product = await context.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            context.Products.Remove(product);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EditProductAsync(string id, ProductServiceModel model)
        {
            var producTypeFromDb = await context.ProductTypes.FirstOrDefaultAsync(x => x.Name == model.ProductType.Name);

            if (producTypeFromDb == null)
            {
                throw new ArgumentNullException(nameof(producTypeFromDb));
            }

            var productFromDb = await context.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (productFromDb == null)
            {
                throw new ArgumentNullException(nameof(productFromDb));
            }

            if (model.Picture != null)
            {
                productFromDb.Picture = model.Picture;
            }

            productFromDb.Name = model.Name;
            productFromDb.Price = model.Price;
            productFromDb.ProductType = producTypeFromDb;
            productFromDb.Quantity = model.Quantity;
            productFromDb.ManufacturedOn = model.ManufacturedOn;
            productFromDb.Description = model.Description;

            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public IQueryable<ProductBrandServiceModel> GetAllProductBrands()
        {
            return context.ProductBrands.To<ProductBrandServiceModel>();
        }

        public IQueryable<ProductServiceModel> GetAllProducts()
        {
            return context.Products.To<ProductServiceModel>();
        }

        public IQueryable<ProductServiceModel> GetAllProductsByBrand(string brand)
        {
            return context.Products
                .Where(x => x.ProductBrand.Name.ToLower() == brand.ToLower())
                .To<ProductServiceModel>();
        }

        public IQueryable<ProductServiceModel> GetAllProductsByPrice(decimal min, decimal max)
        {
            return context.Products
                .Where(x => x.Price >= min && x.Price <= max)
                .OrderBy(x => x.Price)
                .To<ProductServiceModel>();
        }

        public IQueryable<ProductServiceModel> GetAllProductsByType(string type)
        {
            return context.Products
                .Where(x => x.ProductType.Name.ToLower() == type.ToLower())
                .To<ProductServiceModel>();
        }

        public IQueryable<ProductTypeServiceModel> GetAllProductTypes()
        {
            return context.ProductTypes.To<ProductTypeServiceModel>();
        }

        public async Task<ProductServiceModel> GetProductByIdAsync(string id)
        {
            var products = await this.context.Products
                .To<ProductServiceModel>()
                .SingleOrDefaultAsync(x => x.Id == id);

            return products;
        }
    }
}
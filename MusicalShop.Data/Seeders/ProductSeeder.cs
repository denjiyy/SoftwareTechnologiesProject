namespace MusicalShop.Data.Seeders
{
    using MusicalShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductSeeder : ISeeder
    {
        private readonly MusicalShopDbContext context;

        public ProductSeeder(MusicalShopDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (!context.Products.Any())
            {
                var productTypes = await context.ProductTypes.ToDictionaryAsync(pt => pt.Name, pt => pt.Id);
                var productBrands = await context.ProductBrands.ToDictionaryAsync(pb => pb.Name, pb => pb.Id);

                var products = new List<Product>()
                {
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Price = 2589.99m,
                        Description = "The best acoustic Guitar ever existed!",
                        Name = "Lâg HyVibe THV30DCE",
                        Picture = "https://res.cloudinary.com/dnbwy7p6f/image/upload/v1620935444/musical_shop_product_images/lag-tramontane-thv30dce-dreadnought-cutaway-hyvibe_dxsn5q.png",
                        ManufacturedOn = DateTime.UtcNow.AddYears(-1),
                        ProductTypeId = productTypes["Acoustic Guitar"],
                        ProductBrandId = productBrands["Lag"],
                        Quantity = 121
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}

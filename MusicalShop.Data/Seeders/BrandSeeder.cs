namespace MusicalShop.Data.Seeders
{
    using MusicalShop.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class BrandSeeder : ISeeder
    {
        private readonly MusicalShopDbContext context;

        public BrandSeeder(MusicalShopDbContext context)
        {
            this.context = context;
        }
        public async Task SeedAsync()
        {
            if (!context.ProductBrands.Any())
            {
                var brands = new List<ProductBrand>()
            {
                new ProductBrand()
                {
                    Name = "Fender"
                },
                new ProductBrand()
                {
                    Name = "Stag"
                },
                new ProductBrand()
                {
                    Name = "Lag"
                },
                new ProductBrand()
                {
                    Name = "Eko"
                },
                new ProductBrand()
                {
                    Name = "Kawai"
                },
                new ProductBrand()
                {
                    Name = "Roland"
                },
                new ProductBrand()
                {
                    Name = "Kyser"
                }
            };

                await context.ProductBrands.AddRangeAsync(brands);
            }
        }
    }
}

namespace MusicalShop.Data.Seeders
{
    using MusicalShop.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductTypeSeeder : ISeeder
    {
        private readonly MusicalShopDbContext context;

        public ProductTypeSeeder(MusicalShopDbContext context)
        {
            this.context = context;
        }
        public async Task SeedAsync()
        {
            if (!context.ProductTypes.Any())
            {
                var types = new List<ProductType>()
                {
                    new ProductType()
                    { 
                        Name = "Acoustic Guitar"
                    },
                    new ProductType()
                    {
                        Name = "Electric Guitar"
                    },new ProductType()
                    {
                        Name = "Drums"
                    },new ProductType()
                    {
                        Name = "Violin"
                    },new ProductType()
                    {
                        Name = "Piano"
                    },
                    new ProductType()
                    {
                        Name = "Ukulele"
                    },
                    new ProductType()
                    {
                        Name = "Accessories"
                    }
                };
                await context.ProductTypes.AddRangeAsync(types);
            }
        }
    }
}

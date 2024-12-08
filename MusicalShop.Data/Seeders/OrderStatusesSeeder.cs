namespace MusicalShop.Data.Seeders
{
    using MusicalShop.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderStatusesSeeder : ISeeder
    {
        private readonly MusicalShopDbContext context;

        public OrderStatusesSeeder(MusicalShopDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (!context.OrderStatuses.Any())
            {
                var statuses = new List<OrderStatus>()
                {
                    new OrderStatus()
                    {
                        Name = "Ordered"
                    },
                    new OrderStatus()
                    {
                        Name = "Shipped"
                    },
                    new OrderStatus()
                    {
                        Name = "Delivered"
                    },
                    new OrderStatus()
                    {
                        Name = "Canceled"
                    }
                };

                await context.AddRangeAsync(statuses);
                await context.SaveChangesAsync();
            }
        }
    }
}

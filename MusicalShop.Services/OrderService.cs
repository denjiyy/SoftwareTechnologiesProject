namespace MusicalShop.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MusicalShop.Data;
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Services.Models;
    using System;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly MusicalShopDbContext context;

        public OrderService(MusicalShopDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(OrderServiceModel model)
        {
            var orderStatusFromDb = await context.OrderStatuses.SingleOrDefaultAsync(x => x.Name == model.OrderStatus.Name);

            if (orderStatusFromDb == null)
            {
                throw new ArgumentNullException(nameof(orderStatusFromDb));
            }

            var order = Mapper.Map<Order>(model);
            order.Id = Guid.NewGuid().ToString();
            order.OrderStatus = orderStatusFromDb;

            await context.AddAsync(order);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
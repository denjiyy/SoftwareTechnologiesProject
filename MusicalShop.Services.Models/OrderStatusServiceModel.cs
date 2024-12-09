﻿namespace MusicalShop.Services.Models
{
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    public class OrderStatusServiceModel : IMapFrom<OrderStatus>, IMapTo<OrderStatus>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
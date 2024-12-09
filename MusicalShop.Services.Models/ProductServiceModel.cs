namespace MusicalShop.Services.Models
{
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    using System;

    public class ProductServiceModel : IMapTo<Product>, IMapFrom<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeServiceModel ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrandServiceModel ProductBrand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public DateTime ManufacturedOn { get; set; }
        public string Description { get; set; }
    }
}
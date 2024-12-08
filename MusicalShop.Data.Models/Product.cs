namespace MusicalShop.Data.Models
{
    using System;
    public class Product : BaseModel<string>
    {
        public string Name { get; set; }
        public DateTime ManufacturedOn { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public virtual ProductBrand ProductBrand { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }
}
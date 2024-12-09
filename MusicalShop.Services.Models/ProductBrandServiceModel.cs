namespace MusicalShop.Services.Models
{
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;

    public class ProductBrandServiceModel : IMapFrom<ProductBrand>, IMapTo<ProductBrand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
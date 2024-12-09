namespace MusicalShop.Services.Models
{
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    public class ProductTypeServiceModel : IMapFrom<ProductType>, IMapTo<ProductType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
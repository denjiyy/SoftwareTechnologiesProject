namespace MusicalShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductBrand : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
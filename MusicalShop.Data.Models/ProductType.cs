namespace MusicalShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductType : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
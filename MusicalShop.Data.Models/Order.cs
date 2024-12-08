namespace MusicalShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Order : BaseModel<string>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string Address { get; set; }
        public string OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public decimal Total { get; set; }
        public string Comment { get; set; }
        public bool IsCashOnDelivery { get; set; }
    }
}
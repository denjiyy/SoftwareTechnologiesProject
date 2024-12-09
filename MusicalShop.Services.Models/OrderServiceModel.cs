namespace MusicalShop.Services.Models
{
    using MusicalShop.Data.Models;
    using MusicalShop.Services.Mapping;
    public class OrderServiceModel : IMapTo<Order>, IMapFrom<Order>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Town { get; set; }
        public string OrderStatusId { get; set; }
        public OrderStatusServiceModel OrderStatus{get;set;}
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public decimal Total { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public bool IsCashOnDelivery { get; set; }
    }
}
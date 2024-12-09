namespace MusicalShop.Services
{
    using MusicalShop.Services.Models;
    using System.Threading.Tasks;
    public interface IOrderService
    {
        Task<bool> Create(OrderServiceModel model);
    }
}
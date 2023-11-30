using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSucces, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}

using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsServices productsServices;
        private readonly ICustomerService customerService;

        public SearchService(IOrdersService ordersService, IProductsServices productsServices, ICustomerService customerService)
        {
            this.ordersService = ordersService;
            this.productsServices = productsServices;
            this.customerService = customerService;
        }
        public async Task<(bool IsSucces, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var customersResult = await customerService.GetCustomerAsync(customerId);
            var orderResult = await ordersService.GetOrderAsync(customerId);
            var productResult = await productsServices.GetProductsAsync();


            if (orderResult.IsSuccess) {

                foreach(var order in orderResult.Order)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product information is not available";
                    }
                }

                var result = new
                {
                    Customer = customersResult.IsSucces ?
                    customersResult.Customer :
                    new { Name = "Customer Information is not available"}, orderResult.Order
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}

using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.OrdersController.cs
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Zakari Gaudreault St-Jean 2036115
* Date: 		11 14 2023
* Class Name: 	OrdersController.cs
* Description: 	Controller for the orders in the database
*/


    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {

        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider customersProvider)
        {
            this.ordersProvider = customersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.orders);
            }
            return NotFound();
        }

    }
}

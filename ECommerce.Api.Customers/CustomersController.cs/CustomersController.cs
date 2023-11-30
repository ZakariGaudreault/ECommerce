using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.CustomersController.cs
{
    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Zakari Gaudreault St-Jean 2036115
* Date: 		11 14 2023
* Class Name: 	CustomerController.cs
* Description: 	Controller for the customers in the database
   */


    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }    
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

    }
}

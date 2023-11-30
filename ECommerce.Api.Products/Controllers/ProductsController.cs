using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{

    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	Zakari Gaudreault St-Jean 2036115
* Date: 		11 14 2023
* Class Name: 	CustomerController.cs
* Description: 	Controller for the Prudcts in the database
*/


    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductsProvider productsProvider;
        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }


        [HttpGet]
        public async Task<IActionResult> GetActionResultAsync()
        {
           var result = await  productsProvider.GetProcutsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();

        }
    }
}

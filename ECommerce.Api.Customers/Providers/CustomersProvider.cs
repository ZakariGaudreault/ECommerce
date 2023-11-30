using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {


        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        public void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Willie Johns", Address = "10120 th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Sam Johns", Address = "12300 th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Michelle Johns", Address = "132200 th St." });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Robby Johns", Address = "1020 th St." });
                dbContext.SaveChanges();
            }
        }


  

        async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> ICustomersProvider.GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }

        async Task<(bool IsSuccess, Models.Customer Customers, string ErrorMessage)> ICustomersProvider.GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    logger?.LogInformation($"customer found");
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

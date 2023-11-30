using ECommerce.Api.Orders.Db;
using System.Collections.Generic;
using System;

namespace ECommerce.Api.Orders.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CusomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}

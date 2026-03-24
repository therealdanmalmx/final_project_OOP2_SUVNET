using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DTO
{
    public class GetOrderItemDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
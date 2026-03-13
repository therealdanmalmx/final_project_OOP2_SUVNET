using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Courier
    {
        public Guid AccountId { get; set; }
        public bool IsAvailable { get; set; }
        public List<Order> OrderDeliveries { get; set; }
    }
}
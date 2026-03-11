using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class MenuItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
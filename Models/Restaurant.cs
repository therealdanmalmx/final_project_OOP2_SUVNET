using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Restaurant
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Address { get; set; }
        public float Review { get; set; }
        public string OpeningHours { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Restaurant
    {
        public Guid Id { get; private set; } = Guid.CreateVersion7();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public float Review { get; set; }
        public string OpeningHours { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
        public List<MenuItem>? MenuItems { get; set; }

        public Restaurant() { }
        public Restaurant(string name, string description, string address, string openingHours, decimal minimumOrderValue, decimal serviceFee)
        {
            Name = name;
            Description = description;
            Address = address;
            OpeningHours = openingHours;
            MinimumOrderValue = minimumOrderValue;
            ServiceFee = serviceFee;
        }
    }
}


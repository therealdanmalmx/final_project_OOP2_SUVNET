using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DTOs
{
    public class GetRestaurantDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MyProperty { get; set; }
        public float Review { get; set; }
        public string OpeningHours { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal ServiceFee { get; set; }
    }
}
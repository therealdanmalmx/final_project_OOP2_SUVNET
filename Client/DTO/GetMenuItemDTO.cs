using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DTO
{
    public class GetMenuItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid RestaurantId { get; set; }

    }
}
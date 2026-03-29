using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Review
{
    public class AddNewReviewDTO
    {
        public float Score { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid? OrderId { get; set; }
        public Guid? RestaurantId { get; set; }
    }
}
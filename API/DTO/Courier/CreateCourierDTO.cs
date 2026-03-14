using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Courier
{
    public class CreateCourierDTO
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public List<Models.Order> OrderDeliveries { get; set; }
    }
}
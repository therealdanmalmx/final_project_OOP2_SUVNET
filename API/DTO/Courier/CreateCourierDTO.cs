using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CreateCourierDTO
    {
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public List<Models.Order>? Orders { get; set; }
    }
}
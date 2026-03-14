using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Courier
{
    public class GetCourierDTO
    {
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public List<Guid>? Orders { get; set; }
    }
}
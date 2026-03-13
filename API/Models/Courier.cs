using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Migrations;

namespace API.Models
{
    public class Courier
    {
        [Key]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public bool IsAvailable { get; set; }
        public List<Order> OrderDeliveries { get; set; }
    }
}
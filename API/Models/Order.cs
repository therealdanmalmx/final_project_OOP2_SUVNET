using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        private int _number = (int)Random.Shared.NextInt64(1000, 100000);
        public int Number
        {
            get => _number;
            set => _number = value;
        }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Instructions { get; set; }
        public Status Status { get; set; } = Status.received;
        public Guid? CourierId { get; set; }
        public Guid? AccountId { get; set; }
        // public List<OrderItem> OrderItems { get; set; }

        public Order() { }
        public Order(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
        }
    }
}
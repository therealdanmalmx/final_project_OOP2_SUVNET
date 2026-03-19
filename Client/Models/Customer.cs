using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Name is required")]
        public string CustomerName {get; set;} = string.Empty;
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }= string.Empty;
        [Required(ErrorMessage = "Phone is required")]
        public string Phone {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string Instructions {get; set;} = string.Empty;
    }
}
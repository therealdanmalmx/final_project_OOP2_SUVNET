using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.DTO;

namespace Client.DTO
{
    public class MealList
    {
        public List<Meals> Meals { get; set; } = [];
    }
}
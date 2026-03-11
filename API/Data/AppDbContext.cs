using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public MenuItemController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MenuItem>>> GetAllMenuItemsByRestaurantId(Guid id)
        {
            var menuItems = await _dbContext.MenuItems.Where(mi => mi.RestaurantId == id).ToListAsync();

            if (menuItems is null)
            {
                return BadRequest($"No restaurant with id {id} exists");
            }

            return Ok(menuItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMenuItem(MenuItem newMenuItem)
        {
            var newItem = new MenuItem
            {
                Name = newMenuItem.Name,
                Description = newMenuItem.Description,
                Price = newMenuItem.Price,
                RestaurantId = newMenuItem.RestaurantId
            };

            if (newItem is null)
            {
                return BadRequest("Could not add a new menu item");
            }

            _dbContext.MenuItems.Add(newItem);
            await _dbContext.SaveChangesAsync();

            return Created("/api/menuitem", newItem);
        }
    }
}
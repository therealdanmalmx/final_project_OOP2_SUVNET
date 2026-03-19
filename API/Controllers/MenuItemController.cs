using API.DTO.MenuItem;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
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
                return NotFound($"No menu item with id {id} exists");
            }

            return Ok(menuItems);
        }

        [HttpGet("dish/{id}")]
        public async Task<IActionResult> GetMenuItem(Guid id)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(id);

            if (menuItem is null)
            {
                return NotFound($"No menu item with id {id} exists");
            }

            Console.WriteLine("Hello");
            return Ok(menuItem);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDTO updateMenutItem, Guid id)
        {
            var menuItemToUpdate = await _dbContext.MenuItems.FindAsync(id);

            if (menuItemToUpdate is null)
            {
                return BadRequest($"No menu item with id {id} exists");
            }

            if (!string.IsNullOrWhiteSpace(updateMenutItem.Name))
            {
                menuItemToUpdate.Name = updateMenutItem.Name;
            }
            if (!string.IsNullOrWhiteSpace(updateMenutItem.Description))
            {
                menuItemToUpdate.Description = updateMenutItem.Description;
            }
            if (updateMenutItem.Price != 0.0m)
            {
                menuItemToUpdate.Price = updateMenutItem.Price;
            }

            _dbContext.MenuItems.Update(menuItemToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(menuItemToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(Guid id)
        {
            var menuItemToDelete = await _dbContext.MenuItems.FindAsync(id);

            if (menuItemToDelete is null)
            {
                return BadRequest($"Could not find a menut item with id {id}");
            }

            _dbContext.MenuItems.Remove(menuItemToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
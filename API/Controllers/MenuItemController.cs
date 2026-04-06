using API.DTO.MenuItem;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]

    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<MenuItem>>> GetAllMenuItemsByRestaurantId(Guid id)
        {
            try
            {
                var menuItems = await _menuItemService.GetAllMenuItemsByRestaurantId(id);
                return Ok(menuItems);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }
        [AllowAnonymous]
        [HttpGet("dish/{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(Guid id)
        {
            try
            {
                var menuItem = await _menuItemService.DeleteMenuItem(id);
                return menuItem;
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }


        [HttpPost]
        public async Task<ActionResult<MenuItem>> AddNewMenuItem(CreateMenuItemDTO newMenuItem)
        {
            try
            {
                var menuItem = await _menuItemService.AddNewMenuItem(newMenuItem);
                return Ok(menuItem);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new {error = ex.Message});
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItem>> UpdateMenuItem(UpdateMenuItemDTO updateMenutItem, Guid id)
        {
            try
            {
                var menuItem = await _menuItemService.UpdateMenuItem(updateMenutItem, id);
                return menuItem;

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {error = ex.Message});
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuItem>> DeleteMenuItem(Guid id)
        {
            try
            {
                var menuItemToDelete = await _menuItemService.DeleteMenuItem(id);
                return Ok(menuItemToDelete);
            }

            catch (ArgumentNullException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }
    }
}
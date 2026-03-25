using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO.MenuItem;
using Microsoft.EntityFrameworkCore;

namespace API.Services.MenuItem
{
    public class MenuItemService : IMenuItemService
    {
        private readonly AppDbContext _dbContext;

        public MenuItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Models.MenuItem> AddNewMenuItem(CreateMenuItemDTO newMenuItem)
        {
            if (newMenuItem is null)
            {
                throw new ArgumentNullException(nameof(newMenuItem), "Menu item can't be empty");
            }

            if (string.IsNullOrWhiteSpace(newMenuItem.Name))
            {
                throw new ArgumentException(nameof(newMenuItem.Name), "Name must be set");
            }
            if (string.IsNullOrWhiteSpace(newMenuItem.Description))
            {
                throw new ArgumentException(nameof(newMenuItem.Description), "Description must be set");
            }
            if (newMenuItem.Price == 0.0m)
            {
                throw new ArgumentException(nameof(newMenuItem.Price), "Price must be set");
            }
            if (newMenuItem.RestaurantId == default)
            {
                throw new ArgumentException(nameof(newMenuItem.RestaurantId), "Restaurant Id must be set");
            }

            var newItem = new Models.MenuItem
            {
                Name = newMenuItem.Name,
                Description = newMenuItem.Description,
                Price = newMenuItem.Price,
                RestaurantId = newMenuItem.RestaurantId
            };

            if (newItem is null)
            {
                throw new ArgumentException(nameof(newItem), "Could not add a new menu item");
            }

            _dbContext.MenuItems.Add(newItem);
            await _dbContext.SaveChangesAsync();

            return newItem;
        }

        public async Task<Models.MenuItem> DeleteMenuItem(Guid id)
        {
            var menuItemToDelete = await _dbContext.MenuItems.FindAsync(id);

            if (menuItemToDelete is null)
            {
                throw new ArgumentNullException(nameof(menuItemToDelete), $"Could not find a menut item with id {id}");
            }

            _dbContext.MenuItems.Remove(menuItemToDelete);
            await _dbContext.SaveChangesAsync();

            return menuItemToDelete;
        }

        public async Task<List<Models.MenuItem>> GetAllMenuItemsByRestaurantId(Guid id)
        {
            var menuItems = await _dbContext.MenuItems.Where(mi => mi.RestaurantId == id).ToListAsync();

            if (menuItems is null)
            {
                throw new ArgumentNullException(nameof(menuItems), $"No menu item with id {id} exists");
            }

            return menuItems;
        }

        public async Task<Models.MenuItem> GetMenuItem(Guid id)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(id);

            if (menuItem is null)
            {
                throw new ArgumentNullException(nameof(menuItem), $"No menu item with id {id} exists");
            }

            return menuItem;
        }

        public async Task<Models.MenuItem> UpdateMenuItem(UpdateMenuItemDTO updateMenutItem, Guid id)
        {
            var menuItemToUpdate = await _dbContext.MenuItems.FindAsync(id);

            if (menuItemToUpdate is null)
            {
                throw new ArgumentNullException(nameof(menuItemToUpdate), $"No menu item with id {id} exists");
            }

            if (string.IsNullOrWhiteSpace(updateMenutItem.Name))
            {
                throw new ArgumentException(nameof(updateMenutItem.Name), "Name must be set");
            }
            menuItemToUpdate.Name = updateMenutItem.Name;

            if (string.IsNullOrWhiteSpace(updateMenutItem.Description))
            {
                throw new ArgumentException(nameof(updateMenutItem.Description), "Description must be set");
            }
            menuItemToUpdate.Description = updateMenutItem.Description;

            if (updateMenutItem.Price == 0.0m)
            {
                throw new ArgumentException(nameof(updateMenutItem.Price), "Price must be set");
            }
            menuItemToUpdate.Price = updateMenutItem.Price;

            _dbContext.MenuItems.Update(menuItemToUpdate);
            await _dbContext.SaveChangesAsync();

            return menuItemToUpdate;
        }
    }
}
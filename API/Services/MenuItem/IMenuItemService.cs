
using API.DTO;

namespace API.Services
{
    public interface IMenuItemService
    {
        Task<List<Models.MenuItem>> GetAllMenuItemsByRestaurantId(Guid id);

        Task<Models.MenuItem> GetMenuItem(Guid id);

        Task<Models.MenuItem> AddNewMenuItem(CreateMenuItemDTO newMenuItem);

        Task<Models.MenuItem> UpdateMenuItem(UpdateMenuItemDTO updateMenutItem, Guid id);

        Task<Models.MenuItem> DeleteMenuItem(Guid id);
    }
}
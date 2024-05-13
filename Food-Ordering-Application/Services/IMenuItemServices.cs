using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface IMenuItemServices
    {
        public Task<IEnumerable<MenuItem>> GetMenuItems();
        public Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryId(int catId);
        public void AddMenuItem(MenuItem menuItem);
        public void RemoveMenuItem(MenuItem menuItem);
        public Task<IEnumerable<MenuItem>> GetUserFavMenuItems(string userId);

        public Task<string> AddFavMenuItem(string userId, int menuId);
        public Task RemoveFromFavouriteMenu(int menuId, string userId);

    }
}

using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface IRestaurantRepo
    {
        public Task<IEnumerable<Restaurant>> GetAllRestaurants();
        
        public Task<IEnumerable<Category>> GetCategoriesByRest(int restId);

        public Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestAndCat(int catId,int restId);
        public Task<IEnumerable<Restaurant>> GetUserFavRestaurants(string userId);

        public Task<string> AddFavRestaurant(string userId, int restId);
        public Task RemoveFromFavourites(int restId, string userId);
    }
}

using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface IRestaurantRepo
    {
        public Task<IEnumerable<Restaurant>> GetAllRestaurants();
        
        public Task<IEnumerable<CategoryDTO>> GetCategoriesByRest(int restId);

        public Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestAndCat(int catId,int restId);
    }
}

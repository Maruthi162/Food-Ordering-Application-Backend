using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Services
{
    public class RestaurantRepo:IRestaurantRepo
    {
        private readonly FlashFoodsContext _context;
        public RestaurantRepo(FlashFoodsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            var res = await _context.Restaurants.ToListAsync();
            return res;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByRest(int restId)
        {
            var categories= await _context.MenuItems.Where(m=>m.RestaurantId==restId).Select(m=>m.Category).Distinct().ToListAsync();

            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.Name,
                CategoryDescription=c.Description,
                CategoryImgUrl=c.ImgUrl
            }).ToList();

            return categoryDTOs;
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestAndCat(int catId, int restId)
        {
            var Items = await _context.MenuItems.Where(mi => mi.CategoryId == catId && mi.RestaurantId == restId).Select(mi => new MenuItemDto
            {
                MenuItemId = mi.MenuItemId,
                MenuItemName = mi.Name,
                MenuItemDescription = mi.Description,
                MenuItemPrice = mi.Price,
                MenuItemAvilability = mi.Availability,

                MenuItemUrl = mi.Img,

            }).ToListAsync();

            return Items;
        }


    }
}

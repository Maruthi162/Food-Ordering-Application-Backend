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

        public async Task<IEnumerable<Category>> GetCategoriesByRest(int restId)
        {
            var RestCats= await _context.Categories.Where(c=>c.MenuItems.Where(m=>m.RestaurantId==restId).Any()).ToListAsync();
            return RestCats;
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

        public async Task<IEnumerable<Restaurant>> GetUserFavRestaurants(string userId)
        {
            var favRests = await _context.UserFavoriteRestaurants.Where(ufr => ufr.Id == userId).Select(fr => fr.Restaurant).ToListAsync();
            //ToListAsync() will never return null, if lisy null, it will return empty list, no need to check for null
            return favRests;
        }
        public async Task<string> AddFavRestaurant(string userId, int restId)
        {
            var existingfavRest= await _context.UserFavoriteRestaurants.FirstOrDefaultAsync(ufr=>ufr.Id == userId && ufr.RestaurantId==restId);
            if (existingfavRest != null)
            {
                return "alredy favorited";
            }
            // create new favorite
            var newFavRest = new UserFavoriteRestaurants
            {
                Id = userId,
                RestaurantId = restId,
            };
            //add favorite to table
            _context.UserFavoriteRestaurants.Add(newFavRest);
            //save the changes to context
            await _context.SaveChangesAsync();
            return "Added to Favorite";
        }

        public async Task RemoveFromFavourites(int restId, string userId)
        {
            UserFavoriteRestaurants favRestToBeRemoved =await _context.UserFavoriteRestaurants.FirstOrDefaultAsync(ufr=>ufr.RestaurantId==restId && ufr.Id==userId);
            if(favRestToBeRemoved != null )
            {
                _context.UserFavoriteRestaurants.Remove(favRestToBeRemoved);
                _context.SaveChanges();
            }
            
        }


    }
}

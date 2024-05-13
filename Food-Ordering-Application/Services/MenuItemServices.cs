using Food_Ordering_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Food_Ordering_Application.Services
{
    public class MenuItemServices:IMenuItemServices
    {
        private readonly FlashFoodsContext _context;
        public MenuItemServices(FlashFoodsContext  context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
           return await _context.MenuItems.Include(m=>m.Category).Include(m=>m.Restaurant).ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryId(int catId)
        {
            var res=await _context.MenuItems.Where(mi=>mi.CategoryId == catId).Include(mi=>mi.Category).Include(mi=>mi.Restaurant).ToListAsync();
            return res;
        }
        public void AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
        }
        //get all favourite menuitemss
        public async Task<IEnumerable<MenuItem>> GetUserFavMenuItems(string userId)
        {
            var res=await _context.UserFavoriteMenuItems.Where(ufmi=>ufmi.Id==userId).Include(ufmi=>ufmi.MenuItem.Category).Include(ufmi=>ufmi.MenuItem.Restaurant).Select(mi => mi.MenuItem).OfType<MenuItem>().ToListAsync();
            return res;
        }
        //adding menu item to favourite
        public async Task<string> AddFavMenuItem(string userId, int menuId)
        {
            var exists= await _context.UserFavoriteMenuItems.FirstOrDefaultAsync(mi=>mi.Id==userId && mi.MenuItemId==menuId);
            if (exists != null)
            {
                return "already favorited";
            }
            var newFav = new UserFavoriteMenuItems
            {
                Id = userId,
                MenuItemId = menuId
            };
            _context.UserFavoriteMenuItems.Add(newFav);
            _context.SaveChanges() ;
            return "added to fav MenuItems";
        }
        //remove menuitem from favourites
        public async Task RemoveFromFavouriteMenu(int menuId, string userId)
        {
            var favItemToBeRemoved =await _context.UserFavoriteMenuItems.FirstOrDefaultAsync(mi => mi.Id == userId && mi.MenuItemId == menuId);

            if (favItemToBeRemoved != null)
            {
               _context.UserFavoriteMenuItems.Remove(favItemToBeRemoved);
                await _context.SaveChangesAsync();

            }
        }
    }
}
